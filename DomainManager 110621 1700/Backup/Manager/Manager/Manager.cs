using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace Platform
{
    /// <summary>
    /// Выполняет загрузку и управление плагинами
    /// </summary>
    class Manager : IApplication , IPlatform
    {
        // ----- реализация интерфейса -----

        public string Name { get { return "Сервисный центр"; } }
        public string Author { get { return "Developer"; } }
        public string Description { get { return "Сервисный центр"; } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }
        public string ConfigFile { get { return "config.xml"; } }
        

        // ------ платформа -------

        private int speedSurvey = 50;
        private int downTime = 3000;

        private int packetsCount = 7000;
        private int waitTime = 20;

        public int WaitTimeForWorking
        {
            get { return waitTime; }
            set { waitTime = value; }
        }

        public int MaxPacketsCount
        {
            get { return packetsCount; }
            set { packetsCount = value; }
        }
        public int DownTime 
        {
            get { return downTime; }
            set { downTime = value; } 
        }

        public int SpeedSurvey 
        { 
            get { return speedSurvey; }
            set { speedSurvey = value; }
        }
        // ----------- данные класса  ------------

        private IForm form = null;

        private List<Managed> plugins = null;
        private static Manager manager = null;

        private bool needCorrect = false;

        // ----- события ------

        public event Message PluginUnload;

        // ------- буферизация ------

        private bool started = false;

        private Timer timer = null;
        private Mutex mutex = null;

        private List<Packet> share;
        private List<Packet> working;
        
        // ------- Конструктор -------

        protected Manager(IForm frm)
        {
            share = new List<Packet>();
            working = new List<Packet>();

            mutex = new Mutex(false);
            timer = new Timer(new TimerCallback(SendToPlugins), null, Timeout.Infinite, SpeedSurvey);

            form = frm;
        }

        // ------ инстанцировать менеджер -------

        /// <summary>
        /// Инстанцировать менеджер
        /// </summary>
        /// <param name="frm">Форма с которой будет выполняться взаимодействие</param>
        /// <returns></returns>
        public static Manager GetManager(IForm frm)
        {
            if (manager == null) manager = new Manager(frm);
            return manager;
        }

        // -------- загрузить сборку -------

        /// <summary>
        /// Загрузить сборку
        /// </summary>
        /// <param name="path">Папка с плагинами</param>
        public void Load(string path)
        {
            plugins = Loader.LoadPlugins(path);
            if (plugins != null)
            {
                foreach (var plugin in plugins)
                {
                    plugin.plugin.Initialize(this);
                    form.InsertPluginToGiu(plugin.plugin, plugin.key);
                }
            }
            else
                throw new Exception();
        }

        // ---------- обработка поступившего пакета --------

        /// <summary>
        /// Обработка поступившего пакета
        /// </summary>
        /// <param name="packet">Пакет</param>
        public void Process(Packet packet)
        {
            lock (share)
            {
                if (plugins != null)
                {
                    if (plugins.Count > 0)
                    {
                        if (!started)
                        {
                            started = true;
                            timer.Change(0, SpeedSurvey);
                        }
                        share.Add(packet);
                    }
                }
            }
        }

        // ------- рассылка пакета ------

        private void SendToPlugins(object state)
        {
            bool f = false;
            try
            {
                if (mutex.WaitOne(0))
                {
                    f = true;

                    lock (share)
                    {
                        working.AddRange(share);
                        share.Clear();
                    }

                    if (needCorrect)
                    {
                        CorrectPluginsList();
                        needCorrect = false;
                    }
                    InsertNewPackets();

                    bool next = true;
                    while (next)
                    {
                        next = false;
                        foreach (var item in plugins)
                        {
                            switch (item.State)
                            {
                                case ManagedState.Wait:

                                    if (item.plugin.Send)
                                    {
                                        SendPacketToManaged(item);
                                        item.sendPacketTime = DateTime.Now.Ticks;
                                    }

                                    if (item.Packets.Count > 0) next = true;
                                    break;

                                case ManagedState.Processed:

                                    if (item.iAsync.IsCompleted)
                                    {
                                        item.downTime = 0;
                                        item.processedPackets += 1;

                                        item.State = ManagedState.Wait;
                                        if (item.Packets.Count > 0) next = true;
                                    }
                                    else
                                    {
                                        item.downTime = (long)((DateTime.Now.Ticks - item.sendPacketTime) * 1E-4);
                                        if (item.downTime > downTime)
                                        {
                                            item.State = ManagedState.NotResponding;
                                        }
                                        else
                                            if (item.Packets.Count > MaxPacketsCount)
                                            {
                                                item.State = ManagedState.Overflow;
                                            }
                                    }
                                    break;

                                case ManagedState.NotResponding:

                                    needCorrect = true;
                                    break;

                                case ManagedState.Overflow:

                                    needCorrect = true;
                                    break;
                            }
                        }
                    }

                    f = false;
                    mutex.ReleaseMutex();
                }
            }
            finally
            {
                if (f) mutex.ReleaseMutex();
            }
        }

        // -------- отправка пакета плагину ------

        private void SendPacketToManaged(Managed item)
        {
            if (item.Packets.Count > 0)
            {
                Packet packet = item.Packets[0];
                item.iAsync = item.InvokeAsync.BeginInvoke(packet, null, null);

                item.sendPackets += 1;

                if (item.iAsync.CompletedSynchronously)
                {
                    item.processedPackets += 1;
                    item.State = ManagedState.Wait;
                }
                else
                    if (item.iAsync.AsyncWaitHandle.WaitOne(waitTime))
                    {
                        item.processedPackets += 1;
                        item.State = ManagedState.Wait;
                    }
                    else
                        item.State = ManagedState.Processed;

                item.Packets.RemoveAt(0);
            }
        }

        // ------- Корректировка списка --------

        private void CorrectPluginsList()
        {
            bool need = true;
            while (need)
            {
                need = false;
                foreach (Managed item in plugins)
                {
                    if (item.State == ManagedState.Overflow
                        || item.State == ManagedState.NotResponding)
                    {
                        if (PluginUnload != null)
                        {
                            PluginUnload(item);
                        }
                        need = true;
                        plugins.Remove(item);
                        AppDomain.Unload(item.domain);
                        break;
                    }
                }
            }
        }

        // ------- Добавление пакетов ------

        private void InsertNewPackets()
        {
            foreach (Managed item in plugins)
            {
                if (item.plugin.Send)
                {
                    item.Packets.AddRange(working);
                }
                else
                {
                    item.Packets.Clear();
                    item.State = ManagedState.Wait;

                    item.iAsync = null;
                }
            }
            working.Clear();
        }

        // ------- отправка пакета от плагина ------

        public void SendPacket(Packet packet)
        {
            form.SendPacket(packet);
        }

        // ----- выгрузка плагинов ------

        public void Dispose()
        {
            bool f = true;
            try
            {
                if (mutex.WaitOne(5000))
                {
                    f = true;
                    foreach (var item in plugins)
                    {
                        item.plugin.Dispose();
                        AppDomain.Unload(item.domain);
                    }
                    //plugins.Clear();
                    f = false;
                    mutex.ReleaseMutex();
                }
            }
            finally
            {
                if (f) mutex.ReleaseMutex();
            }
        }

        // ---- активировать -------

        public void Activate(string key)
        {
            bool f = false;
            try
            {
                if (mutex.WaitOne(10000))
                {
                    f = true;
                    foreach (var item in plugins)
                    {
                        if (item.key == key)
                        {
                            item.plugin.Activate();
                            break;
                        }
                    }
                    f = false;
                    mutex.ReleaseMutex();
                }
            }
            finally
            {
                if (f) mutex.ReleaseMutex();
            }
        }

        // ------ вернуть информацию о плагинах -----

        public PluginInfo[] PluginsInfo
        {
            get
            {
                List<PluginInfo> pil = new List<PluginInfo>();
                for (int i = 0; i < plugins.Count; i++)
                {
                    PluginInfo p = new PluginInfo(plugins[i].plugin.Version, i + 1);

                    p.Name = plugins[i].plugin.Name;
                    p.Author = plugins[i].plugin.Author;
                    p.Description = plugins[i].plugin.Description;

                    pil.Add(p);
                }

                return pil.ToArray();
            }
        }

        // -------- вернуть загрузчика -------

        public IEFLoader GetEFLoader(FileFormat format)
        {
            return EFLoader.CreateLoader(format);
        }

        // ----- вернуть хранителя -------

        public IEFSaver GetEFSaver(FileFormat format)
        {
            return EFSaver.CreateSaver(format);
        }

        // ---- запись события в журнал ------

        public void ToLogMessage(string Name, string Message, EventLogEntryType TypeEvent)
        {
            Journal.GetLogJournal().WriteEvent(Name + ": " + Message, TypeEvent);
        }

        // ----- получение функций протокола -------

        public IProtocol GetProtocol(ProtocolVersion version)
        {
            return Protocol.GetProtocol(version);
        }

        public IEpromIO GetEpromRW()
        {
            return (new PluginRW(this)) as IEpromIO;
        }
    }

    /// <summary>
    /// Делегат описывающий шаблон вызова метода, который вызывается когда осуществляется выгрузка плагина
    /// </summary>
    /// <param name="item"></param>
    public delegate void Message(Managed item);
}