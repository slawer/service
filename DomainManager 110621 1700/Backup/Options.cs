using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Platform
{
    public partial class Options : Form
    {
        bool nedClose = false;
        public Options()
        {
            InitializeComponent();
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!nedClose)
            {
                if (e.CloseReason == CloseReason.None)
                {
                    bool cancel = false;
                    if (!GetCorrectIP())
                    {
                        cancel = true;
                        errorProviderHost.SetError(this.textBoxHost, "Не корректно указан IP сервера");
                    }

                    if (!GetCorrectPort())
                    {
                        cancel = true;
                        errorProviderPort.SetError(this.textBoxPort, "Введенное значение не является допустимым номером порта сервера");
                    }

                    e.Cancel = cancel;
                }
            }
        }

        private void textBoxHost_TextChanged(object sender, EventArgs e)
        {
            errorProviderHost.Clear();
        }

        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {
            errorProviderPort.Clear();
        }

        bool GetCorrectIP()
        {
            try
            {
                IPHostEntry entry = Dns.GetHostByName(textBoxHost.Text);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        bool GetCorrectPort()
        {
            try
            {
                int port = int.Parse(textBoxPort.Text);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            nedClose = true;
        }
    }
}