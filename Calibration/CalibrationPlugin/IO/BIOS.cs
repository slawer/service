using System;
using System.Threading;
using System.Globalization;

using Platform;
using Calibration.CalibrationPlugin;

namespace Calibration.CalibrationPlugin.IO
{
    /// <summary>
    /// Выполняет операции чтения записи данных
    /// </summary>
    public class BIOS
    {     
        /// <summary>
        /// Номер страницы на которой находиться таблица описателей калибровочных параметров
        /// </summary>
        private const int calibrationTableHandlesPageNumber = 0x06;

        /// <summary>
        /// Смещение по которому распологается начало таблицы описателей калибровочный параметров
        /// </summary>
        private const int calibrationTableHandlesBaseOffset = 0xb4;

        /// <summary>
        /// Размер таблицы калибровки
        /// </summary>
        private const int calibrationTableCountLines = 0x03;

        /// <summary>
        /// Размер строки таблицы калибровки
        /// </summary>
        private const int calibrationLineByteCount = 16;
        // данные класса
        
        private IEpromIO platformIO = null;             // выполняет чтение/запись в eprom устройства
        private IApplication platform = null;           // платфома
                
        private Object sync = null;
        private bool toBreak = false;

        // события
        
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


        public BIOS(IApplication platformApp, IEpromIO platformBIOS)
        {
            platform = platformApp;
            platformIO = platformBIOS;

            sync = new object();
        }

        /// <summary>
        /// Настройки подсистемы ввода/вывода
        /// </summary>
        public Information BIOS_Options
        {
            get { return platformIO.Options; }
        }

