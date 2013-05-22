using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Platform
{
    public partial class Main : Form , IForm
    {
        Manager m = null;

        private int port = 56000;
        private string host = "localhost";

        private int SpeedSurvey = 50;
        private int DownTime = 3000;
        private int MaxPacketsCount = 7000;
        private int WaitTimeForWorking = 20;

        private bool autoconnect = false;

        SocketClient client = null;
        Packer paker = null;

        private bool reConnect = true;
        private ConnectState state = ConnectState.Disconnected;

        // ---- буферизация отправки ---

        private bool started = false;

        System.Threading.Timer timer = null;
        Mutex mutex = null;

        List<Packet> share;
        List<Packet> working;

        delegate void Setter(string status);
        delegate void Remover(Managed item);

        Setter setter;
        Remover remover;

        bool exited = false;
        // ----- Конструктор -----

        public Main()
        {
            InitializeComponent();

            mutex = new Mutex(false);
            timer = new System.Threading.Timer(new TimerCallback(SendPacketToServer), null, Timeout.Infinite, 50);

            share = new List<Packet>();
            working = new List<Packet>();

            remover = new Remover(Deleter);
            setter = new Setter(SetterFunction);
        }

        // --- установка статуса -----

        void SetterFunction(string status)
        {
            StatusLabel.Text = status;
        }

        // ---- удаление из интерфейса ------

        void Deleter(Managed item)
        {
            foreach (ToolStripItem mitem in services.DropDownItems)
            {
                if (mitem.Tag.Equals(item.key))
                {
                    services.DropDownItems.Remove(mitem);
                    break;
                }
            }

            foreach (ToolStripItem mitem in contextServices.DropDownItems)
            {
                if (mitem.Tag.Equals(item.key))
                {
                    contextServices.DropDownItems.Remove(mitem);
                    break;
                }
            }

            foreach (ListViewItem mitem in listView.Items)
            {
                if (mitem.Tag.Equals(item.key))
                {
                    listView.Items.Remove(mitem);
                }
            }

            string message = "Компонент: " + item.plugin.Name + " удален из системы. Так как его статус равен: ";
            switch (item.State)
            {
                case ManagedState.Overflow:

                    message += "переполнен";
                    break;

                case ManagedState.NotResponding:

                    message += "не отвечает";
                    break;
            }
            MessageBox.Show(message);
        }

        // ----- посылка пвкета серверу ---------

        void SendPacketToServer(object state)
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

                    foreach (Packet p in working)
                    {
                        if (client.Connected)
                        {
                            byte[] data = System.Text.Encoding.Default.GetBytes(p.packet);
                            client.Send(data);
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

        // ------ реализация интерфейса -------

        public void SendPacket(Packet packet)
        {
            lock (share)
            {
                if (client == null) return;
                if (!client.Connected) return;

                if (!started)
                {
                    started = true;
                    timer.Change(0, 50);
                }
                share.Add(packet);
            }
        }

        public void InsertPluginToGiu(IPlugin plugin, string key)
        {
            ToolStripMenuItem mainItem = new ToolStripMenuItem(plugin.MainMenuString);
            mainItem.Click += new EventHandler(mainItem_Click);

            mainItem.Tag = key;
            services.DropDownItems.Add(mainItem);

            ToolStripMenuItem contextItem = new ToolStripMenuItem(plugin.ContextMenuString);
            contextItem.Click += new EventHandler(mainItem_Click);

            contextItem.Tag = key;
            contextServices.DropDownItems.Add(contextItem);

            if (plugin.Icon != null)
            {
                imageList.Images.Add(plugin.Icon);

                ListViewItem lstItem = new ListViewItem(plugin.FaceString, imageList.Images.Count - 1);
                lstItem.Tag = key;

                listView.Items.Add(lstItem);
            }
        }

        // ------ выбрали в меню --------

        void mainItem_Click(object sender, EventArgs e)
        {
            string key = GetKey(sender);
            m.Activate(key);
        }

        string GetKey(object sender)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem menu = sender as ToolStripMenuItem;
                return Convert.ToString(menu.Tag);
            }
            return Convert.ToString((sender as ListViewItem).Tag);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon.Icon = Properties.Resources.disconnect;
            string set_file = "config.xml";
            if (System.IO.File.Exists(set_file))
            {
                Settings setts = Settings.Load(set_file);
                foreach (Parameter parameter in setts.Parameters)
                {
                    switch (parameter.Name)
                    {
                        case "host":

                            host = parameter.Value;
                            break;

                        case "port":

                            port = Convert.ToInt32(parameter.Value);
                            break;

                        case "autoconnect":

                            autoconnect = Convert.ToBoolean(parameter.Value);
                            break;

                        case "SpeedSurvey":

                            SpeedSurvey = Convert.ToInt32(parameter.Value);
                            break;

                        case "DownTime":

                            DownTime = Convert.ToInt32(parameter.Value);
                            break;

                        case "MaxPacketsCount":

                            MaxPacketsCount = Convert.ToInt32(parameter.Value);
                            break;

                        case "WaitTimeForWorking":

                            WaitTimeForWorking = Convert.ToInt32(parameter.Value);
                            break;
                    }
                }
            }
            else
            {
                Settings sett = Settings.CreateNewSettings();

                Parameter _host = new Parameter("host");
                Parameter _port = new Parameter("port");
                Parameter _autoconnect = new Parameter("autoconnect");
                Parameter _SpeedSurvey = new Parameter("SpeedSurvey");
                Parameter _DownTime = new Parameter("DownTime");
                Parameter _MaxPacketsCount = new Parameter("MaxPacketsCount");
                Parameter _WaitTimeForWorking = new Parameter("WaitTimeForWorking");

                _host.Value = host;
                _port.Value = port.ToString();
                _autoconnect.Value = autoconnect.ToString();

                _DownTime.Value = DownTime.ToString();
                _SpeedSurvey.Value = SpeedSurvey.ToString();
                _MaxPacketsCount.Value = MaxPacketsCount.ToString();
                _WaitTimeForWorking.Value = WaitTimeForWorking.ToString();

                sett.Insert(_host);
                sett.Insert(_port);
                sett.Insert(_autoconnect);
                sett.Insert(_DownTime);
                sett.Insert(_SpeedSurvey);
                sett.Insert(_MaxPacketsCount);
                sett.Insert(_WaitTimeForWorking);

                sett.Save(set_file, SaveOptions.CreateNewXml);
            }

            // ------- загрузка плагинов -----

            m = Manager.GetManager(this);
            
            m.DownTime = DownTime;
            m.MaxPacketsCount = MaxPacketsCount;
            m.SpeedSurvey = SpeedSurvey;
            m.WaitTimeForWorking = WaitTimeForWorking;

            m.PluginUnload += new Message(m_PluginUnload);
            m.Load(Application.StartupPath);
        }

        // ------ удаляем информацтю о плагине ------

        void m_PluginUnload(Managed item)
        {
            Invoke(remover, item);
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            Focus();
            Activate();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (!exited)
            {
                exited = true;

                m.Dispose();
                Application.Exit();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exited)
            {
                exited = true;
                exit_Click(this, null);
            }
        }

        private void listView_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                mainItem_Click(item, null);
            }
        }

        private void about_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog(this);
        }

        private void settings_Click(object sender, EventArgs e)
        {
            Options opt = new Options();

            opt.textBoxHost.Text = host;
            opt.textBoxPort.Text = port.ToString();
            opt.checkBoxAutostart.Checked = autoconnect;

            opt.numericUpDownDownTime.Value = DownTime;
            opt.numericUpDownMaxPacketsCount.Value = MaxPacketsCount;
            opt.numericUpDownSpeedSyrvey.Value = SpeedSurvey;
            opt.numericUpDownWaitTimeForWorking.Value = WaitTimeForWorking;

            if (opt.ShowDialog(this) == DialogResult.OK)
            {
                host = opt.textBoxHost.Text;
                port = Convert.ToInt32(opt.textBoxPort.Text);
                autoconnect = opt.checkBoxAutostart.Checked;

                DownTime = (int)opt.numericUpDownDownTime.Value;
                MaxPacketsCount = (int)opt.numericUpDownMaxPacketsCount.Value;
                SpeedSurvey = (int)opt.numericUpDownSpeedSyrvey.Value;
                WaitTimeForWorking = (int)opt.numericUpDownWaitTimeForWorking.Value;

                Settings sett = Settings.CreateNewSettings();

                Parameter _host = new Parameter("host");
                Parameter _port = new Parameter("port");

                Parameter _autoconnect = new Parameter("autoconnect");
                Parameter _SpeedSurvey = new Parameter("SpeedSurvey");

                Parameter _DownTime = new Parameter("DownTime");
                Parameter _MaxPacketsCount = new Parameter("MaxPacketsCount");
                Parameter _WaitTimeForWorking = new Parameter("WaitTimeForWorking");

                _host.Value = host;
                _port.Value = port.ToString();
                _autoconnect.Value = autoconnect.ToString();

                _DownTime.Value = DownTime.ToString();
                _SpeedSurvey.Value = SpeedSurvey.ToString();
                _MaxPacketsCount.Value = MaxPacketsCount.ToString();
                _WaitTimeForWorking.Value = WaitTimeForWorking.ToString();


                sett.Insert(_host);
                sett.Insert(_port);
                sett.Insert(_autoconnect);

                sett.Insert(_DownTime);
                sett.Insert(_SpeedSurvey);
                sett.Insert(_MaxPacketsCount);
                sett.Insert(_WaitTimeForWorking);

                sett.Save("config.xml", SaveOptions.CreateNewXml);
            }
        }

        private void plugins_Click(object sender, EventArgs e)
        {
            PluginsForm f = new PluginsForm();

            foreach (PluginInfo pinfo in m.PluginsInfo)
            {
                ListViewItem item = new ListViewItem(pinfo.number.ToString());

                ListViewItem.ListViewSubItem name = new ListViewItem.ListViewSubItem(item, pinfo.Name);
                ListViewItem.ListViewSubItem author = new ListViewItem.ListViewSubItem(item, pinfo.Author);
                ListViewItem.ListViewSubItem description = new ListViewItem.ListViewSubItem(item, pinfo.Description);
                ListViewItem.ListViewSubItem version = new ListViewItem.ListViewSubItem(item, pinfo.version.ToString());

                item.SubItems.Add(name);
                item.SubItems.Add(author);
                item.SubItems.Add(description);
                item.SubItems.Add(version);

                f.listView.Items.Add(item);
            }

            f.ShowDialog(this);
        }

        private void connect_Click(object sender, EventArgs e)
        {
            if (state == ConnectState.Disconnected)
            {
                IPHostEntry entry = Dns.GetHostByName(host);                
                client = new SocketClient();

                client.Port = port;
                client.Host = entry.AddressList[0].ToString();

                client.OnConnect += new EventHandler(client_OnConnect);
                client.OnDisconnect += new EventHandler(client_OnDisconnect);

                paker = new Packer(client);
                paker.OnPacket += new Packer.PacketEventHandler(paker_OnPacket);

                client.Connect();
            }
            reConnect = true;
        }

                // ----- Есть коннект -------

        void client_OnConnect(object sender, EventArgs e)
        {
            state = ConnectState.Connected;

            Invoke(setter, "Подключен к серверу");             
            notifyIcon.Icon = Properties.Resources.connect;
            notifyIcon.ShowBalloonTip(1000, "Сервисный центр", "Подключен к серверу", ToolTipIcon.Info);
        }

        // ------- отсоединен -------

        void client_OnDisconnect(object sender, EventArgs e)
        {
            state = ConnectState.Disconnected;
            notifyIcon.Icon = Properties.Resources.disconnect;
            Invoke(setter, "Не подключен к серверу"); 
            notifyIcon.ShowBalloonTip(1000, "Сервисный центр", "Не подключен к серверу", ToolTipIcon.Info);
            if (reConnect) connect_Click(null, null);
            reConnect = true;
        }

        void paker_OnPacket(string packet)
        {
            Packet item = new Packet(packet, DateTime.Now, null);
            m.Process(item);
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            if (state == ConnectState.Connected)
            {
                if (client != null)
                {
                    if (client.Connected)
                    {
                        client.Disconnect();
                    }
                }
            }
            reConnect = false;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            if (autoconnect)
            {
                connect_Click(null, null);
            }
        }
    }

    enum ConnectState
    {
        Connected,
        Disconnected
    }
}