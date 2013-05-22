using System;

namespace SoftwareDevelopmentKit.Services.Types
{
    /// <summary>
    /// Определяет базовый интерфейс службы
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Генерируется когда происходит ошибка в работе службы
        /// </summary>
        event ServiceMessageEventHandler onError;

        /// <summary>
        /// Генерируется когда служба завершает свою работу
        /// </summary>
        event ServiceMessageEventHandler onExit;

        /// <summary>
        /// Определяет состоние службы
        /// </summary>
        ServiceState State { get; }

        /// <summary>
        /// Запустить службу на выполнение
        /// </summary>
        void ServiceStart(object state);

        /// <summary>
        /// Приостановить выполнение службы
        /// </summary>
        void SuspentService();

        /// <summary>
        /// Возобновить выполнение службы
        /// </summary>
        void ResumeService();

        /// <summary>
        /// Выполняет завершение выполнения службы
        /// </summary>
        void AbortService();
    }    
}