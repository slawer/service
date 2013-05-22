using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Platform;

namespace BOtimeReset1
{
    /// <summary>
    /// Главная форма модуля управляет синхронизацией времени БО
    /// </summary>
    public partial class Form1 : Form
    {
        IApplication app = null;
        IProtocol protocol = null;
        SetTimeParameters par = null;

        CheckBox[] boxArray = new CheckBox[7];

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="application">Интерфейс связи с платформой</param>
        public Form1(IApplication application)
        {
            app = application;
            protocol = app.GetProtocol(ProtocolVersion.x100);
            try
            {
                par = LoadConfiguration(Application.StartupPath + ParametrConstants.ConfigName);
            }
            catch
            {
                par = new SetTimeParameters();
            }
            


                        
            InitializeComponent();

            this.boxArray[0] = this.checkBox1;
            this.boxArray[1] = this.checkBox2;
            this.boxArray[2] = this.checkBox3;
            this.boxArray[3] = this.checkBox4;
            this.boxArray[4] = this.checkBox5;
            this.boxArray[5] = this.checkBox6;
            this.boxArray[6] = this.checkBox7;

            SetImageForm();
            
        }

        /// <summary>
        /// Сохранить конфигурацию из класса параметров в файл
        /// </summary>
        /// <param name="parameters">Сохраняемые параметры</param>
        /// <param name="uri">Имя файла, в котором сохраняется конфигурация</param>
        private void SaveConfiguration(SetTimeParameters parameters, string uri)
        {
            FileStream stream = null;

            try
            {
                stream = File.Open(uri, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();

                bFormatter.Serialize(stream, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Восстановление конфигурации из файла
        /// </summary>
        /// <param name="uri">Имя файла, в котором сохранена конфигурация</param>
        /// <returns>Класс, порождённый из сохранённых параметров</returns>
        /// <exception cref="FileNotFoundException">Не найден файл с конфигурацией</exception>
        /// <exception cref="">Exception</exception>
        private SetTimeParameters LoadConfiguration(string uri)
        {
            FileStream stream = null;
            
            try
            {
                if (File.Exists(uri))
                {
                    stream = File.Open(uri, FileMode.Open);
                    BinaryFormatter bFormatter = new BinaryFormatter();

                     return (SetTimeParameters)bFormatter.Deserialize(stream);
                }
                else
                    throw new FileNotFoundException(ParametrConstants.ErrorNotConfig);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Настройка внешнего вида формы в соответствии с параметрами конфигурации
        /// </summary>
        private void SetImageForm()
        {
            int top = this.checkBox1.Top;

            for (int j = 0; j < this.boxArray.Length; j++)
            {
                if (par.isValidBO(j+1))
                {
                    this.boxArray[j].Location = new Point(this.boxArray[j].Location.X, top);
                    top = top + ParametrConstants.TopStep;
                    this.boxArray[j].Text = par.getNameBO(j + 1);
                    this.boxArray[j].Checked = par.getFlagBO(j + 1);
                    this.boxArray[j].Visible = true;
                    this.boxArray[j].Enabled = true;
                }
                else
                {
                    this.boxArray[j].Text = ParametrConstants.NoValidBO;
                    this.boxArray[j].Checked = false;
                    this.boxArray[j].Visible = false;
                    this.boxArray[j].Enabled = false;
                }
            }
            if (top < (this.button4.Location.Y + this.button4.Size.Height))
                top = (this.button4.Location.Y + this.button4.Size.Height + ParametrConstants.TopStep2);
            top = top + ParametrConstants.TopStep + this.button3.Size.Height + ParametrConstants.TopStep;

            this.Size = new Size(this.Size.Width, top);
        }

        /// <summary>
        /// Формирование строки синхронизации с последующей отсылкой в сеть устройств
        /// Может сопровождаться отсылкой дополнительной строки рестарта
        /// </summary>
        /// <param name="param">Адрес Блока Отображения</param>
        private void SendString(int param)
        {
            DateTime data = DateTime.Now;

            string packetStr = "";

            if (par.OldFormatCMD)
            {
                packetStr = string.Format(
                    "@JOB#000#{0:X2}1001200AB50000{1:D2}{2:D2}{3:D2}00{4:D2}{5:D2}{6:D2}$",
                    param,
                    data.Second, data.Minute, data.Hour,
                    data.Day, data.Month, data.Year - 2000);
            }
            else
            {
                packetStr = string.Format(
                    "@JOB#000#{0:X2}1101200AB50000{1:D2}{2:D2}{3:D2}00{4:D2}{5:D2}{6:D2}00$",
                    param,
                    data.Second, data.Minute, data.Hour,
                    data.Day, data.Month, data.Year - 2000);
            }

            Packet pak = new Packet(packetStr, DateTime.Now, null);
            app.SendPacket(pak);

            if (par.ResetBOAfterSet) SendResetString(param);
        }

        /// <summary>
        /// Формирование строки рестарт с последующей отсылкой в сеть устройств
        /// </summary>
        /// <param name="param">Адрес Блока Отображения</param>
        private void SendResetString(int param)
        {
            string packetStr = string.Format("@JOB#000#{0:X2}0705100100$", param);

            Packet pak = new Packet(packetStr, DateTime.Now, null);
            app.SendPacket(pak);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (par.isValidBO(1)) checkBox1.Checked = true; else checkBox1.Checked = false;
            if (par.isValidBO(2)) checkBox2.Checked = true; else checkBox2.Checked = false;
            if (par.isValidBO(3)) checkBox3.Checked = true; else checkBox3.Checked = false;
            if (par.isValidBO(4)) checkBox4.Checked = true; else checkBox4.Checked = false;
            if (par.isValidBO(5)) checkBox5.Checked = true; else checkBox5.Checked = false;
            if (par.isValidBO(6)) checkBox6.Checked = true; else checkBox6.Checked = false;
            if (par.isValidBO(7)) checkBox7.Checked = true; else checkBox7.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (par.getAdrBO(1) > 0) 
                {
                    SendString(par.getAdrBO(1));
                }
            };
            if (checkBox2.Checked)
            {
                if (par.getAdrBO(2) > 0) 
                {
                    SendString(par.getAdrBO(2));
                }
            };
            if (checkBox3.Checked)
            {
                if (par.getAdrBO(3) > 0) 
                {
                    SendString(par.getAdrBO(3));
                }
            };
            if (checkBox4.Checked)
            {
                if (par.getAdrBO(4) > 0) 
                {
                    SendString(par.getAdrBO(4));
                }
            };
            if (checkBox5.Checked)
            {
                if (par.getAdrBO(5) > 0) 
                {
                    SendString(par.getAdrBO(5));
                }
            };
            if (checkBox6.Checked)
            {
                if (par.getAdrBO(6) > 0) 
                {
                    SendString(par.getAdrBO(6));
                }
            };
            if (checkBox7.Checked)
            {
                if (par.getAdrBO(7) > 0)
                {
                    SendString(par.getAdrBO(7));
                }
            };

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(par);
            
            f2.ShowDialog();

            if (par.CodeExit)
            {
                SaveParams();
                SetImageForm();
            }
        }

        /// <summary>
        /// Сохранение параметров настройки в класс параметров и в файл конфигурации
        /// </summary>
        private void SaveParams()
        {
            par.setFlagBO(1, checkBox1.Checked);
            par.setFlagBO(2, checkBox2.Checked);
            par.setFlagBO(3, checkBox3.Checked);
            par.setFlagBO(4, checkBox4.Checked);
            par.setFlagBO(5, checkBox5.Checked);
            par.setFlagBO(6, checkBox6.Checked);
            par.setFlagBO(7, checkBox7.Checked);

            try
            {
                SaveConfiguration(par, Application.StartupPath + ParametrConstants.ConfigName);
            }
            catch
            {
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveParams();
        }
    }
}
