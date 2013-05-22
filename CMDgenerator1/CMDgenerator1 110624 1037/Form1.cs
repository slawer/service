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

namespace CMDgenerator1
{
    /// <summary>
    /// Главная форма модуля управляет синхронизацией времени БО
    /// </summary>
    public partial class Form1 : Form
    {
        IApplication app = null;
        IProtocol protocol = null;
        SetTimeParameters par = null;

        Button[] boxArray = new Button[7];

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

            this.boxArray[0] = this.button1;
            this.boxArray[1] = this.button2;
            this.boxArray[2] = this.button5;
            this.boxArray[3] = this.button6;
            this.boxArray[4] = this.button7;
            this.boxArray[5] = this.button8;
            this.boxArray[6] = this.button9;

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
            int top = this.button1.Top;

            for (int j = 0; j < this.boxArray.Length; j++)
            {
                if (par.isValidBO(j+1))
                {
                    this.boxArray[j].Location = new Point(this.boxArray[j].Location.X, top);
                    top = top + ParametrConstants.TopStep;
                    this.boxArray[j].Text = par.getNameBO(j + 1);
                    this.boxArray[j].Visible = true;
                    this.boxArray[j].Enabled = true;
                }
                else
                {
                    this.boxArray[j].Text = ParametrConstants.NoValidBO;
                    this.boxArray[j].Visible = false;
                    this.boxArray[j].Enabled = false;
                }
            }
            top = top + ParametrConstants.TopStep2;

            this.Size = new Size(this.Size.Width, top);
        }

        /// <summary>
        /// Формирование строки синхронизации с последующей отсылкой в сеть устройств
        /// Может сопровождаться отсылкой дополнительной строки рестарта
        /// </summary>
        /// <param name="param">Номер команды</param>
        private void SendString(int param)
        {
            int Addr = par.getAdrBO(param);
            string cmdBody = par.getNameCMD(param);
            int Lngth = (cmdBody.Length / 2) + 4;
            string packetStr = "";
            {
                packetStr = string.Format("@JOB#000#{0:X2}{1:X2}{2:S}00$", Addr, Lngth, cmdBody);
            }

            Packet pak = new Packet(packetStr, DateTime.Now, null);
            app.SendPacket(pak);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            SendString(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           SendString(2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SendString(3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendString(4);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendString(5);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SendString(6);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SendString(7);
        }
    }
}
