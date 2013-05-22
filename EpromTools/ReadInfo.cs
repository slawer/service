using System;
using Platform;

namespace EpromWorking
{
    class ReadInfo
    {
        private Device nDevice = Device.D1;
        private int numberDevice = 1;
        int attemptsToRead = 0; // попыток чтения
        int timeoutForAnswer = 1000; // общее время на получениие ответа
        int timeoutBetweenRead = 50; // интервал между попытками чтения
        int numberOfDataChecks = 0; // количество проверок данных
        bool useBroadcast = false; // использовать широковещательный
        bool usingAnAlgorithmWithDataProtection = false; // использовать алгоритм с защитой данных
        int[] pages = null;
        public int countCommands = 0;

        // ----- свойства -------

        public Device Device 
        { 
            get { return nDevice; }
            set { nDevice = value; }
        }

        public int DeviceNumber
        {
            get { return numberDevice; }
            set { numberDevice = value; }
        }

        public int AttemptsToRead
        {
            get { return attemptsToRead; }
            set { attemptsToRead = value; }
        }

        public int TimeoutForAnswer
        {
            get { return timeoutForAnswer; }
            set { timeoutForAnswer = value; }
        }

        public int TimeoutBetweenRead
        {
            get { return timeoutBetweenRead; }
            set { timeoutBetweenRead = value; }
        }

        public int NumberOfDataChecks
        {
            get { return numberOfDataChecks; }
            set { numberOfDataChecks = value; }
        }

        public bool UseBroadcast
        {
            get { return useBroadcast; }
            set { useBroadcast = value; }
        }

        public bool UsingAnAlgorithmWithDataProtection
        {
            get { return usingAnAlgorithmWithDataProtection; }
            set { usingAnAlgorithmWithDataProtection = value; }
        }

        public int[] Pages
        {
            get { return pages; }
        }

        // ----- константы --------

        const int PagesCount = 7;

        // ------- конструктор ---------

        public ReadInfo()
        {
            pages = new int[PagesCount];
            for (int index = 0; index < PagesCount; index++) pages[index] = -1;
        }

        // ----- получить команду защиты данных -----

        public string ProtectionStart
        {
            get { return "09012002A00100"; }
        }

        public string ProtectionEnd
        {
            get { return "0705100100"; }
        }
    }
}