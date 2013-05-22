using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Protocol
{
    public partial class ProtocolForm : Form
    {
        bool started = false;
        private System.Threading.Mutex bMutex;

        private List<string> share;
        private List<string> working;

        public ProtocolForm()
        {
            InitializeComponent();
            bMutex = new System.Threading.Mutex(false);

            share = new List<string>();
            working = new List<string>();
        }

        public void InsertPacket(string packet)
        {
            lock (share)
            {
                if (!started)
                {
                    started = true;
                    timer.Start();
                }

                share.Add(packet);
            }
        }

        private void countPackets_ValueChanged(object sender, EventArgs e)
        {
            if (listBox.Items.Count > (int)countPackets.Value)
            {
                int count = listBox.Items.Count - (int)countPackets.Value;
                for (int i = 0; i < count; i++)
                {
                    listBox.Items.RemoveAt(0);
                }
                listBox.Update();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            bool f = false;
            try
            {
                if (bMutex.WaitOne(3000))
                {
                    f = true;

                    lock (share)
                    {
                        working.AddRange(share);
                        share.Clear();
                    }

                    foreach (string packet in working)
                    {

                        if (checkBoxShowProtocol.Checked)
                        {
                            if (listBox.Items.Count > countPackets.Value)
                            {
                                listBox.Items.RemoveAt(0);
                            }
                            listBox.Items.Add(packet);
                            listBox.SelectedIndex = listBox.Items.Count - 1;
                        }
                    }
                    working.Clear();

                    f = false;
                    bMutex.ReleaseMutex();
                }
            }
            finally
            {
                if (f) bMutex.ReleaseMutex();
            }
        }
    }
}