using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Platform;
using BlockConfiguration.IO;

namespace BlockConfiguration.GUI
{
    public partial class MainForm : Form
    {
        public event EventHandler TurnOnPackets;
        public event EventHandler TurnOffPackets;

        /// <summary>
        /// номер устройства
        /// </summary>
        public int Device
        {
            get { return bios.Options.Device; }
        }

        private HandleIO handle = null;
        private BlockConfigurationIO bios = null;

        public MainForm(IApplication application)
        {
            InitializeComponent();

            handle = new HandleIO();
            bios = new BlockConfigurationIO(application, application.GetEpromRW());
        }

        /// <summary>
        /// Получает пакет на обработку
        /// </summary>
        /// <param name="packet">Поступивший пакет</param>
        public void Packet(string packet)
        {
            bios.Packet(packet);
        }

        private void loadFromDevice_Click(object sender, EventArgs e)
        {
            try
            {
                LoadForm frm = new LoadForm(bios, handle);
                TurnOnPackets(null, null);
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    ShowInTable();
                }
                /*else
                    MessageBox.Show(this, "Не удалось загрузить конфигурация блока отображения",
                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);*/
            }
            finally
            {
                TurnOffPackets(null, null);
            }
        }

        /// <summary>
        /// Загрузить индикаторы в таблицу
        /// </summary>
        private void ShowInTable()
        {
            listViewIndicators.Items.Clear();
            foreach (Indicator indicator in handle.VisionBlock.Indicators)
            {
                ListViewItem item = new ListViewItem(indicator.Jack.ToString());

                item.Tag = indicator;       // определяем указатель на индикатор

                ListViewItem.ListViewSubItem typeIndicator = new ListViewItem.ListViewSubItem(item,
                    Indicator.GetTypeIndicatorString(indicator.IndicatorType));

                ListViewItem.ListViewSubItem netAddress = new ListViewItem.ListViewSubItem(item, ToHex(indicator.Address));
                ListViewItem.ListViewSubItem offset = new ListViewItem.ListViewSubItem(item, indicator.Offset.ToString());
                ListViewItem.ListViewSubItem ptnPos = new ListViewItem.ListViewSubItem(item, indicator.PointPosition.ToString());
                ListViewItem.ListViewSubItem pntOffset = new ListViewItem.ListViewSubItem(item, indicator.OffsetPp.TotalOffset.ToString());

                item.SubItems.Add(typeIndicator);
                item.SubItems.Add(netAddress);
                item.SubItems.Add(offset);
                item.SubItems.Add(ptnPos);
                item.SubItems.Add(pntOffset);

                listViewIndicators.Items.Add(item);
            }

            versionLabel.Text = string.Format("{0} {1}.{2}", "Версия программы:", handle.ProgrammVersion.Major, handle.ProgrammVersion.Minor);
            separatorLabel.Text = "|";
            statusLabel.Text = string.Format("{0}{1}; {2}{3}", "Код последней ошибки: ", handle.LastError, "Количество аварийных перезапусков: ", handle.CountRestart);
        }

        private string ToHex(object arg)
        {
            return string.Format("{0:X2}", arg);
        }

        /// <summary>
        /// определение общих настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commonOptions_Click(object sender, EventArgs e)
        {
            CommonOptionsForm opt = new CommonOptionsForm();

            opt.numericUpDownDevice.Value = bios.Options.Device;
            opt.numericUpDownDeviceAnswerTimeout.Value = bios.Options.TimeoutForResponseFromDevice;
            opt.numericUpDownTimeoutForAnswer.Value = bios.Options.TimeoutBetweenAttemptsToReadWrite;
            opt.numericUpDownCountDo.Value = bios.Options.AttemptsToReadWriteEntries;
            opt.numericUpDownDataCheck.Value = bios.Options.NumberOfDataChecks;

            if (opt.ShowDialog(this) == DialogResult.OK)
            {
                bios.Options.Device = (int)opt.numericUpDownDevice.Value;
                bios.Options.TimeoutForResponseFromDevice = (int)opt.numericUpDownDeviceAnswerTimeout.Value;
                bios.Options.TimeoutBetweenAttemptsToReadWrite = (int)opt.numericUpDownTimeoutForAnswer.Value;
                bios.Options.AttemptsToReadWriteEntries = (int)opt.numericUpDownCountDo.Value;
                bios.Options.NumberOfDataChecks = (int)opt.numericUpDownDataCheck.Value;
            }
        }

        /// <summary>
        /// Конфигурирования общих параметров блока отображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deviceOptions_Click(object sender, EventArgs e)
        {
            if (handle.VisionBlock == null)
            {
                MessageBox.Show(this, "Конфигурация блока отображения не загруженна",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OptionsOfBlockForm frm = new OptionsOfBlockForm();
            frm.Block = handle.VisionBlock;

            if (frm.ShowDialog(this) == DialogResult.OK)
            {                
            }
        }

        /// <summary>
        /// выйти из плагина
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveToFile_Click(object sender, EventArgs e)
        {
            if (handle.VisionBlock != null || handle.Eprom != null)
            {
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    bios.SaveToFile(saveFileDialog.FileName, FileFormat.EF2XML, handle);
                }
            }
            else
                MessageBox.Show(this, "Не загруженна конфигурация", "Информация", MessageBoxButtons.OK);
        }

        private void listViewIndicators_DoubleClick(object sender, EventArgs e)
        {
            EditForm editFrm = new EditForm();

            int index = listViewIndicators.SelectedIndices[0];
            editFrm.Indicator = handle.VisionBlock.Indicators[index];

            if (editFrm.ShowDialog(this) == DialogResult.OK)
            {
                handle.VisionBlock.Indicators[index] = editFrm.Indicator;
                ShowInTable();

                listViewIndicators.Items[index].Selected = true;
            }
        }

        private void saveToDevice_Click(object sender, EventArgs e)
        {
            try
            {
                SaveForm saver = new SaveForm(bios, handle);

                TurnOnPackets(null, null);
                saver.ShowDialog(this);
            }
            finally
            {
                TurnOffPackets(null, null);
            }
        }

        /// <summary>
        /// Загружаем из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadFromFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileFormat format = FileFormat.EF1TXT;
                switch (openFileDialog.FilterIndex)
                {
                    case 1:

                        format = FileFormat.EF1TXT;
                        break;

                    case 2:

                        format = FileFormat.EF2XMLOLD;
                        break;

                    case 3:

                        format = FileFormat.EF2XML;
                        break;
                }
                bios.LoadFromFile(openFileDialog.FileName, handle, format);
                try
                {
                    ShowInTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        /// <summary>
        /// Загрузка конфигурации по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void поУмолчаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bios.LoadDefault(handle);
            try
            {
                ShowInTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
