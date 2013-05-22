using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;

namespace Platform
{
    /// <summary>
    /// Осуществляет чтени/запись. Используется при программировании плагинов платформы
    /// </summary>
    class PluginRW : IEpromIO
    {
        private IProtocol proto = null;         // интерфейс для работы с протоколом обмена
        private IApplication app = null;        // ссылка на интерфейс платформы

        // буферизация
        private List<string> sharing = null;        // первичный
        private List<string> working = null;        // рабочий

        // синхронизация
        private AutoResetEvent mevent;              // определяем наличие ответа
        private object workHandle = null;           // общая синхронизация

        private bool started = false;               // определяет нужен ли нам пакет для обработки

        private Information options = null;         // определяет параметры операции чтени/записи

        private string sending;                 // пакет на отравку
        private string answer;                  // ответ который должен быть получен

        private string data = string.Empty;             // необходим для проверок поступивших данных
        private ResultOperation lastOperation = ResultOperation.Default;    // результат последней операции

        private string template = string.Empty;
        public event EventHandler eComplete;   // генерируется после того как будес считанна очередная строка EPROM устройства

        /// <summary>
        /// Определт результат последней операции чтения/записи
        /// </summary>
        public ResultOperation LastOperation
        {
            get { return lastOperation; }
        }

        /// <summary>
        /// Определяет параметры операции чтения/записи
        /// </summary>
        public Information Options
        {
            get { return options; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="application">Ссылка на интерфейс платформы</param>
        public PluginRW(IApplication application)
        {
            app = application;                              // сохраняем ссылку на платформу
            proto = app.GetProtocol(ProtocolVersion.x100);  // получаем интерфейс на класс выполняющий работу с протоколом

            sharing = new List<string>();       // инициализируем
            working = new List<string>();       // инициализируем

            workHandle = new object();              // инициализируем
            mevent = new AutoResetEvent(false);     // инициализируем

            options = new Information();        // инициализируем
        }

        /// <summary>
        /// Выплняет обработку поступившего пакета
        /// </summary>
        /// <param name="packet">Пакет для обработки</param>
        public void Packet(string packet)
        {
            lock (workHandle)                                           // захватить управление
            {
                if (!started) return;
                if (proto.GetNumberDevice(packet) == options.Device)    // если пакет от нужного нам устройства
                {
                    lock (sharing)              // захватить первичный буфер
                    {
                        sharing.Add(packet);    // сохранить пакет
                    }
                    lock (mevent) mevent.Set();     // установить в сигнальное состояние объект событие. Пакет пришел
                }
            }
        }

        private string CalculateResult()
        {
            try
            {
                List<string> Datas = new List<string>();
                for (int check = 0; check <= options.NumberOfDataChecks; check++)
                {
                    switch (ChekerToRWEprom(new Packet(sending, DateTime.Now, null)))
                    {
                        case ResultOperation.Succes:

                            Datas.Add(proto.GetData(data));
                            data = CheckDatas(Datas);
                            if (data != string.Empty)
                            {
                                Datas.Clear();

                                lastOperation = ResultOperation.Succes;
                                return data;
                            }
                            break;

                        case ResultOperation.Timeout:

                            lastOperation = ResultOperation.Timeout;
                            return string.Empty;

                        case ResultOperation.MorePopit:

                            lastOperation = ResultOperation.MorePopit;
                            return string.Empty;
                    }
                }
                lastOperation = ResultOperation.Default;
                return string.Empty;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Выполняет запись в EPROM устройства(запись осуществляется с помощью команды запись и чтение)
        /// </summary>
        /// <param name="Device">Номер устройства</param>
        /// <param name="Page">Номер страницы(нумерация начинается с единицы)</param>
        /// <param name="Offset">Смещение по которому необходимо начинать записб данных в устройство</param>
        /// <param name="Lenght">Длинна записываемых данных</param>
        /// <param name="Data">Данные для записи</param>
        /// <returns>Результат</returns>
        public string Write(int Device, int Page, int Offset, int Lenght, string Data)
        {
            try
            {
                options.Device = Device;            // сохраняем номер устройства
                options.Page = Page;                // сохраняем номер страницы
                options.Offset = Offset;            // сохраняем смещение на странице
                options.Lenght = Lenght;            // сохраняем длинну данных

                sending = proto.CreateCommand(Device, Command.ReadWrite, Page, Offset, Lenght, Data);  // генерируем команду записи
                answer = string.Format("{0:X2}", (7 + Lenght)) + sending.Substring(13, 6);          // генерируем команду ответа

                started = true;
                return CalculateResult();
            }
            finally
            {
                sharing.Clear();
                working.Clear();

                started = false;
                template = string.Empty;
            }
        }

        /// <summary>
        /// Выполняет чтение данных с EPROM устройства
        /// </summary>
        /// <param name="Device">Номер устройства</param>
        /// <param name="Page">Номер страницы(нумерация страниц начинается с единицы)</param>
        /// <param name="Offset">Смещение с которого необходимо выполнять чтение</param>
        /// <param name="Lenght">Размер, в байтах, данных которые необходимо считать</param>
        /// <returns>Пакет ответа со считанными данными</returns>
        public string Read(int Device, int Page, int Offset, int Lenght)
        {
            try
            {
                options.Device = Device;            // сохраняем номер устройства
                options.Page = Page;                // сохраняем номер страницы
                options.Offset = Offset;            // сохраняем смещение на странице
                options.Lenght = Lenght;            // сохраняем длинну данных

                sending = proto.CreateCommand(Device, Command.Read, Page, Offset, Lenght, null);  // генерируем команду чтения
                answer = string.Format("{0:X2}", (7 + Lenght)) + sending.Substring(13, 6);          // генерируем команду ответа

                started = true;
                return CalculateResult();
            }
            finally
            {
                sharing.Clear();
                working.Clear();
                
                started = false;
                template = string.Empty;
            }
        }

        /// <summary>
        /// Выполняет произвольный запрос, с учетом что на выполняемый запрос должен быть ответ
        /// </summary>
        /// <param name="Question">Строка запроса, который необходимо выполнить(отправить устройству)</param>
        /// <param name="Template">Должен быть равен string.Empty</param>
        /// <param name="Answer">Критерий по которому определяется ответ на запрос</param>
        /// <returns>Пакет ответа на заданный запрос устройству</returns>
        public string FreeQuestion(string Question, string Template, string Answer)
        {
            try
            {
                sending = Question;
                answer = Answer;

                template = Template;

                started = true;
                return CalculateResult();
            }
            finally
            {
                sharing.Clear();
                working.Clear();

                started = false;
                template = string.Empty;
            }
        }

        /// <summary>
        /// Читаем конфигурацию
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private ResultOperation ChekerToRWEprom(Packet packet)
        {
            try
            {
                for (int count = 0; count < options.AttemptsToReadWriteEntries; count++)
                {
                    app.SendPacket(packet);
                    long baseTime = DateTime.Now.Ticks;
                m:
                    if (mevent.WaitOne(options.TimeoutForResponseFromDevice))
                    {
                        lock (sharing)
                        {
                            if (sharing.Count > 0)
                            {
                                working.AddRange(sharing);
                                sharing.Clear();
                            }
                        }

                        if (CheckOneToRWEprom(packet))
                        {
                            return ResultOperation.Succes;
                        }
                        else
                        {
                            long time = (long)((DateTime.Now.Ticks - baseTime) * 1E-4);
                            if (time > options.TimeoutForResponseFromDevice)
                            {
                                return ResultOperation.Timeout;
                            }
                            else
                                goto m;
                        }
                    }
                }
                return ResultOperation.MorePopit;
            }
            finally
            {
                working.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private bool CheckOneToRWEprom(Packet packet)
        {
            bool result = false;
            foreach (string pack in working)
            {
                string total = string.Empty;

                if (template == string.Empty)
                {
                    total = pack.Substring(4, 8);
                }
                else
                    total = template;

                if (total == answer)
                {
                    data = pack;
                    return true;
                }
            }
            return result;
        }

        /// <summary>
        /// проверка данных
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private string CheckDatas(List<string> datas)
        {
            string result = string.Empty;
            if (options.NumberOfDataChecks == 0) result = datas[0];

            if (datas.Count > 1)
            {
                List<string> bytes = new List<string>();
                for (int index = 0; index < 16; index++)
                {
                    bytes.Clear();
                    foreach (string data in datas)
                    {
                        bytes.Add(data.Substring(index * 2, 2));
                    }

                    bool fl = false;
                    for (int i = 0; i < bytes.Count; i++)
                    {
                        if (GetCountEqual(bytes[i], bytes, 2) == 2)
                        {
                            fl = true;
                            result += bytes[i];
                            break;
                        }
                    }
                    if (!fl) return string.Empty;
                }
            }
            return result;
        }

        private int GetCountEqual(string item, List<string> items, int maxCount)
        {
            int count = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (item == items[i])
                {
                    count += 1;
                    if (count == maxCount) return count;
                }
            }
            return count;
        }

        /// <summary>
        /// Возврощает EPROM устройства(считывание выполняется в синхронном режиме)
        /// </summary>
        /// <returns>EPROM устройства</returns>
        public Eprom GetEprom()
        {
            Eprom eprom = new Eprom();
            for (int page = 1; page < 8; page++)
            {
                for (int line = 0; line < 16; line++)
                {
                    string data = Read(Options.Device, page, line * 16, 16);
                    switch (lastOperation)
                    {
                        case ResultOperation.Succes:

                            if (data.Length == 32)
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    string ch = data.Substring(i * 2, 2);
                                    eprom[page - 1][line * 16 + i] = byte.Parse(ch, NumberStyles.AllowHexSpecifier);
                                }
                                if (eComplete != null) eComplete(this, new EventArgs());
                            }
                            else
                                throw new Exception("Произошла ошибка при чтении данных!");

                            break;

                        default:

                            return null;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Возврощает EPROM устройства с заданными страницами
        /// </summary>
        /// <param name="Pages">массив страниц, которые необходимо загрузить</param>
        /// <returns>EPROM устройства</returns>
        public Eprom GetEprom(int[] Pages)
        {
            if (Pages != null)
            {
                Eprom eprom = new Eprom();
                foreach (int pageNumber in Pages)
                {
                    if (pageNumber >= 1 && pageNumber <= 7)
                    {
                        for (int line = 0; line < 16; line++)
                        {
                            string data = Read(Options.Device, pageNumber, line * 16, 16);
                            switch (lastOperation)
                            {
                                case ResultOperation.Succes:

                                    if (data.Length == 32)
                                    {
                                        for (int i = 0; i < 16; i++)
                                        {
                                            string ch = data.Substring(i * 2, 2);
                                            eprom[pageNumber - 1][line * 16 + i] = byte.Parse(ch, NumberStyles.AllowHexSpecifier);
                                        }
                                        if (eComplete != null) eComplete(this, new EventArgs());
                                    }
                                    else
                                        throw new Exception("Произошла ошибка при чтении данных!");

                                    break;

                                default:

                                    return null;
                            }
                        }
                    }
                    else
                        throw new ArgumentException("Значение номера страницы не корректно");
                }
                return eprom;
            }
            return null;
        }

        public string Read()
        {
            try
            {
                sending = proto.CreateCommand(options.Device, Command.Read, options.Page, 
                    options.Offset, options.Lenght, null);  // генерируем команду чтения

                answer = string.Format("{0:X2}", (7 + options.Lenght)) + sending.Substring(13, 6);          // генерируем команду ответа

                started = true;
                return CalculateResult();
            }
            finally
            {
                sharing.Clear();
                working.Clear();

                started = false;
                template = string.Empty;
            }
        }

        /// <summary>
        /// Выполняет запись данных в EPROM устройства, с параметрами указанными в настройках данного класса
        /// </summary>
        /// <param name="Data">Данные которые необходимо записать</param>
        /// <returns>Пакет, который является ответом на запрос записи данных в устройство</returns>
        public string Write(string Data)
        {
            int Lenght = (int)(Data.Length / 2);

            sending = proto.CreateCommand(options.Device, Command.Read, options.Page,
                options.Offset, Lenght, Data);  // генерируем команду чтения
            answer = string.Format("{0:X2}", (7 + Lenght)) + sending.Substring(13, 6);          // генерируем команду ответа

            started = true;
            return CalculateResult();
        }

        /// <summary>
        /// Выполняет произвольный запрос, с учетом что на выполняемый запрос должен быть ответ
        /// </summary>
        /// <param name="Question">Строка запроса, который необходимо выполнить(отправить устройству)</param>
        /// <param name="Answer">Критерий по которому определяется ответ на запрос</param>
        /// <returns>Пакет ответа на заданный запрос устройству</returns>
        public string FreeQuestion(string Question, string Answer)
        {
            return FreeQuestion(Question, string.Empty, Answer);
        }

        /// <summary>
        /// Записать выбранный eprom в устройство
        /// </summary>
        /// <param name="eprom">Eprom который необходимо записать</param>
        public void SetEprom(Eprom eprom)
        {
            for (int page = 1; page < 8; page++)
            {
                for (int line = 0; line < 16; line++)
                {
                    string data = Write(Options.Device, page, line * 16, 16, eprom[page-1].Lines[line].ToString());
                    switch (lastOperation)
                    {
                        case ResultOperation.Succes:

                            if (eComplete != null) eComplete(this, new EventArgs()); 
                            break;

                        default:

                            return;
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Записать выбранный eprom в устройство с заданными страницами
        /// </summary>
        /// <param name="eprom">Eprom который необходимо записать</param>
        /// <param name="Pages">Страницы которые необходимо записать</param>
        public void SetEprom(Eprom eprom, int[] Pages)
        {
            if (Pages != null)
            {
                foreach (int pageNumber in Pages)
                {
                    if (pageNumber >= 1 && pageNumber <= 7)
                    {
                        for (int line = 0; line < 16; line++)
                        {
                            string data = Write(Options.Device, pageNumber, line * 16, 16, eprom[pageNumber - 1].Lines[line].ToString());
                            switch (lastOperation)
                            {
                                case ResultOperation.Succes:

                                    if (eComplete != null) eComplete(this, new EventArgs());
                                    break;

                                default:

                                    return;
                            }
                        }
                    }
                    else
                        throw new ArgumentException("Значение номера страницы не корректно");
                }
                return;
            }
            return;
        }
    }
}