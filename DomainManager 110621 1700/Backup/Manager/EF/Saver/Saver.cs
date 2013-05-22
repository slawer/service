using System;

namespace Platform
{
    /// <summary>
    /// Класс инстанцирующий класс сохранения EPROM устройства в указанном формате
    /// </summary>
    static class EFSaver
    {
        /// <summary>
        /// Вернуть класс сохраняюший EPROM устройства в указанном формате
        /// </summary>
        /// <param name="format">Формат в котором неоходимо сохранить EPROM устройства</param>
        /// <returns>Класс, выполняющий сохранение EPROM в файл</returns>
        public static IEFSaver CreateSaver(FileFormat format)
        {
            switch (format)
            {
                case FileFormat.EF1TXT:

                    return new EF1TXTSaver();

                case FileFormat.EF2XML:

                    return new EF2XMLSaver();

            }
            return null;
        }
    }
}