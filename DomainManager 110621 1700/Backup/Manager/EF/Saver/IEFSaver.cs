using System;

namespace Platform
{
    /// <summary>
    /// Сохранение EPROM устройства в файл
    /// </summary>
    public interface IEFSaver
    {
        /// <summary>
        /// Созранить EPROM устройства в файл
        /// </summary>
        /// <param name="filePath">URI файла в который необходимо сохранить EPROM устройства</param>
        /// <param name="eprom">EPROM клторый необходимо сохранить</param>
        void Save(string filePath, Eprom eprom);
    }
}