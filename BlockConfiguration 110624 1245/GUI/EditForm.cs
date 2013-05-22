using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using BlockConfiguration.IO;

namespace BlockConfiguration.GUI
{
    public partial class EditForm : Form
    {
        Indicator indicator = null;

        public EditForm()
        {
            InitializeComponent();            
        }

        public Indicator Indicator
        {
            get { return indicator; }
            set
            {
                indicator = value;
                InsertToForm();                
            }
        }

        /// <summary>
        /// добавить индикатор на форму
        /// </summary>
        private void InsertToForm()
        {
            comboBoxNetAddress.Text = string.Format("{0:X2}", indicator.Address);
            comboBoxOffDat.Text = string.Format("{0:X2}", indicator.Offset);

            comboBoxPntPos.Text = indicator.PointPosition.ToString();

            switch (indicator.IndicatorType)
            {
                case IndicatorType.Column32: comboBoxType.SelectedIndex = 0; break;
                case IndicatorType.Column32Bipolar: comboBoxType.SelectedIndex = 1; break;
                case IndicatorType.ThreeDigit: comboBoxType.SelectedIndex = 2; break;
                case IndicatorType.FourDigit: comboBoxType.SelectedIndex = 3; break;
                case IndicatorType.FiveDigit: comboBoxType.SelectedIndex = 4; break;
                case IndicatorType.Clock: comboBoxType.SelectedIndex = 5; break;
                default:comboBoxType.SelectedIndex = 6; break;
            }

            comboBoxOffThrByte.Text = indicator.OffsetThr.OffsetOfByte.ToString();
            comboBoxOffThrBits.Text = indicator.OffsetThr.OffsetOfBits.ToString();

            comboBoxOffPpByte.Text = indicator.OffsetPp.OffsetOfByte.ToString();
            comboBoxOffPpBits.Text = indicator.OffsetPp.OffsetOfBits.ToString();

            textBoxFact.Text = indicator.Fact.ToString();
            textBoxOffset.Text = indicator.CorrectOffset.ToString();

            textBoxMax.Text = indicator.Thr_MAX.ToString();
            textBoxMin.Text = indicator.Thr_MIN.ToString();

            checkBoxBlink.Checked = ((indicator.Con & 2) > 1) ? true : false;
            checkBoxHide.Checked = ((indicator.Con & 4) > 1) ? true : false;
            checkBoxDWORD.Checked = ((indicator.Con & 0x80) > 1) ? true : false;
        }

        /// <summary>
        /// Нажата кнопка принять изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accept_Click(object sender, EventArgs e)
        {
            try
            {
                indicator.Fact = Convert.ToSingle(textBoxFact.Text);
                indicator.CorrectOffset = Convert.ToInt32(textBoxOffset.Text);

                indicator.Thr_MAX = Convert.ToInt32(textBoxMax.Text);
                indicator.Thr_MIN = Convert.ToInt32(textBoxMin.Text);

                switch (comboBoxType.Text.ToString())
                {
                    case "Столбик, 32 деления": indicator.IndicatorType = IndicatorType.Column32; break;
                    case "Столбик, 32 деления биполярный": indicator.IndicatorType = IndicatorType.Column32Bipolar; break;
                    case "3-х значный": indicator.IndicatorType = IndicatorType.ThreeDigit; break;
                    case "4-х значный": indicator.IndicatorType = IndicatorType.FourDigit; break;
                    case "5-и значный": indicator.IndicatorType = IndicatorType.FiveDigit; break;
                    case "Часы": indicator.IndicatorType = IndicatorType.Clock; break;
                    default: indicator.IndicatorType = IndicatorType.Default; break;
                }

                indicator.OffsetPp.OffsetOfBits = byte.Parse(comboBoxOffPpBits.Text.ToString());
                indicator.OffsetPp.OffsetOfByte = byte.Parse(comboBoxOffPpByte.Text.ToString());

                indicator.OffsetThr.OffsetOfBits = byte.Parse(comboBoxOffThrBits.Text.ToString());
                indicator.OffsetThr.OffsetOfByte = byte.Parse(comboBoxOffThrByte.Text.ToString());

                indicator.Address = byte.Parse(comboBoxNetAddress.Text.ToString(), NumberStyles.AllowHexSpecifier);
                indicator.Offset = byte.Parse(comboBoxOffDat.Text.ToString(), NumberStyles.AllowHexSpecifier);

                indicator.PointPosition = byte.Parse(comboBoxPntPos.Text.ToString(), NumberStyles.AllowHexSpecifier);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxBlink_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) indicator.Con |= 2;
            else
            {
                indicator.Con &= 0xFD;
            }
        }

        private void checkBoxHide_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                indicator.Con |= 4;
            }
            else
            {
                indicator.Con &= 0xFB;
            }
        }

        private void checkBoxDWORD_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                indicator.Con |= 0x80;
            }
            else
            {
                indicator.Con &= 0x7F;
            }
        }
    }
}