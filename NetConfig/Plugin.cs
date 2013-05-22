using System;
using System.Threading;
using System.Windows.Forms;

using Platform;
using NetConfig.GUI;

namespace NetConfig
{
    class Plugin : IPlugin
    {
        public string Author { get { return "Козловский Александр"; } }
        public string ContextMenuString { get { return "Конфигурация сети устройств"; } }
        public string Description { get { return "Данный плагин позволяет определить конфигурацию сети устройств"; } }
        public string FaceString { get { return "Конфигурация сети устройств"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources.Andere005; } }
        public string MainMenuString { get { return "Конфигурация сети устройств"; } }
        public string Name { get { return "Конфигурация сети устройств"; } }
        public bool Send { get { return (Interlocked.Read(ref needPacket) == 1); } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        private long needPacket = 0;

        private IApplication app = null;
        private IProtocol protocol = null;


        private bool started = false;
        private object sync = null;


        private MainForm mForm = null;

        public void Activate()
        {
            lock (sync)
            {
                if (!started)
                {
                    mForm = new MainForm(app);
                    mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);

                    mForm.TurnOnPackets += new EventHandler(TurnOnPackets);
                    mForm.TurnOffPackets += new EventHandler(TurnOffPackets);

                    started = true;
                    mForm.Show();
                }
            }
        }

        public void Dispose() { }

        public void Initialize(IApplication application)
        {
            app = application;
            protocol = application.GetProtocol(ProtocolVersion.x100);

            sync = new object();
        }

        public void Process(Packet packet)
        {
            lock (sync)
            {
                if (protocol.IsFromDevice(packet.packet))
                {
                    mForm.Packet(packet.packet);
                }
            }
        }

        /// <summary>
        /// закрываем форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            started = false;
        }

        // включить поступление пакетов
        private void TurnOnPackets(object sender, EventArgs e)
        {
            Interlocked.Exchange(ref needPacket, 1);
        }

        // отключить поступление пакетов
        private void TurnOffPackets(object sender, EventArgs e)
        {
            Interlocked.Exchange(ref needPacket, 0);
        }
    }
}