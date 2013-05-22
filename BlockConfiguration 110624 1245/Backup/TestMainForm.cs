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
using BlockConfiguration.GUI;

namespace BlockConfiguration
{
    public partial class TestMainForm : Form
    {
        private HandleIO handle = null;
        private BlockConfigurationIO bios = null;

        public TestMainForm(IApplication application)
        {
            InitializeComponent();

            handle = new HandleIO();
            bios = new BlockConfigurationIO(application, application.GetEpromRW());
        }

        public void Packet(string packet)
        {
            bios.Packet(packet);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadForm frm = new LoadForm(bios, handle);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                ShowInTable();
            }
            else
                MessageBox.Show("NO");
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
        }

        private string ToHex(object arg)
        {
            return string.Format("{0:X2}", arg);
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

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (handle.VisionBlock == null)
            {
                MessageBox.Show(this, "Конфигурация блока отображения не загруженна",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OptionsOfBlockForm frm = new OptionsOfBlockForm();
            frm.Block = handle.VisionBlock.Clone();

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                handle.VisionBlock = frm.Block.Clone();
            }
        }
    }
}