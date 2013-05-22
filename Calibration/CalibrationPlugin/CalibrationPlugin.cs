using System;
using System.Threading;
using System.Windows.Forms;

using Platform;
using Calibration.CalibrationPlugin.GUI;

namespace Calibration
{
    class PCalibrationPlugin : IPlugin
    {
        public string Author { get { return "Козловский Александр"; } }
        public string ContextMenuString { get { return "Калибровка"; } }
        public string Description { get { return "Калибровка"; } }
        public string FaceString { get { return "Калибровка"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources.H001; } }
        public string MainMenuString { get { return "Калибровка"; } }
        public string Name { get { return "Калибровка"; } }
        public bool Send { get { return (Interlocked.Read(ref needPacket) == 1) ; } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        // разрешить/запретит отправку паветов
        private long needPacket = 0;

        // ---------------------
        private IProtocol proto = null;
        private IApplication app = null;

        // синхронизация
        private object sync = null;
        private bool started = false;

        // интерфейсная часть
        private MainForm mForm = null;

        // создаем экземпляр класса
        public PCalibrationPlugin()
        {
            sync = new object();
        }

        public void Activate()
        {
            lock (sync)
            {
                if (!started)
                {
                    mForm = new MainForm(app, app.GetEpromRW(), proto);
                    mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);

                    mForm.TurnOnPackets += new EventHandler(TurnOnPackets);
                    mForm.TurnOffPackets += new EventHandler(TurnOffPackets);

                    started = true;
                    mForm.Show();
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

        public void Dispose() { }

        // инициализация
        public void Initialize(IApplication application)
        {
            app = application;
            proto = app.GetProtocol(ProtocolVersion.x100);
        }

        // обработка пакета
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
    }
}