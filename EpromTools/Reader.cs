using System;
using System.Threading;
using System.Drawing;
using Platform;

namespace EpromWorking
{
    class Woker : IPlugin
    {
        public string Author { get { return "Козловский Александр"; } }
        public string ContextMenuString { get { return "Работа с EPROM устройства"; } }
        public string Description { get { return "Осуществляет работу с EPROM устройства"; } }
        public string FaceString { get { return "Работа с EPROM устройства"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources.Pixadex; } }
        public string MainMenuString { get { return "Работа с EPROM устройства"; } }
        public string Name { get { return "Работа с EPROM устройства"; } }
        public bool Send { get { return (Interlocked.Read(ref needed) == 1); } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        IApplication app = null;

        EpromWorkingForm frm = null;
        bool isWorking = false;
        long needed = 0;

        public void Activate() 
        {
            if (!isWorking)
            {
                frm = new EpromWorkingForm();
                frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);
                frm.app = app;
                frm.Show();

                isWorking = true;
                Interlocked.Exchange(ref needed, 1);
            }
        }

        void frm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Interlocked.Exchange(ref needed, 0);
            isWorking = false;            
        }

        public void Dispose()
        {
        }

        public void Initialize(IApplication application)
        {
            app = application;
        }

        public void Process(Packet packet)
        {
            if (isWorking)
            {
                frm.Packet(packet);
            }
        }
    }

    class CopedCell
    {
        public int row = 0;
        public int col = 0;
        public string value = string.Empty;

        public CopedCell(int Row, int Col, string Value)
        {
            row = Row; col = Col; value = Value;
        }
    }
    enum Operation
    {
        SelectedRead, Read, SelectedWrite, Write, Default
    }

}