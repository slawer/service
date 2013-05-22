using System;
using System.Threading;
using System.Globalization;

using Platform;

namespace BlockConfiguration.IO
{
    public class BlockConfigurationIO
    {
        // ---- данные класса ----

        private IEpromIO platformIO = null;             // выполняет чтение/запись в eprom устройства
        private IApplication platform = null;           // платфома
        
        // ---- синхронизация ----
        
        private Object sync = null;
        private bool toBreak = false;

        /// <summary>
        /// Удачно завершилась операция чтения строки Eprom с устройства
        /// </summary>
        public event EventHandler eCompleteReadEpromLine;

        /// <summary>
        /// Таймаут при чтении строки с Eprom устройства
        /// </summary>
        public event EventHandler eTimeoutReadEpromLine;

        /// <summary>
        /// Тпервышен лимит попыток чтения строки с Eprom устройства
        /// </summary>
        public event EventHandler eMorePopitReadEpromLine;


        /// <summary>
        /// Удачно завершилась операция сохранения строки Eprom с устройства
        /// </summary>
        public event EventHandler eSaveCompleteReadEpromLine;

        /// <summary>
        /// Таймаут при сохранения строки с Eprom устройства
        /// </summary>
        public event EventHandler eSaveTimeoutReadEpromLine;

        /// <summary>
        /// Тпервышен лимит попыток сохранения строки с Eprom устройства
        /// </summary>
        public event EventHandler eSaveMorePopitReadEpromLine;

        /// <summary>
        /// Настройки чтения/записи
        /// </summary>
        public Information Options
        {
            get { return platformIO.Options; }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="application">Ссылка на платформу</param>
        /// <param name="epromIO">Ссылка на класс выполняющий, ввод/вывод на уровне платформы</param>
        public BlockConfigurationIO(IApplication application, IEpromIO epromIO)
        {
            platform = application;
            platformIO = epromIO;

            sync = new object();
        }

        /// <summary>
        /// Обработать пакет
        /// </summary>
        /// <param name="packet">Пакет который необходимо обработать</param>
        public void Packet(string packet)
        {
            platformIO.Packet(packet);
        }

        /// <summary>
        /// Отмена операции чтения данных с устройства
        /// </summary>
        public void Cancel()
        {
            lock (sync) toBreak = true;
        }

        public Eprom LoadEprom()
        {
            int[] pages = { 1, 2, 3, 4 };
            Eprom eprom = new Eprom();

            for (int page = 0; page < pages.Length; page++)
            {
                for (int line = 0; line < 16; line++)
                {
                    string data = platformIO.Read(platformIO.Options.Device, pages[page], line * 16, 16);
                    ResultOperation Result = platformIO.LastOperation;

                    switch (Result)
                    {
                        case ResultOperation.Succes:

                            if (data.Length == 32)
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    string ch = data.Substring(i * 2, 2);
                                    eprom[pages[page] - 1][line * 16 + i] = byte.Parse(ch, NumberStyles.AllowHexSpecifier);
                                }
                            }
                            else
                                throw new Exception("Произошла ошибка при передаче данных приложению!");

                            lock (sync) if (eCompleteReadEpromLine != null) eCompleteReadEpromLine(this, new EventArgs());
                            break;

                        case ResultOperation.Timeout:

                            if (eTimeoutReadEpromLine != null) eTimeoutReadEpromLine(this, new EventArgs());
                            return null;

                        case ResultOperation.MorePopit:

                            if (eMorePopitReadEpromLine != null) eMorePopitReadEpromLine(this, new EventArgs());
                            return null;

                        default:

                            return null;
                    }

                    Thread.Sleep(platformIO.Options.TimeoutBetweenAttemptsToReadWrite);
                    lock (sync)
                    {
                        if (toBreak)
                        {
                            toBreak = false;
                            return null;
                        }
                    }
                }
            }
            return eprom;
        }

