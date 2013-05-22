using System;

namespace Platform
{
    /// <summary>
    /// Интерфейс определяющий базовые параметры платформы
    /// </summary>
    public interface IPlatform
    {
        /// <summary>
        /// Частота рассылки пакетов плагинам 
        /// </summary>
        int SpeedSurvey { get; set; }

        /// <summary>
        /// Максимальное время на обработку пакета плагином
        /// </summary>
        int DownTime { get; set; }

        /// <summary>
        /// Максимальное количество пакетов , которое может быть в очереди на обработку плагином.
        /// </summary>
        int MaxPacketsCount { get; set; }

        /// <summary>
        /// Время предоставляемое плагину на обработку пакета
        /// </summary>
        int WaitTimeForWorking { get; set; }

        /// <summary>
        /// Обработка поступившего пакета
        /// </summary>
        /// <param name="packet">Пакет</param>
        void Process(Packet packet);

        string ConfigFile { get; }
    }
}