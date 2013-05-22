using System;
using System.IO;
using System.Text;
using Platform;
using System.Threading;
using System.Collections.Generic;

namespace WriterToFile
{
    class Writer : IPlugin
    {
        // ------ данные класса -------

        private string filePath = string.Empty;
        private bool needWrite = false;

        private List<Filter> filters;
        private StreamWriter writer = null;

        private bool started = false;

        private Timer timer = null;
        private Mutex mutex = null;

        private List<Packet> share;
        private List<Packet> working;

        private StringBuilder builder;

        // ----------------------------

        public string Author { get { return "Козловский Александр"; } }
        public string ContextMenuString { get { return "Сохранить в файл протокол обмена"; } }
        public string Description { get { return "Записывает в файл обмен пакетами"; } }
        public string FaceString { get { return "Сохранить в файл протокол обмена"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources.Text_Editor; } }
        public string MainMenuString { get { return "Сохранить в файл протокол обмена"; } }
        public string Name { get { return "Запись в файл протокола обмена"; } }
        public bool Send { get { return true; } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        public void Activate()
        {
                WriterForm frm = new WriterForm();

                frm.Filters = filters.ToArray();
                frm.textBoxFilePath.Text = filePath;
                frm.checkBoxNeedWrite.Checked = needWrite;
                frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);
                frm.Show();
        }

        void frm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            bool f = false;
            try
            {
                if (mutex.WaitOne(3000))
                {                    
                    f = true;

                    WriterForm frm = sender as WriterForm;
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel) return;

                    needWrite = frm.checkBoxNeedWrite.Checked;

                    filters.Clear();
                    filters.AddRange(frm.Filters);

                    if (filePath != frm.textBoxFilePath.Text)
                    {
                        filePath = frm.textBoxFilePath.Text;
                        
                        if (writer != null)
                        {
                            writer.Close();
                            writer.Dispose();
                        }

                        writer = new StreamWriter(filePath);
                        writer.AutoFlush = true;
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

        public void Dispose() 
        {
            bool f = false;
            try
            {
                if (mutex.WaitOne(3000))
                {
                    f = true;
                    if (writer != null)
                    {
                        writer.Close();
                        writer.Dispose();
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
        
        public void Initialize(IApplication application)
        {
            filters = new List<Filter>();

            mutex = new Mutex(false);
            timer = new Timer(new TimerCallback(CallBack), null, Timeout.Infinite, 50);

            share = new List<Packet>();
            working = new List<Packet>();

            builder = new StringBuilder();
        }

        public void Process(Packet packet)
        {
            lock (share)
            {
                if (!started)
                {
                    timer.Change(0, 50);
                    started = true;
                }
                share.Add(packet);
            }
        }

        // ------ тик -------

        private void CallBack(object state)
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

                    if (needWrite == false || writer == null)
                    {
                        working.Clear();
                        return;
                    }
                    
                    // ------ пишем в файл -----
                    
                    foreach (Packet packet in working)
                    {
                        bool nedWrite = true;
                        foreach (var item in filters)
                        {
                            if (item.state)
                            {
                                nedWrite = item.ValidPacket(packet.packet);
                                if (nedWrite) break;
                            }
                        }

                        if (nedWrite)
                        {
                            string totalString;

                            string mils = packet.dateReceived.Millisecond.ToString();
                            if (mils.Length < 2) mils = "00" + mils;
                            else if (mils.Length < 3) mils = "0" + mils;
                            string date = packet.dateReceived.ToLongTimeString() + "." + mils + " > ";

                            builder.Append(date + packet.packet);

                            totalString = builder.ToString();
                            builder = builder.Remove(0, builder.Length);

                            writer.WriteLine(totalString);                            
                        }
                    }
                    working.Clear();

                    f = false;
                    mutex.ReleaseMutex();
                }
            }
            finally
            {
                if (f) mutex.ReleaseMutex();
            }
        }
    }

    public class Filter
    {
        public string filter = string.Empty;
        public bool state = false;

        public bool ValidPacket(string packet)
        {
            string ladd = packet.Substring(2, 2);
            if (ladd == filter)
                return true;
            return false;
        }
    }
}