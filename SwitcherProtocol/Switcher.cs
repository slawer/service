using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Platform;

namespace SwitcherProtocol
{
    class Switcher : IPlugin
    {
        public string Author { get { return "Козловский Александр"; } }
        public string ContextMenuString { get { return "Переключатель протокола обмена с устройством"; } }
        public string Description { get { return "Переключет устройство в режим обмена с протокола ModBus на DSN и обратно"; } }
        public string FaceString { get { return "Переключатель протокола обмена с устройством"; } }
        public Icon Icon { get { return Properties.Resources.Andere118; } }
        public string MainMenuString { get { return "Переключет устройство в режим обмена по протоколу ModBus на DSN и обратно"; } }
        public string Name { get { return "Protocol switcher"; } }
        public bool Send { get { return false; } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        // ---- данные класса ----

        private MainForm frm = null;

        public void Activate()
        {
            if (frm == null)
            {
                frm = new MainForm();
                frm.FormClosed += new FormClosedEventHandler(FormClosed);
                frm.Show();
            }
        }

        /// <summary>
        /// закрыли форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            frm.Dispose();
            frm = null;            
        }

        public void Dispose()
        {
        }

        public void Initialize(IApplication application)
        {
        }

        public void Process(Packet packet)
        {
        }
    }
}