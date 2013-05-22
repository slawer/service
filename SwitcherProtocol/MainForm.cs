using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SwitcherProtocol
{
    public partial class MainForm : Form
    {
        // ---- данные класса ----
                
        private RadioEnum radio = RadioEnum.CheckOne;

        private typeCRC crcType = typeCRC.CycleOneByte;
        private ProcolType protocol = ProcolType.ModBus;

        private AutoResetEvent mevent = null;

        public MainForm()
        {
            mevent = new AutoResetEvent(false);

            InitializeComponent();

            radioButtonSwitchOne.Tag = RadioEnum.CheckOne;
            radioButtonSwitchAll.Tag = RadioEnum.CheckAll;

            comboBoxTypeCrc.SelectedIndex = 1;
            comboBoxProtocols.SelectedIndex = 1;            
        }

        /// <summary>
        /// Переключили радио
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radio = (RadioEnum)((sender as RadioButton).Tag);
                switch (radio)
                {
                    case RadioEnum.CheckOne:

                        numericUpDowndeviceNumber.Enabled = true;
                        break;

                    case RadioEnum.CheckAll:

                        numericUpDowndeviceNumber.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Выбрали протокол обмена на переключение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxProtocols_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProtocols.SelectedIndex == 0)
            {
                protocol = ProcolType.DSN;
                comboBoxTypeCrc.Enabled = false;
            }
            else
            {
                protocol = ProcolType.ModBus;
                comboBoxTypeCrc.Enabled = true;
            }
        }

        /// <summary>
        /// настраиваем порт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            PortOptions options = new PortOptions();

            options.SizeOfReadBuffer = serialPort.ReadBufferSize;
            options.SizeOfWriteBuffer = serialPort.WriteBufferSize;
            options.BaudRate = serialPort.BaudRate;
            options.DataBits = serialPort.DataBits;
            options.Parity = serialPort.Parity;
            options.StopBits = serialPort.StopBits;
            options.ComName = serialPort.PortName;

            if (options.ShowDialog(this) == DialogResult.OK)
            {
                serialPort.ReadBufferSize = options.SizeOfReadBuffer;
                serialPort.WriteBufferSize = options.SizeOfWriteBuffer;
                serialPort.BaudRate = options.BaudRate;
                serialPort.DataBits = options.DataBits;
                serialPort.Parity = options.Parity;
                serialPort.StopBits = options.StopBits;
                serialPort.PortName = options.ComName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();
                    UseWaitCursor = true;

                    listBoxStatusViewer.Items.Clear();
                    listBoxStatusViewer.Items.Add(string.Format("Порт: {0} открыт", serialPort.PortName));

                    switch (radio)
                    {
                        case RadioEnum.CheckOne:

                            SwitchOne((int)numericUpDowndeviceNumber.Value, protocol, crcType);
                            break;

                        case RadioEnum.CheckAll:

                            SwitchAll(protocol, crcType);
                            break;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    UseWaitCursor = false;
                    serialPort.Close();
                    listBoxStatusViewer.Items.Add(string.Format("Порт: {0} закрыт", serialPort.PortName));
                }
            }
            else
                MessageBox.Show(this, "Порт открыт", "Информация", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// переключить одно устройство
        /// </summary>
        /// <param name="deviceNumber">номер устройства</param>
        /// <param name="pType">Тип протокола</param>
        private void SwitchOne(int device, ProcolType pType, typeCRC CrcType)
        {
            switch (pType)
            {
                case ProcolType.DSN:

                    SwitchDeviceToDSN(device);
                    listBoxStatusViewer.SelectedIndex = listBoxStatusViewer.Items.Count - 1;
                    break;

                case ProcolType.ModBus:

                    SwitchDeviceToModbus(device, crcType);
                    listBoxStatusViewer.SelectedIndex = listBoxStatusViewer.Items.Count - 1;
                    break;
            }
        }

        /// <summary>
        /// переключить все устройства
        /// </summary>
        /// <param name="pType">Тип протокола</param>
        private void SwitchAll(ProcolType pType, typeCRC CrcType)
        {
            switch (pType)
            {
                case ProcolType.DSN:

                    for (int device = 1; device < 32; device++)
                    {
                        SwitchDeviceToDSN(device);
                        listBoxStatusViewer.SelectedIndex = listBoxStatusViewer.Items.Count - 1;
                    }
                    break;

                case ProcolType.ModBus:

                    for (int device = 1; device < 32; device++)
                    {
                        SwitchDeviceToModbus(device, CrcType);
                        listBoxStatusViewer.SelectedIndex = listBoxStatusViewer.Items.Count - 1;
                    }

                    break;
            }

        }

        /// <summary>
        /// Ошибка на линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
        }

        /// <summary>
        /// получили данные из порта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            mevent.Set();
        }

        public ushort CalculateCRC16(byte[] Packet, int lenght)
        {
            ushort[] Crc16Table = 
            {
                0x0000, 0xC0C1, 0xC181, 0x0140, 0xC301, 0x03C0, 0x0280, 0xC241,
                0xC601, 0x06C0, 0x0780, 0xC741, 0x0500, 0xC5C1, 0xC481, 0x0440,
                0xCC01, 0x0CC0, 0x0D80, 0xCD41, 0x0F00, 0xCFC1, 0xCE81, 0x0E40,
                0x0A00, 0xCAC1, 0xCB81, 0x0B40, 0xC901, 0x09C0, 0x0880, 0xC841,
                0xD801, 0x18C0, 0x1980, 0xD941, 0x1B00, 0xDBC1, 0xDA81, 0x1A40,
                0x1E00, 0xDEC1, 0xDF81, 0x1F40, 0xDD01, 0x1DC0, 0x1C80, 0xDC41,
                0x1400, 0xD4C1, 0xD581, 0x1540, 0xD701, 0x17C0, 0x1680, 0xD641,
                0xD201, 0x12C0, 0x1380, 0xD341, 0x1100, 0xD1C1, 0xD081, 0x1040,
                0xF001, 0x30C0, 0x3180, 0xF141, 0x3300, 0xF3C1, 0xF281, 0x3240,
                0x3600, 0xF6C1, 0xF781, 0x3740, 0xF501, 0x35C0, 0x3480, 0xF441,
                0x3C00, 0xFCC1, 0xFD81, 0x3D40, 0xFF01, 0x3FC0, 0x3E80, 0xFE41,
                0xFA01, 0x3AC0, 0x3B80, 0xFB41, 0x3900, 0xF9C1, 0xF881, 0x3840,
                0x2800, 0xE8C1, 0xE981, 0x2940, 0xEB01, 0x2BC0, 0x2A80, 0xEA41,
                0xEE01, 0x2EC0, 0x2F80, 0xEF41, 0x2D00, 0xEDC1, 0xEC81, 0x2C40,
                0xE401, 0x24C0, 0x2580, 0xE541, 0x2700, 0xE7C1, 0xE681, 0x2640,
                0x2200, 0xE2C1, 0xE381, 0x2340, 0xE101, 0x21C0, 0x2080, 0xE041,
                0xA001, 0x60C0, 0x6180, 0xA141, 0x6300, 0xA3C1, 0xA281, 0x6240,
                0x6600, 0xA6C1, 0xA781, 0x6740, 0xA501, 0x65C0, 0x6480, 0xA441,
                0x6C00, 0xACC1, 0xAD81, 0x6D40, 0xAF01, 0x6FC0, 0x6E80, 0xAE41,
                0xAA01, 0x6AC0, 0x6B80, 0xAB41, 0x6900, 0xA9C1, 0xA881, 0x6840,
                0x7800, 0xB8C1, 0xB981, 0x7940, 0xBB01, 0x7BC0, 0x7A80, 0xBA41,
                0xBE01, 0x7EC0, 0x7F80, 0xBF41, 0x7D00, 0xBDC1, 0xBC81, 0x7C40,
                0xB401, 0x74C0, 0x7580, 0xB541, 0x7700, 0xB7C1, 0xB681, 0x7640,
                0x7200, 0xB2C1, 0xB381, 0x7340, 0xB101, 0x71C0, 0x7080, 0xB041,
                0x5000, 0x90C1, 0x9181, 0x5140, 0x9301, 0x53C0, 0x5280, 0x9241,
                0x9601, 0x56C0, 0x5780, 0x9741, 0x5500, 0x95C1, 0x9481, 0x5440,
                0x9C01, 0x5CC0, 0x5D80, 0x9D41, 0x5F00, 0x9FC1, 0x9E81, 0x5E40,
                0x5A00, 0x9AC1, 0x9B81, 0x5B40, 0x9901, 0x59C0, 0x5880, 0x9841,
                0x8801, 0x48C0, 0x4980, 0x8941, 0x4B00, 0x8BC1, 0x8A81, 0x4A40,
                0x4E00, 0x8EC1, 0x8F81, 0x4F40, 0x8D01, 0x4DC0, 0x4C80, 0x8C41,
                0x4400, 0x84C1, 0x8581, 0x4540, 0x8701, 0x47C0, 0x4680, 0x8641,
                0x8201, 0x42C0, 0x4380, 0x8341, 0x4100, 0x81C1, 0x8081, 0x4040 
            };

            ushort crc = 0xffff;
            for (int offset = 0; offset < lenght; offset++)
            {
                crc = (ushort)((crc >> 8) ^ Crc16Table[(crc & 0xff) ^ Packet[offset]]);
            }
            return crc;
        }

        /// <summary>
        /// Вычислить циклицескую однобайтную CRC для пакета по протоколу DSN 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public void CalculateCycleOneByte(byte[] buffer)
        {
            ushort total_crc = 0x00;
            for (int index = 1; index < buffer.Length - 1; index++)
            {
                total_crc += buffer[index];
            }
            byte t = (byte)total_crc;
            buffer[buffer.Length - 1] = t;            
        }

        /// <summary>
        /// Вычислить циклицескую двухбайтную CRC для пакета по протоколу DSN 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="buffer"></param>
        public void CalculateCycleTwoByte(byte[] packet)
        {
            ushort total_crc = 0x00;
            for (int index = 1; index < packet.Length - 2; index++)
            {
                total_crc += packet[index];
            }

            byte[] crc = BitConverter.GetBytes(total_crc);

            packet[packet.Length - 2] = crc[1];
            packet[packet.Length - 1] = crc[0];
        }

        /// <summary>
        /// Сформировать пакет по протоколу modbus
        /// </summary>
        /// <param name="device"></param>
        private byte[] CreateModbusProtocol(int device)
        {
            byte[] packet = { 0x00, 0x06, 0xAA, 0xAA, 0x55, 0x55, 0x00, 0x00 };
            packet[0] = (byte)device;

            ushort crc = CalculateCRC16(packet, packet.Length - 2);
            byte[] crc_byte = BitConverter.GetBytes(crc);

            packet[packet.Length - 2] = crc_byte[0];
            packet[packet.Length - 1] = crc_byte[1];

            return packet;
        }

        /// <summary>
        /// Сформировать пакет по протоколу DSN
        /// </summary>
        /// <param name="device">номер устройства</param>
        private byte[] CreateDSNProtocol(int device, typeCRC CrcType)
        {
            byte[] packet = { 0x7E, 0x00, 0x0A, 0x02, 0x20, 0x03, 0xAA, 0x55, 0x55, 0x00, 0x00 };
            packet[1] = (byte)device;

            switch (crcType)
            {
                case typeCRC.CycleOneByte:

                    CalculateCycleOneByte(packet);
                    break;

                case typeCRC.CycleTwoByte:

                    CalculateCycleTwoByte(packet);
                    break;

                case typeCRC.CRC16:
           
                    ushort crc = CalculateCRC16(packet, packet.Length - 2);
                    byte[] crc_byte = BitConverter.GetBytes(crc);

                    packet[packet.Length - 2] = crc_byte[1];
                    packet[packet.Length - 1] = crc_byte[0];

                    break;
            }
            return packet;
        }

        /// <summary>
        /// Сформировать втроковое представление пакета
        /// </summary>
        /// <param name="packet">Пакет</param>
        /// <returns>Строковое представление пакета</returns>
        private string ToStringPacket(byte[] packet)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte item in packet)
            {
                builder.AppendFormat(" [{0:X2}]", item);
            }
            return builder.ToString();
        }

        /// <summary>
        /// переключить устройство на протокол modbus
        /// </summary>
        /// <param name="device">номер устройства</param>
        private void SwitchDeviceToDSN(int device)
        {
            byte[] packet = CreateModbusProtocol(device);
            string toScreenString = ToStringPacket(packet);

            listBoxStatusViewer.Items.Add(string.Format("Отправка пакета:{0}", toScreenString));
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            mevent.Reset();
            serialPort.Write(packet, 0, packet.Length);

            if (mevent.WaitOne(300))
            {
                byte[] buffer = new byte[packet.Length];
                int bytesRead = serialPort.Read(buffer, 0, buffer.Length);

                string answerString = ToStringPacket(buffer);
                listBoxStatusViewer.Items.Add(string.Format("Ответ          :{0}", answerString));
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();

                if (bytesRead == packet.Length)
                {
                    for (int index = 0; index < packet.Length; index++)
                    {
                        if (packet[index] != buffer[index])
                        {
                            // ---- Ошибка ----
                            listBoxStatusViewer.Items.Add(string.Format("Устройство: {0} не удалось переключить на протокол DSN", device));
                            return;
                        }
                    }
                    listBoxStatusViewer.Items.Add(string.Format("Устройство номер {0} успешно переключенно на протокол DSN", device));
                }
                else
                {
                    listBoxStatusViewer.Items.Add(string.Format("Устройство: {0} не удалось переключить на протокол DSN", device));
                }

            }
            else
            {
                listBoxStatusViewer.Items.Add(string.Format("Устройство: {0} не отвечает", device));
            }
        }

        /// <summary>
        /// переключить устройство на протокол DSN
        /// </summary>
        /// <param name="device">номер устройства</param>
        private void SwitchDeviceToModbus(int device, typeCRC CrcType)
        {
            byte[] packet = CreateDSNProtocol(device, crcType);
            string toScreenString = ToStringPacket(packet);

            listBoxStatusViewer.Items.Add(string.Format("Отправка пакета:{0}", toScreenString));
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();
            Application.DoEvents();

            mevent.Reset();
            serialPort.Write(packet, 0, packet.Length);

            if (mevent.WaitOne(300))
            {
                byte[] buffer = new byte[packet.Length];
                int bytesRead = serialPort.Read(buffer, 0, buffer.Length);

                string answerString = ToStringPacket(buffer);
                listBoxStatusViewer.Items.Add(string.Format("Ответ          :{0}", answerString));
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();
                Application.DoEvents();

                if (bytesRead == packet.Length)
                {
                    packet[1] |= 0x80;
                    for (int index = 0; index < packet.Length - 2; index++)
                    {
                        if (packet[index] != buffer[index])
                        {
                            // ---- Ошибка ----
                            listBoxStatusViewer.Items.Add(string.Format("Устройство: {0} не удалось переключить на протокол Modbus", device));
                            return;
                        }
                    }
                    listBoxStatusViewer.Items.Add(string.Format("Устройство номер {0} успешно переключенно на протокол Modbus", device));
                }
                else
                {
                    listBoxStatusViewer.Items.Add(string.Format("Устройство: {0} не удалось переключить на протокол Modbus", device));
                }
            }
            else
            {
                listBoxStatusViewer.Items.Add(string.Format("Устройство: {0} не отвечает", device));
            }

        }

        /// <summary>
        /// переключили тип контрольной суммы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTypeCrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTypeCrc.SelectedIndex == 0) crcType = typeCRC.CRC16;
            else
                if (comboBoxTypeCrc.SelectedIndex == 1) crcType = typeCRC.CycleOneByte;
                else
                    crcType = typeCRC.CycleTwoByte;
        }
    }

    enum RadioEnum
    {
        CheckOne = 1,
        CheckAll = 2
    }

    enum ProcolType
    {
        DSN,
        ModBus
    }

    enum typeCRC
    {
        CRC16,
        CycleOneByte,
        CycleTwoByte
    }
}