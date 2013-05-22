using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CMDgenerator1
{
    public class Class1: IPlugin
    {
        long t = 0;
        IApplication app = null;

        public string Author { get { return "Пряничников Александр"; } }
        public string ContextMenuString { get { return "Послать команду в устройство"; } }
        public string Description { get { return "Позволяет послать предустановленную команду в устройство"; } }
        public string FaceString { get { return "Послать команду в устройство"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources._1507_ParallelPort; } }
        public string MainMenuString { get { return "Послать команду в устройство"; } }
        public string Name { get { return "Послать команду в устройство"; } }
        public bool Send { get { return (Interlocked.Read(ref t) == 1); } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        public void Activate()
        {
            Form1 f1 = new Form1(app);
            f1.Show();

        }
        
        public void Dispose() {}

        public void Initialize(IApplication application)
        {
            app = application;
            IProtocol iP = app.GetProtocol(ProtocolVersion.x100);
        }

        public void Process(Packet packet) {}
    }
}
