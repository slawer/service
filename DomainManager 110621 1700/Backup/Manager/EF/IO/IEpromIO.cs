using System;
using System.Collections.Generic;

namespace Platform
{
    /// <summary>
    /// 
    /// </summary>
    public enum ResultOperation { Succes, Timeout, MorePopit, Default }

    public interface IEpromIO
    {
        Information Options { get; }
        ResultOperation LastOperation { get; }

        event EventHandler eComplete;

        void Packet(string packet);

        string Read();
        string Write(string Data);

        string Read(int Device, int Page, int Offset, int Lenght);
        string Write(int Device, int Page, int Offset, int Lenght, string Data);

        string FreeQuestion(string Question, string Answer);
        string FreeQuestion(string Question, string Template, string Answer);        

        Eprom GetEprom();
        Eprom GetEprom(int[] Pages);

        void SetEprom(Eprom eprom);
        void SetEprom(Eprom eprom, int[] Pages);
    }
        /// <summary>
    /// Класс описывающий что и как необходимо прочитать/записать. Не выполняется контроль данных!
    /// </summary>
    public class Information
    {
        private int deviceNumber = 0x01;                        // номер устройства
        private int attemptsToReadWritwEntries = 0x01;          // попыток чтения/записи
        private int timeoutForResponseFromDevice = 1000;        // таймаут ожидания ответа от устройства
        private int timeoutBetweenAttemptsToReadWrite = 0;      // таймаут между попытками чтения/записи
        private int numberOfDataChecks = 0x00;                  // количество проверок данных
        private bool useBroadcastAddress = false;               // использовать широковещательный адрес
        private bool useAlgorithmWithDataProtection = false;    // использовать алгоритм с защитой данных

        /// <summary>
        /// Номер устройства
        /// </summary>
        public int Device
        {
            get { return deviceNumber; }
            set { deviceNumber = value; }
        }

        /// <summary>
        /// Попыток чтения/записи
        /// </summary>
        public int AttemptsToReadWriteEntries
        {
            get { return attemptsToReadWritwEntries; }
            set { attemptsToReadWritwEntries = value; }
        }

        /// <summary>
        /// Таймаут ожидания ответа от устройства
        /// </summary>
        public int TimeoutForResponseFromDevice
        {
            get { return timeoutForResponseFromDevice; }
            set { timeoutForResponseFromDevice = value; }
        }

        /// <summary>
        /// Таймаут между попытками чтения/записи
        /// </summary>
        public int TimeoutBetweenAttemptsToReadWrite
        {
            get { return timeoutBetweenAttemptsToReadWrite; }
            set { timeoutBetweenAttemptsToReadWrite = value; }
        }

        /// <summary>
        /// Количество проверок данных
        /// </summary>
        public int NumberOfDataChecks
        {
            get { return numberOfDataChecks; }
            set { numberOfDataChecks = value; }
        }

        /// <summary>
        /// Использовать широковещательный адрес
        /// </summary>
        public bool UseBroadcastAddress
        {
            get { return useBroadcastAddress; }
            set { useBroadcastAddress = value; }
        }

        /// <summary>
        /// Использовать алгоритм с защитой данных
        /// </summary>
        public bool UseAlgorithmWithDataProtection
        {
            get { return useAlgorithmWithDataProtection; }
            set { useAlgorithmWithDataProtection = value; }
        }

        // ------ специфика ---------

        private int pageNumber = 0x01;      // номер страницы
        private int offsetInPage = 0x00;    // смещение на странице
        private int lenghtOfData = 0x00;    // длинна данных

        /// <summary>
        /// Страница
        /// </summary>
        public int Page
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }

        /// <summary>
        /// Смещение на странице
        /// </summary>
        public int Offset
        {
            get { return offsetInPage; }
            set { offsetInPage = value; }
        }

        /// <summary>
        /// Длинна данных
        /// </summary>
        public int Lenght
        {
            get { return lenghtOfData; }
            set { lenghtOfData = value; }
        }

        /// <summary>
        /// тело команды на разрешение записи начало "09012002A00100"
        /// </summary>        
        public string ProtectionStart
        {
            get { return "09012002A00100"; }
        }

        /// <summary>
        /// тело команды на разрешение записи конец "0705100100"
        /// </summary>
        public string ProtectionEnd
        {
            get { return "0705100100"; }
        }
    }
}