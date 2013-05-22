using System;
using System.Drawing;
using Platform;

namespace DeviceTable
{
    class DeviceTable : IPlugin
    {
        bool disposing = false;
        DevicesForm frm = null;

        public string Author { get { return "Козловский Александр"; } }
        public string ContextMenuString { get { return "Табло состояния сети устройств"; } }
        public string Description { get { return "Показывает табло состояния сети устройств"; } }
        public string FaceString { get { return "Табло состояния сети устройств"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources.xScope; } }
        public string MainMenuString { get { return "Табло состояния сети устройств"; } }
        public string Name { get { return "Табло состояния сети устройств"; } }
        public bool Send { get { return true; } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        // ----- активация -------

        public void Activate()
        {
            if (frm == null)
            {
                frm = new DevicesForm();
                frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);            
            }
            frm.Activate();
            frm.Show();
        }

        
        public void Dispose()
        {
            disposing = true;
        }

        // ----- инициализация ------

        public void Initialize(IApplication application)
        {
            frm = new DevicesForm();
            frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing); 
        }

        // ------ закрытие формы -------

        void frm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!disposing)
            {
                lock (frm)
                {
                    e.Cancel = true;
                    frm.Hide();
                }
            }
        }

        // ------ пришел пакет -----------

        public void Process(Packet packet)
        {
            lock (frm)
            {
                if (frm != null)
                {
                    frm.SetPacket(packet);
                }
            }
        }
    }
}