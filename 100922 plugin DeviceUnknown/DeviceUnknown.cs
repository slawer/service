using System;
using System.Drawing;
using System.Threading;

using Platform;

namespace DeviceUnknown
{
    class DeviceUnknown : IPlugin
    {
        public string Author { get { return "Козловский Александр"; } }
        public string ContextMenuString { get { return "Установка начальных настроек устройства"; } }
        public string Description { get { return "Позволяет присвоить номер устройству. Работает по широковещательной команде опроса"; } }
        public string FaceString { get { return "Установка начальных настроек устройства"; } }
        public System.Drawing.Icon Icon { get { return Properties.Resources.Drive_Ext_ZIP; } }
        public string MainMenuString { get { return "Установка начальных настроек устройства"; } }
        public string Name { get { return "Установка начальных настроек устройства"; } }
        public bool Send { get { return (Interlocked.Read(ref needed) == 1); } }
        public Version Version { get { return new Version(1, 0, 0, 0); } }

        IApplication app = null;

        MainForm frm = null;

        bool isWorking = false;
        long needed = 0;

        public void Activate()
        {
            if (!isWorking)
            {
                frm = new MainForm(app);
                frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frm_FormClosing);
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

        /// <summary>
        /// Получает пакет для обработки
        /// </summary>
        /// <param name="packet">Пакет</param>
        public void Process(Packet packet)
        {
            if (isWorking)
            {
                frm.Packet(packet);
            }
        }
    }
}