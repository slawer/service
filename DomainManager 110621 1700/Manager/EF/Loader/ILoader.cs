using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform
{
    /// <summary>
    /// Перечесление доступных форматов файла
    /// </summary>
    public enum FileFormat
    {
        /// <summary>
        /// простой текстовый формат (устарел)
        /// </summary>
        EF1TXT, 
        
        /// <summary>
        /// Формат XML (основной формат)
        /// </summary>
        EF2XML, 
        
        /// <summary>
        /// Формат XML (устаревший)
        /// </summary>
        EF2XMLOLD
    }

    /// <summary>
    /// Загрузка EPROM устройства из файла
    /// </summary>
    public interface IEFLoader
    {
        /// <summary>
        /// Загрузка EPROM устройства из файла
        /// </summary>
        /// <param name="filePath">URI файла, содержащего EPROM устройства</param>
        /// <returns>Загруженный EPROM устройства или же null, если загрузить EPROM не удалось</returns>
        Eprom Load(string filePath);
    }
}