        /// <summary>
        /// Инициализирует блок отображения
        /// </summary>
        /// <param name="eprom">Eprom устройства из которого необходимо выделить конфигурацию блока отображения</param>
        /// <returns>Блок отображения</returns>
        public Block CreateBlock(Eprom eprom)
        {
            Block block = new Block();
            for (int i = 0; i < 16; i++)
            {
                Indicator indicator = new Indicator();

                indicator.Jack = GetJackFromIndex(i + 1);

                indicator.Con = eprom[1][i * 16 + 0];
                indicator.Address = eprom[1][i * 16 + 1];

                indicator.Offset = eprom[1][i * 16 + 2];
                indicator.OffsetThr.TotalOffset = eprom[1][i * 16 + 3];

                indicator.OffsetPp.TotalOffset = eprom[1][i * 16 + 5];
                indicator.PointPosition = eprom[1][i * 16 + 6];

                switch (eprom[1][i * 16 + 7])
                {
                    case 1: indicator.IndicatorType = IndicatorType.Column32; break;
                    case 2: indicator.IndicatorType = IndicatorType.Column32Bipolar; break;
                    case 3: indicator.IndicatorType = IndicatorType.ThreeDigit; break;
                    case 4: indicator.IndicatorType = IndicatorType.FourDigit; break;
                    case 5: indicator.IndicatorType = IndicatorType.FiveDigit; break;
                    case 6: indicator.IndicatorType = IndicatorType.Clock; break;
                }

                byte[] factArray = new byte[4];
                byte[] CorrectoffsetArray = new byte[4];

                byte[] minArray = new byte[4];
                byte[] maxArray = new byte[4];

                for (int ind = 0; ind < 4; ind++)
                {
                    factArray[ind] = eprom[2][i * 16 + ind];
                    CorrectoffsetArray[ind] = eprom[2][(i * 16) + (ind + 4)];

                    minArray[ind] = eprom[2][(i * 16) + (ind + 8)];
                    maxArray[ind] = eprom[2][(i * 16) + (ind + 12)];
                }

                ReverseBytes(factArray);
                float fact = BitConverter.ToSingle(factArray, 0);

                ReverseBytes(CorrectoffsetArray);
                int correct = BitConverter.ToInt32(CorrectoffsetArray, 0);

                ReverseBytes(minArray);
                int min = BitConverter.ToInt32(minArray, 0);

                ReverseBytes(maxArray);
                int max = BitConverter.ToInt32(maxArray, 0);

                indicator.Fact = fact;
                indicator.CorrectOffset = correct;

                indicator.Thr_MIN = min;
                indicator.Thr_MAX = max;

                block.Indicators.Add(indicator);
            }

            block.Address = eprom[0][0x18];
            block.Speed = eprom[0][0x10];
            block.TypeCRC = eprom[0][0x11];
            block.SpeedOpros = eprom[0][0xe0];
            block.NumbersOfIndicators = eprom[0][0xd0];

            InitBlockCommonOptions(block, eprom);
            return block;
        }

        private IndicatorJack GetJackFromIndex(int index)
        {
            switch (index)
            {
                case 1: return IndicatorJack.XP10_CS1;
                case 2: return IndicatorJack.XP10_CS2;
                case 3: return IndicatorJack.XP11_CS1;
                case 4: return IndicatorJack.XP11_CS2;
                case 5: return IndicatorJack.XP12_CS1;
                case 6: return IndicatorJack.XP12_CS2;
                case 7: return IndicatorJack.XP13_CS1;
                case 8: return IndicatorJack.XP13_CS2;
                case 9: return IndicatorJack.XP14_CS1;
                case 10: return IndicatorJack.XP14_CS2;
                case 11: return IndicatorJack.XP15_CS1;
                case 12: return IndicatorJack.XP15_CS2;
                case 13: return IndicatorJack.XP16_CS1;
                case 14: return IndicatorJack.XP16_CS2;
                case 15: return IndicatorJack.XP17_CS1;
                case 16: return IndicatorJack.XP17_CS2;
                default: return IndicatorJack.Default;
            }
        }

        private void InitBlockCommonOptions(Block block, Eprom eprom)
        {
            for (int i = 0; i < 8; i++)
            {
                CmdOpros cmd = new CmdOpros();

                cmd.Address = eprom[0][0xf0 + i * 2];
                cmd.SizeBuffer = eprom[0][0xf0 + ((i * 2) + 1)];

                block.Cmds.Add(cmd);
            }
        }

        /// <summary>
        /// Вычисляет CRC16 Eprom устройства
        /// </summary>
        /// <param name="eprom">Eprom устройства для которого необходимо вычислить контрольную сумму</param>
        /// <returns>Контрольная сумма</returns>
        public ushort CalculateCRC16(Eprom eprom)
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
            for (int page = 0; page < 3; page++)
            {
                for (int offset = 0; offset < 256; offset++)
                {
                    crc = (ushort)((crc >> 8) ^ Crc16Table[(crc & 0xff) ^ eprom[page][offset]]);
                }
            }
            
            byte[] crc_bytes = BitConverter.GetBytes(crc);

            byte b = crc_bytes[1];
            crc_bytes[1] = crc_bytes[0];
            crc_bytes[1] = b;
            return BitConverter.ToUInt16(crc_bytes, 0);

