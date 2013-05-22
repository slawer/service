using System;
using System.Threading;
using System.Security.AccessControl;

namespace SoftwareDevelopmentKit.Synchronization
{
    /// <summary>
    /// Простой мьютекс. Является простой оболочкой для мьютекса за исключением того что 
    /// можно не указывать время ожидания захвата блокировки
    /// </summary>
    public class _Mutex
    {
        // ---- данные класса ----

        private Mutex mutex = null;             // основной объект синхронизации
        private int timeWait = 3000;            // время ожидания захвата мьютекса

        private long captured = 0;              // сколько раз удалось захватить мьютекс
        private long not_captured = 0;          // сколько раз не удалось захватить мьютекс        

        /// <summary>
        /// Инициализирует новый экземпляр класса SoftwareDevelopmentKit.Synchronization._Mutex 
        /// со свойствами по умолчанию.
        /// </summary>
        public _Mutex()
        {
            mutex = new Mutex();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса SoftwareDevelopmentKit.Synchronization._Mutex 
        /// логическим значением, указывающим, является ли вызывающий поток изначальным владельцем мьютекса.
        /// </summary>
        /// <param name="initiallyOwned">Значение true для предоставления вызывающему потоку изначального владения
        /// мьютексом; в противном случае — false.</param>
        public _Mutex(bool initiallyOwned)
        {
            mutex = new Mutex(initiallyOwned);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса SoftwareDevelopmentKit.Synchronization._Mutex логическим значением,
        /// показывающим наличие начального владения семафором у вызывающего потока,
        /// и строкой, являющейся именем семафора.
        /// </summary>
        /// <param name="initiallyOwned">
        /// Значение true для предоставления вызывающему потоку изначального владения
        /// именованным системным мьютексом, если этот мьютекс создан данным вызовом;
        /// в противном случае – значение false.
        /// </param>
        /// <param name="name">
        /// Имя SoftwareDevelopmentKit.Synchronization._Mutex. Если значение равно null, у объекта System.Threading.Mutex
        /// нет имени.
        /// </param>
        /// <exception cref="System.UnauthorizedAccessException">
        /// Именованный мьютекс существует, имеет безопасность управления доступом, а
        /// пользователь не имеет прав System.Security.AccessControl.MutexRights.FullControl.
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// Произошла ошибка Win32.
        /// </exception>
        /// <exception cref="System.ApplicationException">
        /// Именованный мьютекс не может быть создан; вероятно, дескриптор ожидания другого
        /// типа имеет то же имя.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Длина значения параметра name более 260 символов
        /// </exception>
        public _Mutex(bool initiallyOwned, string name)
        {
            mutex = new Mutex(initiallyOwned, name);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса SoftwareDevelopmentKit.Synchronization._Mutex логическим значением,
        /// указывающим, должен ли вызывающий поток быть изначальным владельцем мьютекса,
        /// строкой, являющейся именем мьютекса, и логическим значением, которое при
        /// возврате метода показывает, предоставлено ли вызывающему потоку изначальное
        /// владение мьютексом.
        /// </summary>
        /// <param name="initiallyOwned">
        /// Значение true для предоставления вызывающему потоку изначального владения
        /// именованным системным мьютексом, если этот мьютекс создан данным вызовом;
        /// в противном случае – значение false.
        /// </param>
        /// <param name="name">
        /// Имя SoftwareDevelopmentKit.Synchronization._Mutex. Если значение равно null, у объекта System.Threading.Mutex
        /// нет имени.
        /// </param>
        /// <param name="createdNew">
        /// При возврате из метода содержит логическое значение true, если был создан
        /// локальный мьютекс (то есть, если параметр name имеет значение null или содержит
        /// пустую строку) или был создан именованный системный мьютекс; значение false,
        /// если указанный именованный системный мьютекс уже существует. Этот параметр
        /// передается неинициализированным.
        /// </param>
        /// <exception cref="System.UnauthorizedAccessException">
        /// Именованный мьютекс существует, имеет безопасность управления доступом, а
        /// пользователь не имеет прав System.Security.AccessControl.MutexRights.FullControl.
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// Произошла ошибка Win32.
        /// </exception>
        /// <exception cref="System.ApplicationException">
        /// Именованный мьютекс не может быть создан; вероятно, дескриптор ожидания другого
        /// типа имеет то же имя.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Длина значения параметра name более 260 символов
        /// </exception>
        public _Mutex(bool initiallyOwned, string name, out bool createdNew)
        {
            mutex = new Mutex(initiallyOwned, name, out createdNew);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса SoftwareDevelopmentKit.Synchronization._Mutex логическим значением,
        /// указывающим, должен ли вызывающий поток быть изначальным владельцем мьютекса,
        /// строкой, являющейся именем мьютекса, и логическим значением, которое при
        /// возврате метода показывает, предоставлено ли вызывающему потоку изначальное
        /// владение мьютексом, а также безопасность управления доступом для применения
        /// к именованному мьютексу.
        /// </summary>
        /// <param name="initiallyOwned">
        /// Значение true для предоставления вызывающему потоку изначального владения
        /// именованным системным мьютексом, если этот мьютекс создан данным вызовом;
        /// в противном случае – значение false.
        /// </param>
        /// <param name="name">
        /// Имя SoftwareDevelopmentKit.Synchronization._Mutex. Если значение равно null, у объекта System.Threading.Mutex
        /// нет имени.
        /// </param>
        /// <param name="createdNew">
        /// При возврате из метода содержит логическое значение true, если был создан
        /// локальный мьютекс (то есть, если параметр name имеет значение null или содержит
        /// пустую строку) или был создан именованный системный мьютекс; значение false,
        /// если указанный именованный системный мьютекс уже существует. Этот параметр
        /// передается неинициализированным.
        /// </param>
        /// <param name="mutexSecurity">
        /// Объект System.Security.AccessControl.MutexSecurity, представляющий безопасность
        /// управления доступом для применения к именованному системному мьютексу.
        /// </param>
        /// <exception cref="System.UnauthorizedAccessException">
        /// Именованный мьютекс существует, имеет безопасность управления доступом, а
        /// пользователь не имеет прав System.Security.AccessControl.MutexRights.FullControl.
        /// </exception>
        /// <exception cref="System.IO.IOException">
        /// Произошла ошибка Win32.
        /// </exception>
        /// <exception cref="System.ApplicationException">
        /// Именованный мьютекс не может быть создан; вероятно, дескриптор ожидания другого
        /// типа имеет то же имя.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Длина значения параметра name более 260 символов
        /// </exception>
        public _Mutex(bool initiallyOwned, string name, out bool createdNew, MutexSecurity mutexSecurity)
        {
            mutex = new Mutex(initiallyOwned, name, out createdNew, mutexSecurity);
        }

        /// <summary>
        /// Получает объект System.Security.AccessControl.MutexSecurity, представляющий
        /// безопасность управления доступом для именованного мьютекса.
        /// </summary>
        /// <returns>
        /// Объект System.Security.AccessControl.MutexSecurity, представляющий безопасность
        /// управления доступом для именованного мьютекса.
        /// </returns>
        /// <exception cref="System.UnauthorizedAccessException">
        /// Текущий объект System.Threading.Mutex представляет именованный системный
        /// мьютекс, но пользователь не имеет прав System.Security.AccessControl.MutexRights.ReadPermissions.-или-Текущий
        /// объект System.Threading.Mutex представляет именованный системный мьютекс,
        /// но он не был открыт с правами System.Security.AccessControl.MutexRights.ReadPermissions.
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// Не поддерживается Windows 98 и Windows Millennium Edition.
        /// </exception>
        public MutexSecurity GetAccessControl()
        {
            try
            {
                return mutex.GetAccessControl();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message, ex);
            }
            catch (NotSupportedException ex)
            {
                throw new NotSupportedException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Блокирует текущий поток до получения текущим дескриптором System.Threading.WaitHandle
        /// сигнала, используя 32-разрядное целочисленное значение со знаком для измерения
        /// интервала времени.
        /// </summary>
        /// <param name="milliseconds">
        /// Время ожидания в миллисекундах или System.Threading.Timeout.Infinite. (-1),
        /// если ожидание должно быть бесконечным.
        /// </param>
        /// <exception cref="System.ObjectDisposedException">
        /// Текущий экземпляр уже был удален
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Параметр timeout является отрицательным числом, отличным от -1, которое представляет
        /// неограниченное время ожидания.  –или– Значение параметра timeout больше величины
        /// System.Int32.MaxValue.
        /// </exception>
        /// <exception cref="System.Threading.AbandonedMutexException">
        /// Ожидание завершено, поскольку поток завершил работу, не освободив мьютекс.
        /// Это исключение не выдается в операционных системах Windows 98 и Windows Millennium
        /// Edition.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Текущий экземпляр является прозрачным прокси для объекта System.Threading.WaitHandle
        /// в другом домене приложения.
        /// </exception>
        /// <returns>
        /// Значение true при получении сигнала текущим экземпляром; в противном случае
        /// — false.
        /// </returns>
        public Boolean Wait(int milliseconds)
        {
            if (mutex.WaitOne(milliseconds))
            {
                captured += 1;
                return true;
            }
            not_captured += 1;
            return false;
        }

        /// <summary>
        /// Блокирует текущий поток до получения текущим дескриптором System.Threading.WaitHandle
        /// сигнала, используя 32-разрядное целочисленное значение со знаком для измерения
        /// интервала времени.
        /// </summary>
        public Boolean Wait()
        {
            return Wait(timeWait);
        }

        /// <summary>
        /// Освобождает объект SoftwareDevelopmentKit.Synchronization._Mutex один раз.
        /// </summary>
        /// <exception cref="ApplicationException">
        /// Вызывающий поток не является владельцем мьютекса.
        /// </exception>
        public void ReleaseMutex()
        {
            try
            {
                mutex.ReleaseMutex();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Определяет время ожидания захвата мьютекса
        /// </summary>
        public int TimeWait
        {
            get { return timeWait; }
            set { timeWait = value; }
        }

    }
}