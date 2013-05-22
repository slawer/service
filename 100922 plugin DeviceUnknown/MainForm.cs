using System;
using System.Globalization;
using System.Windows.Forms;

using Platform;
using SoftwareDevelopmentKit.Services.Types;

namespace DeviceUnknown
{
    public partial class MainForm : Form
    {
        // ---- данные класса ----

        private Binder binder = null;

        private IService reader = null;
        private IApplication app = null;        // ссылка на платформу

        public MainForm(IApplication application)
        {            
            app = application;
            binder = new Binder(app);

            InitializeComponent();

            r_inserter = new ReadInserte(ReadInsert);
            w_inserter = new ReadInserte(WriteInsert);
        }

        /// <summary>
        /// Получает пакет для обработки
        /// </summary>
        /// <param name="packet">Пакет который необходимо обработать</param>
        public void Packet(Packet packet)
        {
            if (binder != null)
            {
                binder.Packet(packet.packet);
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 32; i++)
            {
                string total = string.Format("{0:X2}", i | 0x80);
                comboBoxAnswerNumber.Items.Add(total);
            }

            comboBoxAnswerNumber.SelectedIndex = 0;
            comboBoxAlgorithmWrite.SelectedIndex = 1;
        }

        /// <summary>
        /// Изменили номер требуемого устройства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownNumberForAnswer_ValueChanged(object sender, EventArgs e)
        {
            string total = string.Format("{0:X2}", (int)numericUpDownNumberForAnswer.Value | 0x80);
            foreach (string item in comboBoxAnswerNumber.Items)
            {
                if (item == total)
                {
                    comboBoxAnswerNumber.SelectedItem = item;
                }
            }
        }

        /// <summary>
        /// Прочитать данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void read_Click(object sender, EventArgs e)
        {
            reader = new ReaderWriter();
            
            reader.onExit += new ServiceMessageEventHandler(reader_onExit);

            SaveRWOprions();
            binder.OperationType = OperationType.Read;

            reader.ServiceStart(binder);
            read.Enabled = false;
        }

        /// <summary>
        /// Сохранить указанные на текущий момент настройки чтения/записи
        /// </summary>
        private void SaveRWOprions()
        {
            binder.Options.CountAttemptIo = (int)numericUpDownCountDo.Value;
            binder.Options.CountDataCheck = (int)numericUpDownCountDataCheck.Value;
            binder.Options.DeviceAnswerTimeout = (int)numericUpDownDeviceAnswerTimeout.Value;
            binder.Options.TimeoutBeetwenQuestions = (int)numericUpDownNumberForAnswer.Value;

            if (comboBoxAlgorithmWrite.SelectedIndex == 0)
            {
                binder.Options.Algorithm = false;
            }
            else
                binder.Options.Algorithm = true;

            binder.DeviceNumber = (int)numericUpDownNumberForAnswer.Value;
            binder.AnswerDeviceNumber = binder.DeviceNumber | 0x80;

            binder.AnswerTimeout = (int)numericUpDownAnswerTimeout.Value;            
        }
        
        /// <summary>
        /// Завершено чтение данных с устройства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reader_onExit(object sender, ServiceEventArgs e)
        {
            Invoke(r_inserter);
        }

        /// <summary>
        /// Завершено чтение данных с устройства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void writer_onExit(object sender, ServiceEventArgs e)
        {
            Invoke(w_inserter);
        }

        private ReadInserte r_inserter = null;
        private ReadInserte w_inserter = null;

        delegate void ReadInserte();

        /// <summary>
        /// Вывести прочитанные данные на форму
        /// </summary>
        private void ReadInsert()
        {
            read.Enabled = true;

            switch (binder.LastOperation)
            {
                case ResultOperation.Succes:

                    string result = binder.ResultString;
                    if (result.Length == 0x20)
                    {
                        textBoxNumberAnswer.Text = result.Substring(16, 2);
                        textBoxNumberAnswering.Text = result.Substring(18, 2);

                        textBoxTimeoutToAnswer.Text = result.Substring(26, 2);
                    }
                    else
                        MessageBox.Show(this, "Данные пришедшие от устройства не коррекны!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;

                default:

                    MessageBox.Show(this, "Не удалось прочитать данные", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }

        /// <summary>
        /// Вывести результат записи на форму
        /// </summary>
        private void WriteInsert()
        {
            write.Enabled = true;

            switch (binder.LastOperation)
            {
                case ResultOperation.Succes:

                    string result = binder.ResultString;
                    if (result.Length == 0x20)
                    {
                        textBoxNumberAnswer.Text = result.Substring(16, 2);
                        textBoxNumberAnswering.Text = result.Substring(18, 2);

                        textBoxTimeoutToAnswer.Text = result.Substring(26, 2);
                    }
                    else
                        MessageBox.Show(this, "Данные пришедшие от устройства не коррекны!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;

                default:

                    MessageBox.Show(this, "Не удалось записать данные", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }

        /// <summary>
        /// Записываем данные в устройсво
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void write_Click(object sender, EventArgs e)
        {
            reader = new ReaderWriter();

            reader.onExit += new ServiceMessageEventHandler(writer_onExit);

            SaveRWOprions();
            binder.OperationType = OperationType.Write;

            reader.ServiceStart(binder);
            write.Enabled = false;
        }
    }
}