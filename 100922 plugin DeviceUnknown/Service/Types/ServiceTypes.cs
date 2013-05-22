using System;
using SoftwareDevelopmentKit.Synchronization;

namespace SoftwareDevelopmentKit.Services.Types
{
    /// <summary>
    /// Определяет делегат обработки сообщений службы
    /// </summary>
    /// <param name="sender">Источник события</param>
    /// <param name="e">Параметры события</param>
    public delegate void ServiceMessageEventHandler(Object sender, ServiceEventArgs e);

    /// <summary>
    /// Определяет тип события
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Информация
        /// </summary>
        Information,

        /// <summary>
        /// Ошибка
        /// </summary>
        Error,

        /// <summary>
        /// Фатальная ошибка
        /// </summary>
        FatalError
    }

    /// <summary>
    /// Определяет данные события службы
    /// </summary>
    public class ServiceEventArgs : EventArgs
    {
        // ---- данные класса ----

        private string message;         // сообщение
        private EventType e_type;       // тип события

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public ServiceEventArgs()
            : base()
        {
            message = string.Empty;
            e_type = EventType.Information;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="_message">Строка сообщения</param>
        public ServiceEventArgs(string _message)
            : base()
        {
            message = _message;
            e_type = EventType.Information;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="_message">Строка сообщения</param>
        /// <param name="_type">Тип события</param>
        public ServiceEventArgs(string _message, EventType _type)
            : base()
        {
            e_type = _type;
            message = _message;            
        }

        /// <summary>
        /// Определяет сообщение события
        /// </summary>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// Определяет тип события
        /// </summary>
        public EventType Type
        {
            get { return e_type; }
        }
    }

    // ----------- 

    /// <summary>
    /// Определяет состояние службы
    /// </summary>
    public enum ServiceState
    {
        /// <summary>
        /// Запуск потока на выполнение
        /// </summary>
        Running,

        /// <summary>
        /// Приостановить выполненение
        /// </summary>
        Suspend,

        /// <summary>
        /// Завершить выполнение потока
        /// </summary>
        Aborted,

        /// <summary>
        /// Состояние по умолчанию
        /// </summary>
        Default,

        /// <summary>
        /// Состояние не удалось определить
        /// </summary>
        Unknown
    }

    // ----------------

    /// <summary>
    /// Предоставляет потокобезопасный доступ к объекту состояния службы
    /// </summary>
    internal class ServiceStatus
    {
        // ---- данные класса ----

        private _Mutex mutex;               // обеспечивает потокобезопасный доступ к состоянию службы
        private ServiceState state;         // определяет текущее состояние службы

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public ServiceStatus()
        {
            mutex = new _Mutex();
            state = ServiceState.Default;
        }

        /// <summary>
        /// Опрелеляет текущее состояние службы
        /// </summary>
        public ServiceState State
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.Wait())
                    {
                        blocked = true;
                        return state;
                    }
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
                return ServiceState.Unknown;
            }

            set
            {
                bool blocked = false;
                try
                {
                    if (mutex.Wait())
                    {
                        blocked = true;
                        state = value;
                    }
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
        }
    }
}