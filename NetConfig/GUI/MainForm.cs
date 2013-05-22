using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Platform;

namespace NetConfig.GUI
{
    public partial class MainForm : Form
    {
        public event EventHandler TurnOnPackets;
        public event EventHandler TurnOffPackets;

        IApplication application = null;
        IProtocol protocol = null;

        private Maker maker = null;
        private Inserter inserter = null;

        IAsyncResult result = null;
        private StatusHandle status = null;

        Incer incer;
        Initer initer;

        public MainForm(IApplication app)
        {
            InitializeComponent();

            application = app;
            protocol = application.GetProtocol(ProtocolVersion.x100);

            inserter = new Inserter(insert);
            status = new StatusHandle();

            incer = new Incer(IncP);
            initer = new Initer(InitProgressBar);
        }

        private void insert(string packet)
        {
            listBox.Items.Add(packet);
            ShowDevice(protocol.GetNumberDevice(packet));
        }

        private void MakeBroadcast()
        {
            try
            {
                TurnOnPackets(null, null);

                string packet = protocol.CreateCommand(Device.D3F, Command.Read, PageNumber.P1, 0, 16, string.Empty);
                application.SendPacket(new Packet(packet, DateTime.Now, null));

                Thread.Sleep(status.Interval);
                Invoke(incer, 1);
            }
            finally
            {
                TurnOffPackets(null, null);
            }
        }

        private void MakeCuclic()
        {
            try
            {
                TurnOnPackets(null, null);

                for (int i = 1; i < 32; i++)
                {
                    string packet = protocol.CreateCommand(i, Command.Read, 1, 0, 16, null);
                    application.SendPacket(new Packet(packet, DateTime.Now, null));

                    Thread.Sleep(status.Interval);
                    Invoke(incer, 1);
                }

                status.Working = false;
            }
            finally
            {
                TurnOffPackets(null, null);
            }
        }

        /// <summary>
        /// Принимает пакет на обработку
        /// </summary>
        /// <param name="packet"></param>
        public void Packet(string packet)
        {
            if (protocol.PageAdress(packet) == 1 && protocol.GetData(packet).Length == 32)
            {
                Invoke(inserter, packet);
                ShowDevice(protocol.GetNumberDevice(packet));
            }            
        }

        /// <summary>
        /// Показать номер устройства на форме
        /// </summary>
        /// <param name="device"></param>
        private void ShowDevice(int device)
        {
            switch (device)
            {
                case 1: device1.BackColor = Color.LimeGreen; break;
                case 2: device2.BackColor = Color.LimeGreen; break;
                case 3: device3.BackColor = Color.LimeGreen; break;
                case 4: device4.BackColor = Color.LimeGreen; break;
                case 5: device5.BackColor = Color.LimeGreen; break;
                case 6: device6.BackColor = Color.LimeGreen; break;
                case 7: device7.BackColor = Color.LimeGreen; break;
                case 8: device8.BackColor = Color.LimeGreen; break;
                case 9: device9.BackColor = Color.LimeGreen; break;
                case 10: device10.BackColor = Color.LimeGreen; break;
                case 11: device11.BackColor = Color.LimeGreen; break;
                case 12: device12.BackColor = Color.LimeGreen; break;
                case 13: device13.BackColor = Color.LimeGreen; break;
                case 14: device14.BackColor = Color.LimeGreen; break;
                case 15: device15.BackColor = Color.LimeGreen; break;
                case 16: device16.BackColor = Color.LimeGreen; break;
                case 17: device17.BackColor = Color.LimeGreen; break;
                case 18: device18.BackColor = Color.LimeGreen; break;
                case 19: device19.BackColor = Color.LimeGreen; break;
                case 20: device20.BackColor = Color.LimeGreen; break;
                case 21: device21.BackColor = Color.LimeGreen; break;
                case 22: device22.BackColor = Color.LimeGreen; break;
                case 23: device23.BackColor = Color.LimeGreen; break;
                case 24: device24.BackColor = Color.LimeGreen; break;
                case 25: device25.BackColor = Color.LimeGreen; break;
                case 26: device26.BackColor = Color.LimeGreen; break;
                case 27: device27.BackColor = Color.LimeGreen; break;
                case 28: device28.BackColor = Color.LimeGreen; break;
                case 29: device29.BackColor = Color.LimeGreen; break;
                case 30: device30.BackColor = Color.LimeGreen; break;
                case 31: device31.BackColor = Color.LimeGreen; break;
                default: deviceNO.BackColor = Color.LimeGreen; break;
            }
        }

        /// <summary>
        /// Сброс всех устройст
        /// </summary>
        private void ResetDevice()
        {
            device1.BackColor = Color.Tomato;
            device2.BackColor = Color.Tomato;
            device3.BackColor = Color.Tomato;
            device4.BackColor = Color.Tomato;
            device5.BackColor = Color.Tomato;
            device6.BackColor = Color.Tomato;
            device7.BackColor = Color.Tomato;
            device8.BackColor = Color.Tomato;
            device9.BackColor = Color.Tomato;
            device10.BackColor = Color.Tomato;
            device11.BackColor = Color.Tomato;
            device12.BackColor = Color.Tomato;
            device13.BackColor = Color.Tomato;
            device14.BackColor = Color.Tomato;
            device15.BackColor = Color.Tomato;
            device16.BackColor = Color.Tomato;
            device17.BackColor = Color.Tomato;
            device18.BackColor = Color.Tomato;
            device19.BackColor = Color.Tomato;
            device20.BackColor = Color.Tomato;
            device21.BackColor = Color.Tomato;
            device22.BackColor = Color.Tomato;
            device23.BackColor = Color.Tomato;
            device24.BackColor = Color.Tomato;
            device25.BackColor = Color.Tomato;
            device26.BackColor = Color.Tomato;
            device27.BackColor = Color.Tomato;
            device28.BackColor = Color.Tomato;
            device29.BackColor = Color.Tomato;
            device30.BackColor = Color.Tomato;
            device31.BackColor = Color.Tomato;
            deviceNO.BackColor = Color.Tomato;
        }

        /// <summary>
        /// определяем настройки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void options_Click(object sender, EventArgs e)
        {
            OptionsForm opt = new OptionsForm(status);
            opt.ShowDialog(this);
        }

        private void readConfig_Click(object sender, EventArgs e)
        {
            if (result == null || result.IsCompleted)
            {
                ResetDevice();
                listBox.Items.Clear();

                if (status.Algorithm == UsedAlgorithm.Cucliced)
                {
                    Invoke(initer, 0, 31);
                    maker = new Maker(MakeCuclic);
                }
                else
                {
                    Invoke(initer, 0, 1);
                    maker = new Maker(MakeBroadcast);
                }

                status.Working = true;
                result = maker.BeginInvoke(null, null);
            }
        }

        /// <summary>
        /// инициализировать статус
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void InitProgressBar(int min, int max)
        {
            progressBar.Minimum = min;
            progressBar.Maximum = max;

            progressBar.Value = 0;
        }

        /// <summary>
        /// увеличить значение статуса на еденицу
        /// </summary>
        /// <param name="value"></param>
        private void IncP(int value)
        {
            progressBar.Increment(value);
            Application.DoEvents();
        }
    }

    delegate void Inserter(string packet);
    delegate void Maker();
    delegate void Initer(int min, int max);
    delegate void Incer(int value);
}