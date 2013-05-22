using System;
using System.IO;
using System.Globalization;

namespace Platform
{
    /// <summary>
    /// Класс выполняющий загрузку EPROM устройства из файла
    /// </summary>
    class EF1TXTLoader : IEFLoader
    {
        // ----- константы --------

        private const int lineLenght = 55;
        
        /// <summary>
        /// Загружает EPROM устройства из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns>Загруженный EPROM устройства или же null, если загрузить EPROM не удалось</returns>
        public Eprom Load(string filePath)
        {
            try
            {
                Eprom eprom = new Eprom();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int page = 0, offset = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length == lineLenght)
                        {
                            string total = line.Substring(4).Replace(" ", string.Empty);
                            for (int i = 0; i < total.Length / 2; i++)
                            {
                                string sByte = total.Substring(i * 2, 2);
                                eprom[page][offset] = (byte)(int.Parse(sByte, NumberStyles.AllowHexSpecifier));
                                offset += 1;
                            }
                        }
                        else
                        {
                            offset = 0;
                            page += 1;
                        }
                    }
                    return eprom;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}