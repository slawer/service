using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BOtimeReset1
{
    /// <summary>
    /// Форма настройки параметров блоков отображения
    /// </summary>
    public partial class Form2 : Form
    {
        SetTimeParameters par = null;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parameters">Параметры настройки программы</param>
        public Form2(SetTimeParameters parameters)
        {
            int j; 

            InitializeComponent();
            par = parameters;
            j = par.getAdrBO(1);
            textBox1.Text = j.ToString();
            j = par.getAdrBO(2);
            textBox2.Text = j.ToString();
            j = par.getAdrBO(3);
            textBox3.Text = j.ToString();
            j = par.getAdrBO(4);
            textBox4.Text = j.ToString();
            j = par.getAdrBO(5);
            textBox5.Text = j.ToString();
            j = par.getAdrBO(6);
            textBox6.Text = j.ToString();
            j = par.getAdrBO(7);
            textBox7.Text = j.ToString();

            textBox8.Text = par.getNameBO(1);
            textBox9.Text = par.getNameBO(2);
            textBox10.Text = par.getNameBO(3);
            textBox11.Text = par.getNameBO(4);
            textBox12.Text = par.getNameBO(5);
            textBox13.Text = par.getNameBO(6);
            textBox14.Text = par.getNameBO(7);

            checkBox1.Checked = par.OldFormatCMD;
            checkBox2.Checked = par.ResetBOAfterSet;

            par.CodeExit = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j1, j2, j3, j4, j5, j6, j7;

            int jNum = 1;

            try
            {
                j1 = int.Parse(textBox1.Text); jNum++;
                j2 = int.Parse(textBox2.Text); jNum++;
                j3 = int.Parse(textBox3.Text); jNum++;
                j4 = int.Parse(textBox4.Text); jNum++;
                j5 = int.Parse(textBox5.Text); jNum++;
                j6 = int.Parse(textBox6.Text); jNum++;
                j7 = int.Parse(textBox7.Text);
            }

            catch
            {
                string cErr = string.Format("Не верно задан адрес Устройства в команде № {0:D}", jNum);
                MessageBox.Show(cErr);
                return;
            };

            par.setAdrBO(1, j1);par.setNameBO(1, textBox8.Text);
            par.setAdrBO(2, j2);par.setNameBO(2, textBox9.Text);
            par.setAdrBO(3, j3);par.setNameBO(3, textBox10.Text);
            par.setAdrBO(4, j4);par.setNameBO(4, textBox11.Text);
            par.setAdrBO(5, j5);par.setNameBO(5, textBox12.Text);
            par.setAdrBO(6, j6);par.setNameBO(6, textBox13.Text);
            par.setAdrBO(7, j7);par.setNameBO(7, textBox14.Text);
                
            par.OldFormatCMD = checkBox1.Checked;
            par.ResetBOAfterSet = checkBox2.Checked;

            par.CodeExit = true;

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            par.CodeExit = false;
            Close();
        }
    }
}
