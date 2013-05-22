using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Platform;
using Calibration.CalibrationPlugin.IO;

namespace Calibration.CalibrationPlugin.GUI
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

        private BIOS pBios = null;
        private ObjectCurrentState pObject = null;

        public SaveForm(BIOS bios, ObjectCurrentState currentState)
        {
            InitializeComponent();

            incer = new Incer(IncP);
            initer = new Initer(InitProgressBar);

            DialogResult = DialogResult.OK;

            pBios = bios;
            pObject = currentState;

            pBios.eSaveCompleteReadEpromLine += new EventHandler(pBios_eSaveCompleteReadEpromLine);
            pBios.eSaveMorePopitReadEpromLine += new EventHandler(pBios_eSaveMorePopitReadEpromLine);
            pBios.eSaveTimeoutReadEpromLine += new EventHandler(pBios_eSaveTimeoutReadEpromLine);

            mes = new Messeger(ShowMessage);
        }

        private void ShowMessage(string text)
        {
            MessageBox.Show(this, "Не удалось прочитать конфигурацию устройства." +
                text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void pBios_eSaveTimeoutReadEpromLine(object sender, EventArgs e)
        {            
            {
                this.Invoke(mes, "Устройство не отвечает на запросы");
            }
        }

        void pBios_eSaveMorePopitReadEpromLine(object sender, EventArgs e)
        {
            this.Invoke(mes, "Превышен лимит попыток записи.");
        }

        void pBios_eSaveCompleteReadEpromLine(object sender, EventArgs e)
        {
            Invoke(incer, 1);
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

        void WriteCFG()
        {
            try
            {
                pBios.SaveCalibrationTableToFile(pObject.SelectedCalibrationTable.CalibrationTable, pObject.Eprom);
                pBios.SaveCalibrationTableToDevice(pObject.SelectedCalibrationTable.CalibrationTable, pObject.Eprom);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка во время записи конфигурации",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SaveForm_Shown(object sender, EventArgs e)
        {
            Invoke(initer, 0, 3);
            maker = new Maker(WriteCFG);
            async = maker.BeginInvoke(null, null);
        }

        void bios_eComplete(object sender, EventArgs e)
        {
            Invoke(incer, 1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (async.IsCompleted)
            {
                timer1.Stop();
                Close();
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