using System;
using System.Diagnostics;

namespace Platform
{
    public interface IApplication
    {
        /// <summary>
        /// Имя платформы
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Автор платформы
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Описание платформы
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Версия платформы
        /// </summary>
        Version Version { get; }                

        /// <summary>
        /// Отправка пакета по TCP серверу
        /// </summary>
        /// <param name="packet">Пакет</param>
        void SendPacket(Packet packet);

        /// <summary>
        /// Возвращяет загрузчик, выполняющий загрузку EPROM с диска(файла)
        /// </summary>
        /// <param name="format">Формат в котором сохранен EPROM в файле</param>
        /// <returns></returns>
        IEFLoader GetEFLoader(FileFormat format);

        /// <summary>
        /// Возвращяет сохраняльщик, выполняющий сохранение EPROM на диска(файла)
        /// </summary>
        /// <param name="format">Формат в котором нужно сохранить EPROM в файле</param>
        /// <returns></returns>
        IEFSaver GetEFSaver(FileFormat format);

        /// <summary>
        /// Возвращяет объект, предоставляющий функции для работы с протоколом
        /// </summary>
        /// <param name="version">Версия формата протокола</param>
        /// <returns></returns>
        IProtocol GetProtocol(ProtocolVersion version);

        /// <summary>
        /// Выполняет сохранение инфомации в лог
        /// </summary>
        /// <param name="Name">Имя плагина, сохраняющего сообщение в лог</param>
        /// <param name="Message">Сообщение</param>
        /// <param name="TypeEvent">Тип сообщения</param>
        void ToLogMessage(string Name, string Message, EventLogEntryType TypeEvent);

        /// <summary>
        /// Получить интерфейс реализующий чтение/записть
        /// </summary>
        /// <returns>Интерфей реализующий чтени/запись</returns>
        IEpromIO GetEpromRW();
    }
}