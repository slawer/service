using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Platform;
using System.Threading;

namespace DeviceTable
{
    public partial class DevicesForm : Form
    {
        mStr[] mstr = null;
        bool inHex = false;

        public DevicesForm()
        {
            InitializeComponent();
            mstr = new mStr[32];


            for (int i = 0; i < 32; i++)
            {
                mstr[i] = new mStr();
                mstr[i].dt = DateTime.Now;
                mstr[i].key = (i + 1);

                foreach (Control control in this.Controls)
                {
                    if (control.Tag.ToString() == mstr[i].key.ToString())
                    {
                        mstr[i].mLabel = control as Label;
                        break;
                    }
                }
            }
        }

        public void SetPacket(Packet packet)
        {
            string n = packet.packet.Substring(2, 2);
            
            try
            {

                int index = int.Parse(n, System.Globalization.NumberStyles.AllowHexSpecifier) - 1;
                byte b = (byte)int.Parse(n[0].ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);

                int lb = b & 0x8;
                if (lb == 0) return;

                index = index - 128;
                if (index > 31) return;

                lock (mstr)
                {
                    mstr[index].dt = packet.dateReceived;
                    mstr[index].mLabel.BackColor = Color.LimeGreen;
                }

            }
            catch (Exception)
            {
            }
        }

        // ----- тик --------

        private void timer_Tick(object sender, EventArgs e)
        {
            lock (mstr)
            {
                DateTime now = DateTime.Now;
                foreach (var item in mstr)
                {
                    long msec = (long)((now.Ticks - item.dt.Ticks) * 1E-4);
                    if (msec > 3000)
                    {
                        item.mLabel.BackColor = Color.DarkSalmon;
                    }
                }
            }
        }

        private void DevicesForm_Shown(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lock (mstr)
            {
                if (inHex)
                {
                    foreach (var item in mstr)
                    {
                        int n = int.Parse(item.mLabel.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
                        if (n == 63) continue;

                        item.mLabel.Text = string.Format("{0:D2}", n);
                    }
                    inHex = false;
                }
            }
        }

        private void шеснадцатеричныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (mstr)
            {
                if (!inHex)
                {
                    foreach (var item in mstr)
                    {
                        int n = 0;
                        try
                        {
                            n = int.Parse(item.mLabel.Text);
                            item.mLabel.Text = string.Format("{0:X2}", n);
                        }
                        catch (Exception) { }
                        
                    }
                    inHex = true;
                }
            }
        }
    }

    class mStr
    {
        public DateTime dt;
        public int key;
        public Label mLabel;

        public mStr()
        {
            dt = new DateTime();
        }        
    }
}