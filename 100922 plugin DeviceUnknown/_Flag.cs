using System;
using System.Threading;

namespace SoftwareDevelopmentKit.Synchronization
{
    /// <summary>
    /// Позволяет определить состояние булевой переменной в многопоточном приложении
    /// </summary>
    class _Flag
    {
        // ---- данные класса ----

        private Mutex mutex = null;         // синхронизует доступ к булевой переменной
        private Boolean flag = false;       // булева переменная

        /// <summary>
        /// Инициализирует новый экземпляр класса со значение флага false
        /// </summary>
        public _Flag()
        {
            mutex = new Mutex();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с указанным значение флага
        /// </summary>
        /// <param name="state">Состояние булевой переменной</param>
        public _Flag(bool state)
        {
            flag = state;
            mutex = new Mutex();
        }

        public Boolean Flag
        {
            get
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(3000))
                    {
                        blocked = true;
                        return flag;
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }

            set
            {
                bool blocked = false;
                try
                {
                    if (mutex.WaitOne(3000))
                    {
                        blocked = true;
                        flag = value;
                    }
                    else
                        throw new TimeoutException();
                }
                finally
                {
                    if (blocked) mutex.ReleaseMutex();
                }
            }
        }

    }
}
