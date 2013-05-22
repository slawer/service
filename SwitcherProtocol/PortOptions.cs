using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace SwitcherProtocol
{
    public partial class PortOptions : Form
    {
        private string portname = string.Empty;

        public PortOptions()
        {
            InitializeComponent();
        }

        public string ComName
        {
            get
            {
                return comboBoxPortNames.Text;
            }
            set
            {
                portname = value;
            }
        }

        public int SizeOfReadBuffer
        {
            get
            {
                return int.Parse(comboBoxBufferRead.Text);
            }
            set
            {
                comboBoxBufferRead.Text = value.ToString();
            }
        }

        public int SizeOfWriteBuffer
        {
            get { return int.Parse(comboBoxBufferWrite.Text); }
            set
            {
                comboBoxBufferWrite.Text = value.ToString();
            }
        }

        public int BaudRate
        {
            get { return int.Parse(comboBoxBaudRate.Text); }
            set
            {
                comboBoxBaudRate.Text = value.ToString();
            }
        }

        public int DataBits
        {
            get { return int.Parse(comboBoxDataBits.Text); }
            set
            {
                comboBoxDataBits.Text = value.ToString();
            }
        }

        public Parity Parity
        {
            get
            {
                return GetParity(comboBoxParity.Text);
            }
            set
            {
                comboBoxParity.SelectedIndex = GetIndexOfParity(value);
            }
        }

        public StopBits StopBits
        {
            get
            {
                switch (comboBoxStopBits.SelectedIndex)
                {
                    case 0:

                        return StopBits.One;

                    case 1:

                        return StopBits.Two;
                }
                return StopBits.None;
            }

            set
            {
                switch (value)
                {
                    case StopBits.One:

                        comboBoxStopBits.SelectedIndex = 0;
                        break;

                    case StopBits.Two:

                        comboBoxStopBits.SelectedIndex = 1;
                        break;

                    default:

                        break;
                }
            }
        }

        private Parity GetParity(string text)
        {
            System.IO.Ports.Parity pr = Parity.None;
            switch (text)
            {
                case "Чет":

                    pr = Parity.Even;
                    break;

                case "Нечет":

                    pr = Parity.Odd;
                    break;

                case "Нет":

                    pr = Parity.None;
                    break;

                case "Маркер":

                    pr = Parity.Mark;
                    break;

                case "Пробел":

                    pr = Parity.Space;
                    break;

                default:

                    break;
            }
            return pr;
        }

        private int GetIndexOfParity(Parity parity)
        {
            int val = -1;
            switch (parity)
            {
                case Parity.Even:

                    val = 0;
                    break;

                case Parity.Mark:

                    val = 3;
                    break;

                case Parity.None:

                    val = 2;
                    break;

                case Parity.Odd:

                    val = 1;
                    break;

                case Parity.Space:

                    val = 4;
                    break;
            }
            return val;

        }

        private void ComOptions_Load(object sender, EventArgs e)
        {
            foreach (string port in SerialPort.GetPortNames())
            {
                comboBoxPortNames.Items.Add(port);
            }
            comboBoxPortNames.SelectedIndex = comboBoxPortNames.Items.Count - 1;
        }
    }
}
