using System;

namespace Platform
{
    /// <summary>
    /// Класс инстанцирующий класс загрузки EPROM устройства в указанном формате
    /// </summary>
    public static class EFLoader
    {
        /// <summary>
        /// Возвращает загрузчик EPROM для указанного формата файла
        /// </summary>
        /// <param name="format">Формат в котором хранится EPROM устройства</param>
        /// <returns>Загрузчик для указанного формата</returns>
        public static IEFLoader CreateLoader(FileFormat format)
        {
            switch (format)
            {
                case FileFormat.EF1TXT:

                    return new EF1TXTLoader();

                case FileFormat.EF2XML:

                    return new EF2XMLLoader();

                case FileFormat.EF2XMLOLD:

                    return new EF2XMLOLDLoader();
            }
            return null;
        }
    }
}