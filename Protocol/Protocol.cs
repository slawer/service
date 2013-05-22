using System;
using System.Text;
using System.Threading;
using System.Drawing;
using Platform;
using System.Collections.Generic;

namespace Protocol
{
    public class Protocol : IPlugin
    {
        public string Name { get { return "Протокол обмена"; } }
        public string Author { get { return "Козловский Александр"; } }
        public string Description { get { return "Показывает в реальном времени протокол обмена"; } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }
        public bool Send { get { return true; } }

        public string MainMenuString { get { return "Протокол обмена"; } }
        public string ContextMenuString { get { return "Протокол обмена"; } }

        public Icon Icon { get { return Properties.Resources.Default_App; } }
        public string FaceString { get { return "Протокол"; } }

        // -------- 

        IApplication app;
        StringBuilder builder;

        List<ProtocolForm> forms;

        public void Process(Packet packet)
        {
            string mils = packet.dateReceived.Millisecond.ToString();
            if (mils.Length < 2) mils = "00" + mils;
            else if (mils.Length < 3) mils = "0" + mils;
            string date = packet.dateReceived.ToLongTimeString() + "." + mils + " > ";

            builder.Append(date + packet.packet);

            string total = builder.ToString();
            builder.Remove(0, builder.Length);

            lock (forms)
            {
                foreach (var f in forms)
                {
                    f.InsertPacket(total);
                }
            }
        }

        public Protocol()
        {
            forms = new List<ProtocolForm>();
            builder = new StringBuilder();
        }

        public void Activate() 
        {
            ProtocolForm f = new ProtocolForm();
            f.FormClosing += new System.Windows.Forms.FormClosingEventHandler(f_FormClosing);

            lock (forms)
            {
                forms.Add(f);
            }

            f.Show();
        }

        void f_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            lock (forms)
            {
                forms.Remove(sender as ProtocolForm);
            }
        }

        public void Dispose()
        {
            lock (forms)
            {
                bool fl = true;
                while (fl)
                {
                    fl = false;
                    foreach (var f in forms)
                    {
                        f.Close();
                        fl = true;
                        break;
                    }
                }
            }
        }

        public void Initialize(IApplication application)
        {
            app = application;
        }
    }
}