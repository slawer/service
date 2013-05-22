using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Platform;
using Calibration.CalibrationPlugin.IO;

namespace Calibration.CalibrationPlugin.GUI
{
    public partial class LoadeForm : Form
    {
        private BIOS pBios = null;
        private ObjectCurrentState pObject = null;

        Maker maker = null;
        Messeger mes = null;

        Incer incer;
        Initer initer;

        IAsyncResult async = null;
        Object obj = null;

        public StatusLoad status = StatusLoad.Success;

        public LoadeForm(BIOS bios, ObjectCurrentState currentState)
        {
            InitializeComponent();

            pBios = bios;
            pObject = currentState;

            pBios.eCompleteReadEpromLine += new EventHandler(pBios_eCompleteReadEpromLine);
            pBios.eMorePopitReadEpromLine += new EventHandler(pBios_eMorePopitReadEpromLine);
            pBios.eTimeoutReadEpromLine += new EventHandler(pBios_eTimeoutReadEpromLine);

            incer = new Incer(IncP);
            initer = new Initer(InitProgressBar);

            mes = new Messeger(ShowMessage);
            obj = new object();
        }

        private void ShowMessage(string text)
        {
            MessageBox.Show(this, "Не удалось прочитать конфигурацию устройства." + 
                text, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        /// <summary>
        /// Превышено время ожидания ответа от устройства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pBios_eTimeoutReadEpromLine(object sender, EventArgs e)
        {
            lock (obj)
            {
                status = StatusLoad.Timeout;
                this.Invoke(mes, "Устройство не отвечает на запросы");
            }
        }

        /// <summary>
        /// Превышен лимит попыток чтения данных с устройства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pBios_eMorePopitReadEpromLine(object sender, EventArgs e)
        {
            lock (obj)
            {
                status = StatusLoad.MorePopit;
                this.Invoke(mes, "Превышен лимит попыток записи.");
            }
        }

        /// <summary>
        /// операция чтения завершилась успехом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pBios_eCompleteReadEpromLine(object sender, EventArgs e)
        {
            lock (obj)
            {
                this.Invoke(incer, 1);
            }
        }

        /// <summary>
        /// старт загрузки!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadeForm_Shown(object sender, EventArgs e)
        {
            Invoke(initer, 0, 48);
            maker = new Maker(ReadCFG);
            async = maker.BeginInvoke(null, null);
        }

        /// <summary>
        /// Основная процедура чтения данных с устройства
        /// </summary>
        void ReadCFG()
        {
            try
            {
                pObject.Eprom = pBios.LoadEprom();
                if (pObject.Eprom != null)
                {
                    pObject.CalibrationTableHandles = pBios.CreateCalibrationTableHandles(pObject.Eprom);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка во время загрузки конфигурации",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// инициализировать статус
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void InitProgressBar(int min, int max)
        {
            progressBar.Minimum = min;
            progressBar.Maximum = max;

            progressBar.Value = 0;
        }

        /// <summary>
        /// увеличить значение статуса на еденицу
        /// </summary>
        /// <param name="value"></param>
        private void IncP(int value)
        {
            progressBar.Increment(value);
            Application.DoEvents();
        }

        /// <summary>
        /// таймер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (async.IsCompleted)
            {
                timer.Stop();
                lock (obj)
                {
                    DialogResult = DialogResult.Cancel;
                    switch (status)
                    {
                        case StatusLoad.Success: 

                            DialogResult = DialogResult.OK;                            
                            break;
                    }

                    pBios.eCompleteReadEpromLine -= pBios_eCompleteReadEpromLine;
                    pBios.eMorePopitReadEpromLine -= pBios_eMorePopitReadEpromLine;
                    pBios.eTimeoutReadEpromLine -= pBios_eTimeoutReadEpromLine;

                    Close();
                }
            }
        }

        /// <summary>
        /// отмена выполняемой операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, EventArgs e)
        {
            pBios.Cancel();
            status = StatusLoad.Cancel;
        }
    }

    delegate void Maker();
    delegate void Messeger(string text);
    delegate void Initer(int min, int max);
    delegate void Incer(int value);

    public enum StatusLoad
    {
        Success,
        Timeout,
        MorePopit,
        Cancel
    }
}