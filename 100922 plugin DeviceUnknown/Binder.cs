using System;

using Platform;

namespace DeviceUnknown
{
    public delegate void BinderPacketHandler(string packet);

    /// <summary>
    /// Осуществляет прием и передачу данных от/к службы(е)
    /// </summary>
    public class Binder
    {
        // ---- данные класса ----

        private OperationType o_type = OperationType.Read;  // тип запрашиваемой операции

        private IProtocol proto = null;                 // осуществляет работу с протоколом Dsn
        private IApplication application = null;        // сервисы службы

        public event BinderPacketHandler onPacket;      // генерится когда есть пакет
        private IoOptions options = null;               // настройки чтени/записи

        private int deviceNumber = 0xff;                // номер на который реагировать
        private int answerDeviceNumber = 0xff;          // номер которым реагировать

        private int answerTimeout = 0xff;               // время задержки перед ответом
        private ResultOperation lastOperation;          // результат выполнения чтения

        private string resultData = string.Empty;       // результирующие данные при операции чтения

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Binder(IApplication app)
        {
            application = app;
            proto = application.GetProtocol(ProtocolVersion.x100);

            options = new IoOptions();
        }

        /// <summary>
        /// Определяет номер на который реагирует устройство
        /// </summary>
        public int DeviceNumber
        {
            get { return deviceNumber; }
            set { deviceNumber = value; }
        }

        /// <summary>
        /// Определяет номер которым реагировать устройству
        /// </summary>
        public int AnswerDeviceNumber
        {
            get { return answerDeviceNumber; }
            set { answerDeviceNumber = value; }
        }

        /// <summary>
        /// Определяет результат выполнения операции чтения/записи
        /// </summary>
        public ResultOperation LastOperation
        {
            get { return lastOperation; }
            set { lastOperation = value; }
        }

        /// <summary>
        /// Определяет результирующие данные при операции чтения
        /// </summary>
        public string ResultString
        {
            get { return resultData; }
            set { resultData = value; }
        }

        /// <summary>
        /// Определяет время задержки перед ответом
        /// </summary>
        public int AnswerTimeout
        {
            get { return answerTimeout; }
            set { answerTimeout = value; }
        }

        /// <summary>
        /// Передает пакет на обработу
        /// </summary>
        /// <param name="packet">Передаваемый пакет</param>
        public void Packet(string packet)
        {
            if (onPacket != null)
            {
                if (proto.IsFromDevice(packet))
                {
                    string data = proto.GetData(packet);
                    if (data.Length == 0x20)
                    {
                        onPacket(packet);
                    }
                }
            }
        }

        /// <summary>
        /// Определяет настройки чтени/записи
        /// </summary>
        public IoOptions Options
        {
            get { return options; }
        }

        /// <summary>
        /// Определяет сервисы платформы
        /// </summary>
        public IApplication App
        {
            get { return application; }
        }

        /// <summary>
        /// Определяет сервисы для работы с протоколом
        /// </summary>
        public IProtocol Proto
        {
            get { return proto; }
        }

        /// <summary>
        /// Определяет операцию которую необходимо выполнить
        /// </summary>
        public OperationType OperationType
        {
            get { return o_type; }
            set { o_type = value; }
        }
    }

    /// <summary>
    /// Опции обмена
    /// </summary>
    public class IoOptions
    {
        // ---- данные класса ----

        private int deviceAnswerTimeout = 1000;     // таймаут ответа устройства
        private int timeoutBeetwenQuestions = 50;   // таймаут между попытками чтения/записи

        private int countDataCheck = 1;             // количество проверок данных
        private int countAttemptIo = 1;             // количество попыток чтени/записи

        private bool algorithm = true;             // тип алгоритма

        /// <summary>
        /// Инициализирует новы экземпляр класса
        /// </summary>
        public IoOptions()
        {
        }

        /// <summary>
        /// Определяет таймаут ответа от устройства
        /// </summary>
        public int DeviceAnswerTimeout
        {
            get { return deviceAnswerTimeout; }
            set { deviceAnswerTimeout = value; }
        }

        /// <summary>
        /// Определяет таймаут между попытками чтения/записи
        /// </summary>
        public int TimeoutBeetwenQuestions
        {
            get { return timeoutBeetwenQuestions; }
            set { timeoutBeetwenQuestions = value; }
        }

        /// <summary>
        /// Определяет количество проверок данных
        /// </summary>
        public int CountDataCheck
        {
            get { return countDataCheck; }
            set { countDataCheck = value; }
        }

        /// <summary>
        /// Определяет количество попыток чтени/записи
        /// </summary>
        public int CountAttemptIo
        {
            get { return countAttemptIo; }
            set { countAttemptIo = value; }
        }

        /// <summary>
        /// Определяет тип алгоритма записи данных.
        /// true - Алгоритм с защитой данных, false - Алгоритм без защиты данных
        /// </summary>
        public bool Algorithm
        {
            get { return algorithm; }
            set { algorithm = value; }
        }
    }

    /// <summary>
    /// Определяет тип операции
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// Чтение
        /// </summary>
        Read,

        /// <summary>
        /// Запись
        /// </summary>
        Write
    }
}