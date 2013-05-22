using System;
using System.Threading;
using System.Windows.Forms;

using Platform;
using BlockConfiguration.GUI;

namespace BlockConfiguration
{
    class Plugin : IPlugin
    {
        public string Author { get { return "Козловский Александр"; } }
        public string ContextMenuString { get { return "Настройка блока отображения"; } }
        public string Description { get { return "Данный плагин позволяет выполнять настройку блоков отображения"; } }
        public string FaceString { get { return "Настройка блока отображения"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources.Andere036; } }
        public string MainMenuString { get { return "Настройка блока отображения"; } }
        public string Name { get { return "Настройщик блока отображения"; } }
        public bool Send { get { return (Interlocked.Read(ref needPacket) == 1); } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        private long needPacket = 0;

        private IProtocol proto = null;
        private IApplication app = null;

        // синхронизация
        private object sync = null;
        private bool started = false;

        MainForm mForm = null;

        public Plugin()
        {
            sync = new object();
        }

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

        public void Dispose()
        {
        }

        public void Initialize(IApplication application)
        {
            app = application;
            proto = app.GetProtocol(ProtocolVersion.x100);
        }

        public void Process(Packet packet)
        {
            lock (sync)
            {
                if (started)
                {
                    if (proto.IsFromDevice(packet.packet) &&
                        proto.GetNumberDevice(packet.packet) == mForm.Device)
                    {
                        mForm.Packet(packet.packet);
                    }
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
            TurnOffPackets(null, null);
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