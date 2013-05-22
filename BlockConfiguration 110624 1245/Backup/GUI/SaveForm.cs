using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BlockConfiguration.IO;

namespace BlockConfiguration.GUI
{
    public partial class SaveForm : Form
    {
        delegate void Maker();

        delegate void Initer(int min, int max);
        delegate void Incer(int value);

        Messeger mes = null;
        Maker maker = null;

        Incer incer;
        Initer initer;

        IAsyncResult async = null;

        private BlockConfigurationIO pBios = null;
        private HandleIO pObject = null;

        object obj = null;

        public SaveForm(BlockConfigurationIO bios, HandleIO obje)
        {
            InitializeComponent();

            pBios = bios;
            pObject = obje;

            obj = new object();

            incer = new Incer(IncP);
            initer = new Initer(InitProgressBar);

            DialogResult = DialogResult.OK;

            pBios.eSaveCompleteReadEpromLine += new EventHandler(pBios_eSaveCompleteReadEpromLine);
            pBios.eSaveMorePopitReadEpromLine += new EventHandler(pBios_eSaveMorePopitReadEpromLine);
            pBios.eSaveTimeoutReadEpromLine += new EventHandler(pBios_eSaveTimeoutReadEpromLine);
        }

        /// <summary>
        /// Отображает сообщение
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        private void ShowMessage(string text)
        {
            MessageBox.Show(this, "Не удалось прочитать конфигурацию устройства." +
                text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void InitProgressBar(int min, int max)
        {
            progressBar.Minimum = min;
            progressBar.Maximum = max;

            progressBar.Value = 0;
        }

        private void IncP(int value)
        {
            progressBar.Increment(value);
            Application.DoEvents();
        }

        void pBios_eSaveTimeoutReadEpromLine(object sender, EventArgs e)
        {
            lock (obj)
            {
                this.Invoke(mes, "Устройство не отвечает на запросы");
            }
        }

        void pBios_eSaveMorePopitReadEpromLine(object sender, EventArgs e)
        {
            lock (obj)
            {
                this.Invoke(mes, "Превышен лимит попыток чтения записи.");
            }
        }

        void pBios_eSaveCompleteReadEpromLine(object sender, EventArgs e)
        {
            lock (obj)
            {
                Invoke(incer, 1);
            }
        }

        /// <summary>
        /// Процедура сохранения данных в устройство
        /// </summary>
        void WriteCFG()
        {
            try
            {
                pBios.SaveToEprom(pObject);
                pBios.SaveToDevice(pObject);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка во время записи конфигурации",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SaveForm_Shown(object sender, EventArgs e)
        {
            Invoke(initer, 0, 64);
            maker = new Maker(WriteCFG);
            async = maker.BeginInvoke(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (async.IsCompleted)
            {
                timer1.Stop();
                lock (obj)
                {
                    Close();
                }
            }
        }

        private void SaveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pBios.eSaveCompleteReadEpromLine -= pBios_eSaveCompleteReadEpromLine;
            pBios.eSaveMorePopitReadEpromLine -= pBios_eSaveMorePopitReadEpromLine;
            pBios.eSaveTimeoutReadEpromLine -= pBios_eSaveTimeoutReadEpromLine;
        }
    }
}