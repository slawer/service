using System.Threading;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace Platform
{
    /// <summary>
    /// Класс, выполняющий сохранение сообщений в лог
    /// </summary>
    class Journal
    {
        // ----- Данные класса --------

        private static Journal log = null;
        private static Mutex logSync = null;

        // -------- Конструктор --------

        protected Journal()
        {
        }

        // -------- Получение одиночки -------

        /// <summary>
        /// Получение экземпляря класса для сохранения сообщения в файл
        /// </summary>
        /// <returns>Класс Journal</returns>
        public static Journal GetLogJournal()
        {
            if (log == null)
            {
                logSync = new Mutex(false);

                log = new Journal();
                log.InitLog();                
            }
            return log;
        }

        // ------- Инициализация --------

        protected void InitLog()
        {
            if (!EventLog.Exists(myLog) || !EventLog.SourceExists(mySource))
            {
                EventLog.CreateEventSource(mySource, myLog);
                EventLog log = new EventLog(myLog);
                log.MaximumKilobytes = maximumKilobytes;
                log.ModifyOverflowPolicy(OverflowAction.OverwriteOlder, retentionDay);
            }
        }

        // -------- Записать данные в журнал -------

        /// <summary>
        /// Записать сообщение в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="eventType">Тип сообщения</param>
        public void WriteEvent(string message, EventLogEntryType eventType)
        {
            bool flag = false;
            try
            {
                if (logSync.WaitOne(waitTimeout))
                {
                    flag = true;
                    EventLog.WriteEntry(mySource, message, eventType);
                    flag = false;
                    logSync.ReleaseMutex();
                }
            }
            finally
            {
                if (flag) logSync.ReleaseMutex();
            }
        }

        /// <summary>
        /// Записать сообщение в лог
        /// </summary>
        /// <param name="message">Сообщени</param>
        /// <param name="stack">Стек</param>
        /// <param name="eventType">Тип сообщения</param>
        public void WriteEvent(string message, string stack, EventLogEntryType eventType)
        {
            bool flag = false;
            try
            {
                if (logSync.WaitOne(waitTimeout))
                {
                    flag = true;

                    string logMessage = "Message: " + Constants.vbCrLf + message + Constants.vbCrLf +
                        "Stack" + Constants.vbCrLf + stack;

                    EventLog.WriteEntry(mySource, logMessage, eventType);
                    flag = false;
                    logSync.ReleaseMutex();
                }
            }
            finally
            {
                if (flag) logSync.ReleaseMutex();
            }
        }

        // --------- Базовые константы ---------

        private const int retentionDay = 7;
        private const int maximumKilobytes = 4194240;

        private const int waitTimeout = 3000;

        private const string myLog = "DomainManager";
        private const string mySource = "99759686-58d6-4955-b8a9-fe102f0dbd07";
    }
}