        /// <summary>
        /// Выполняет загрузку Eprom устройства
        /// </summary>
        /// <returns>Загруженный Eprom устройства</returns>
        public Eprom LoadEprom()
        {
            int[] pages = { 4, 5, 7 };
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
        /// Выполняет загрузку Eprom с файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="format">Формат в котором сохранен Eprom</param>
        /// <returns></returns>
        public Eprom LoadEpromFromFile(string filePath, FileFormat format)
        {
            IEFLoader ldr = null;
            Eprom eprom = new Eprom();

            switch (format)
            {
                case FileFormat.EF1TXT:

                    ldr = platform.GetEFLoader(FileFormat.EF1TXT);
                    break;

                case FileFormat.EF2XMLOLD:

                    ldr = platform.GetEFLoader(FileFormat.EF2XMLOLD);
                    break;

                case FileFormat.EF2XML:

                    ldr = platform.GetEFLoader(FileFormat.EF2XML);
                    break;
            }
            eprom = ldr.Load(filePath);
            return eprom;
        }

        /// <summary>
        /// Сохраняет тблицу калибровки в файл
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="table">Таблица калибровки, которую необходимо сохранить</param>
        /// <param name="eprom">Eprom в который необходимо сохранить данную таблицу калибровки</param>
        public void SaveEpromToFile(string filePath, CalibrationTable table, Eprom eprom)
        {
            IEFSaver svr = null;
            svr = platform.GetEFSaver(FileFormat.EF2XML);

            SaveCalibrationTableToFile(table, eprom);
            svr.Save(filePath, eprom);
        }

        /// <summary>
        /// Сохраняет таблицу калибровки в указанный eprom
        /// </summary>
        /// <param name="table">Таблица калибровки, которую необходимо сохранить</param>
        /// <param name="eprom">Eprom в который необходимо сохранить таблицу калибровки</param>
        public void SaveCalibrationTableToFile(CalibrationTable table, Eprom eprom)
        {
            int[] Indices = { 0x0400, 0x0430, 0x0460, 0x0490, 0x04C0, 0x04F0, 0x0520, 0x0550 };
            foreach (int index in Indices)
            {
                if (eprom.GetByte(index) == table.Name)
                {
                    byte size = (byte)(table.Parameters.Count * 4);
                    eprom.SetByte(index + 3, size);

                    int offset = index + 4;
                    foreach (Parameter parameter in table.Parameters)
                    {
                        eprom.SetByte(offset++, (byte)(parameter.Physical >> 8));
                        eprom.SetByte(offset++, (byte)parameter.Physical);

                        eprom.SetByte(offset++, (byte)(parameter.Calibrated >> 8));
                        eprom.SetByte(offset++, (byte)parameter.Calibrated);
                    }
                    if (table.Parameters.Count < 11)
                    {
                        eprom.SetByte(offset++, 0xff);
                        eprom.SetByte(offset++, 0xff);

                        eprom.SetByte(offset++, 0xff);
                        eprom.SetByte(offset++, 0xff);
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Сохранить таблицу калибровки в файл
        /// </summary>
        /// <param name="table">Таблица калибровки, которую необходимо сохранить</param>
        /// <param name="eprom">Виртуальный eprom устройства</param>
        public void SaveCalibrationTableToDevice(CalibrationTable table, Eprom eprom)
        {
            try
            {
                int[] Indices = { 0x0400, 0x0430, 0x0460, 0x0490, 0x04C0, 0x04F0, 0x0520, 0x0550 };
                foreach (int index in Indices)
                {
                    if (eprom.GetByte(index) == table.Name)
                    {
                        string protectStart = "@JOB#000#" + string.Format("{0:X2}", platformIO.Options.Device) + 
                            platformIO.Options.ProtectionStart + "$";
                        platform.SendPacket(new Packet(protectStart, DateTime.Now, null));
                        
                        for (int line = 0; line < calibrationTableCountLines; line++)
                        {
                            int offset = index + (line * 16);

                            int pageNumber = (int)(offset / 256);
                            int offsetPage = (int)(offset % 256);

                            string data = string.Empty;
                            for (int byteIndex = 0; byteIndex < calibrationLineByteCount; byteIndex++)
                            {
                                data += string.Format("{0:X2}", eprom.GetByte(offset++));
                            }

                            platformIO.Write(platformIO.Options.Device, pageNumber, offsetPage, 16, data);
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
            }
            finally
            {
                string protectEnd = "@JOB#000#" + string.Format("{0:X2}", platformIO.Options.Device) + 
                    platformIO.Options.ProtectionEnd + "$";
                platform.SendPacket(new Packet(protectEnd, DateTime.Now, null));
            }
        }

        /// <summary>
        /// Получить массив описателей калибровочных параметров
        /// </summary>
        /// <param name="eprom">EPROM устройства из которого необходимо извлеч данные</param>
        /// <returns>Массив описателй калибровочных параметров</returns>
        public CalibrationTableHandle[] CreateCalibrationTableHandles(Eprom eprom)
        {
            if (eprom != null)
            {
                CalibrationTableHandle[] handles = new CalibrationTableHandle[20];
                for (int index = 0; index < handles.Length; index++)
                {
                    int baseOffset = calibrationTableHandlesBaseOffset + (index * CalibrationTableHandle.SizeInTable);

                    byte name = eprom[calibrationTableHandlesPageNumber][baseOffset];
                    byte offset = eprom[calibrationTableHandlesPageNumber][baseOffset + 1];

                    handles[index] = new CalibrationTableHandle(name, offset);
                }
                return handles;
            }
            else
                throw new ArgumentNullException("eprom", "Не может принимать значение null");
        }

        /// <summary>
        /// Возвращяет таблицу калибровки
        /// </summary>
        /// <param name="eprom">EPROM устройства из которого необходимо извлеч данные</param>
        /// <param name="CalibrationParameterName">Имя калибровочного параметра</param>
        /// <returns>Таблица калибровки</returns>
        public static LastError CreateCalibrationTable(Eprom eprom, CalibrationTableHandle calibrationHandle)
        {
            int[] Indices = { 0x0400, 0x0430, 0x0460, 0x0490, 0x04C0, 0x04F0, 0x0520, 0x0550 };
            foreach (int index in Indices)
            {
                if (eprom.GetByte(index) == calibrationHandle.Name)
                {
                    byte size = eprom.GetByte(index + 3);
                    CalibrationTable table = new CalibrationTable(calibrationHandle.Name, size);

                    int offset = index + 4;         // смещение по которому начинаются калибровочные значения
                    if ((size % 4) != 0) return LastError.Error;

                    for (int i = 0; i < size / 4; i++)
                    {
                        byte[] physical = new byte[2];
                        byte[] calibrated = new byte[2];

                        physical[1] = eprom.GetByte(offset++);
                        physical[0] = eprom.GetByte(offset++);

                        calibrated[1] = eprom.GetByte(offset++);
                        calibrated[0] = eprom.GetByte(offset++);

                        Parameter param = new Parameter();

                        param.Physical = (ushort)BitConverter.ToInt16(physical, 0);
                        param.Calibrated = (ushort)BitConverter.ToInt16(calibrated, 0);

                        table.Parameters.Add(param);
                    }
                    calibrationHandle.CalibrationTable = table;
                    return LastError.Success;
                }
            }
            return LastError.Error;
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

        /// <summary>
        /// Возвращяет таблицу калибровки
        /// </summary>
        /// <param name="eprom">EPROM устройства из которого необходимо извлеч данные</param>
        /// <param name="CalibrationParameterName">Имя калибровочного параметра</param>
        /// <returns>Таблица калибровки</returns>
        public LastError GetCalibrationTable(Eprom eprom, CalibrationTableHandle calibrationHandle)
        {
            int[] Indices = { 0x0400, 0x0430, 0x0460, 0x0490, 0x04C0, 0x04F0, 0x0520, 0x0550 };
            foreach (int index in Indices)
            {
                if (eprom.GetByte(index) == calibrationHandle.Name)
                {
                    byte size = eprom.GetByte(index + 3);
                    CalibrationTable table = new CalibrationTable(calibrationHandle.Name, size);

                    int offset = index + 4;         // смещение по которому начинаются калибровочные значения
                    if ((size % 4) != 0) return LastError.Error;

                    for (int i = 0; i < size / 4; i++)
                    {
                        byte[] physical = new byte[2];
                        byte[] calibrated = new byte[2];

                        physical[1] = eprom.GetByte(offset++);
                        physical[0] = eprom.GetByte(offset++);

                        calibrated[1] = eprom.GetByte(offset++);
                        calibrated[0] = eprom.GetByte(offset++);

                        Parameter param = new Parameter();

                        param.Physical = (ushort)BitConverter.ToInt16(physical, 0);
                        param.Calibrated = (ushort)BitConverter.ToInt16(calibrated, 0);

                        table.Parameters.Add(param);
                    }
                    calibrationHandle.CalibrationTable = table;
                    return LastError.Success;
                }
            }
            return LastError.Error;
        }
    }
}