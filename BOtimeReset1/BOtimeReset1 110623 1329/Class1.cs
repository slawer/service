using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BOtimeReset1
{
    public class Class1: IPlugin
    {
        long t = 0;
        IApplication app = null;

        public string Author { get { return "Пряничников Александр"; } }
        public string ContextMenuString { get { return "Установить время в БО"; } }
        public string Description { get { return "Синхронизирует время компьютера и Блока Отображения"; } }
        public string FaceString { get { return "Установить время в БО"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources._1380_wall_clockFINAL; } }
        public string MainMenuString { get { return "Установить время в БО"; } }
        public string Name { get { return "Синхронизация часов БО и компьютера"; } }
        public bool Send { get { return (Interlocked.Read(ref t) == 1); } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        public void Activate()
        {
            //MessageBox.Show("Ну наконец-то" + Constants.vbCrLf + "Привет орлы!" );
            //MessageBox.Show(c);
            //string f = string.Format("kjnuhu {0}, knjihnun {1}", 1, 2)

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