            //return crc;
        }

        public Version GetVersionOfProgramm()
        {
            string packetVersion = platformIO.FreeQuestion(VersionQuestion, VersionAnswer);

            if (packetVersion != null && packetVersion != string.Empty)
            {
                if (packetVersion.Length == 14)
                {
                    string pre = packetVersion.Substring(0, 4);
                    if (pre.ToUpper() == "B234")
                    {
                        int offset = 4;
                        try
                        {
                            int major = int.Parse(packetVersion.Substring(offset, 2), NumberStyles.AllowHexSpecifier);
                            offset += 2;

                            int minor = int.Parse(packetVersion.Substring(offset, 2), NumberStyles.AllowHexSpecifier);
                            offset += 2;

                            int lastError = int.Parse(packetVersion.Substring(offset, 2), NumberStyles.AllowHexSpecifier);
                            offset += 2;

                            int restart = int.Parse(packetVersion.Substring(offset, 2), NumberStyles.AllowHexSpecifier);
                            Version ver = new Version(major, minor, lastError, restart);

                            return ver;                            
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Строка, команда запроса номера версии ПО блока отображения
        /// </summary>
        private string VersionQuestion
        {
            get
            {
                string totalString = "@JOB#000#" + string.Format("{0:X2}", Options.Device) + "09012002B23400$";
                return totalString;
            }
        }

        /// <summary>
        /// Строка ответа запроса номера версии ПО блока отображения
        /// </summary>
        private string VersionAnswer
        {
            get
            {
                string totalString = "0E012007";
                return totalString;
            }
        }

        /// <summary>
        /// Сохранить конфигурацию устройства в Eprom
        /// </summary>
        /// <param name="handle">Описатель</param>
        public void SaveToEprom(HandleIO handle)
        {
            for (int i = 0; i < 16; i++)
            {
                if (handle.VisionBlock.Indicators[i].IndicatorType != IndicatorType.Default)
                {
                    handle.Eprom[1][i * 16 + 0] = handle.VisionBlock.Indicators[i].Con;
                    handle.Eprom[1][i * 16 + 1] = handle.VisionBlock.Indicators[i].Address;

                    handle.Eprom[1][i * 16 + 2] = handle.VisionBlock.Indicators[i].Offset;
                    handle.Eprom[1][i * 16 + 3] = handle.VisionBlock.Indicators[i].OffsetThr.TotalOffset;

                    handle.Eprom[1][i * 16 + 5] = handle.VisionBlock.Indicators[i].OffsetPp.TotalOffset;
                    handle.Eprom[1][i * 16 + 6] = handle.VisionBlock.Indicators[i].PointPosition;
                }

                switch (handle.VisionBlock.Indicators[i].IndicatorType)
                {
                    case IndicatorType.Column32: handle.Eprom[1][i * 16 + 7] = 1; break;
                    case IndicatorType.Column32Bipolar: handle.Eprom[1][i * 16 + 7] = 2; break;
                    case IndicatorType.ThreeDigit: handle.Eprom[1][i * 16 + 7] = 3; break;
                    case IndicatorType.FourDigit: handle.Eprom[1][i * 16 + 7] = 4; break;
                    case IndicatorType.FiveDigit: handle.Eprom[1][i * 16 + 7] = 5; break;
                    case IndicatorType.Clock: handle.Eprom[1][i * 16 + 7] = 6; break;
                }

                byte[] factArray = BitConverter.GetBytes(handle.VisionBlock.Indicators[i].Fact);
                ReverseBytes(factArray);

                byte[] CorrectoffsetArray = BitConverter.GetBytes(handle.VisionBlock.Indicators[i].CorrectOffset);
                ReverseBytes(CorrectoffsetArray);
                
                byte[] minArray = BitConverter.GetBytes(handle.VisionBlock.Indicators[i].Thr_MIN);
                ReverseBytes(minArray);

                byte[] maxArray = BitConverter.GetBytes(handle.VisionBlock.Indicators[i].Thr_MAX);
                ReverseBytes(maxArray);

                for (int ind = 0; ind < 4; ind++)
                {
                    handle.Eprom[2][i * 16 + ind] = factArray[ind];
                    handle.Eprom[2][(i * 16) + (ind + 4)] = CorrectoffsetArray[ind];

                    handle.Eprom[2][(i * 16) + (ind + 8)] = minArray[ind];
                    handle.Eprom[2][(i * 16) + (ind + 12)] = maxArray[ind];
                }

                handle.Eprom[0][0x18] = handle.VisionBlock.Address;
                handle.Eprom[0][0x10] = handle.VisionBlock.Speed;
                handle.Eprom[0][0x11] = handle.VisionBlock.TypeCRC;
                handle.Eprom[0][0xe0] = handle.VisionBlock.SpeedOpros;
                handle.Eprom[0][0xd0] = handle.VisionBlock.NumbersOfIndicators;

                for (int j = 0; j < 8; j++)
                {
                    handle.Eprom[0][0xf0 + j * 2] = handle.VisionBlock.Cmds[j].Address;
                    handle.Eprom[0][0xf0 + ((j * 2) + 1)] = handle.VisionBlock.Cmds[j].SizeBuffer;
                }

                ushort crc16 = CalculateCRC16(handle.Eprom);

                handle.Eprom[3][0] = (byte)crc16;
                handle.Eprom[3][1] = (byte)(crc16 >> 8);
            }
        }

        private static byte[] ReverseBytes(byte[] inArray)
        {
            byte temp;
            int highCtr = inArray.Length - 1;

            for (int ctr = 0; ctr < inArray.Length / 2; ctr++)
            {
                temp = inArray[ctr];
                inArray[ctr] = inArray[highCtr];
                inArray[highCtr] = temp;
                highCtr -= 1;
            }
            return inArray;
        }

        public void SaveToFile(string filePath, FileFormat format, HandleIO handle)
        {
            IEFSaver svr = null;
            svr = platform.GetEFSaver(FileFormat.EF2XML);

            SaveToEprom(handle);
            svr.Save(filePath, handle.Eprom);
        }

        public void SaveToDevice(HandleIO pObject)
        {
            try
            {
                string protectStart = "@JOB#000#" + string.Format("{0:X2}", platformIO.Options.Device) +
                    platformIO.Options.ProtectionStart + "$";
                platform.SendPacket(new Packet(protectStart, DateTime.Now, null));

                int[] pages = { 0, 1, 2, 3 };

                foreach (int page in pages)
                {
                    for (int line = 0; line < 16; line++)
                    {

                        string data = pObject.Eprom[page].Lines[line].ToString();

                        platformIO.Write(platformIO.Options.Device, page + 1, line * 16, 16, data);
                        switch (platformIO.LastOperation)
                        {
                            case ResultOperation.Succes:

                                if (eSaveCompleteReadEpromLine != null) eSaveCompleteReadEpromLine(this, new EventArgs());
                                break;

                            case ResultOperation.Timeout:

                                if (eSaveTimeoutReadEpromLine != null) eSaveTimeoutReadEpromLine(this, new EventArgs());
                                return;

                            case ResultOperation.MorePopit:

                                if (eSaveMorePopitReadEpromLine != null) eSaveMorePopitReadEpromLine(this, new EventArgs());
                                return;

                            default:

                                return;
                        }
                    }
                }
            }
            finally
            {
                string protectEnd = "@JOB#000#" + string.Format("{0:X2}", platformIO.Options.Device) +
                    platformIO.Options.ProtectionEnd + "$";
                platform.SendPacket(new Packet(protectEnd, DateTime.Now, null));
            }
        }

        public void LoadFromFile(string filePath, HandleIO handle, FileFormat format)
        {
            IEFLoader ldr = platform.GetEFLoader(format);
            Eprom eprom = new Eprom();
            
            eprom = ldr.Load(filePath);
            if (eprom != null)
            {
                handle.Eprom = eprom;
                handle.VisionBlock = CreateBlock(eprom);

                if (handle.VisionBlock != null)
                {
                    handle.CRC16 = CalculateCRC16(eprom);
                    handle.ProgrammVersion = new Version(0, 0, 0, 0);
                }
            }
        }

        public void LoadDefault(HandleIO handle)
        {
            Eprom eprom = new Eprom(0x00);

            for (int by = 0; by < 256; by++)
            {
                eprom[0][by] = 0xff;
            }
            handle.Eprom = eprom;
            handle.VisionBlock = CreateBlock(eprom);

            handle.ProgrammVersion = new Version(0,0,0,0);
        }

        public void SetCurrentTime()
        {
            DateTime now = DateTime.Now;
            byte num = byte.Parse(now.Year.ToString().Substring(2, 2));
            
            string str = "@JOB#000#" + string.Format("{0:X2}", this.platformIO.Options.Device) + "1101200AB50000" + 
                string.Format("{0:D2}", now.Second) + string.Format("{0:D2}", now.Minute) + string.Format("{0:D2}", now.Hour) + "00" + string.Format("{0:D2}", now.Day) + 
                string.Format("{0:D2}", now.Month) + string.Format("{0:D2}", num) + "00$";
            
            this.platform.SendPacket(new Platform.Packet(str, now, null));

            string str2 = "@JOB#000#" + string.Format("{0:X2}", this.platformIO.Options.Device) + this.platformIO.Options.ProtectionEnd + "$";
            
            this.platform.SendPacket(new Platform.Packet(str2, DateTime.Now, null));
        }
    }
}