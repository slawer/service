using System;

namespace Platform
{
    /// <summary>
    /// Класс описывающий строку страницы EPROM устройства
    /// </summary>
    public class Line
    {
        public byte[] line;

        // ---- конструктор ------

        public Line()
        {
            line = new byte[16];
        }

        // --- свойства -------

        /// <summary>
        /// Получить байт по его смещению в строке
        /// </summary>
        /// <param name="index">Смещение</param>
        /// <returns>Байт</returns>
        public byte this[int index]
        {
            get
            {
                if (index > 0 && index < line.Length)
                {
                    return line[index];
                }
                throw new IndexOutOfRangeException();
            }

            set
            {
                if (index > 0 && index < line.Length)
                {
                    line[index] = value;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public override string ToString()
        {
            string total = string.Empty;
            foreach (var item in line)
            {
                total += string.Format("{0:x2}", item);
            }
            return total;
        }
    }
}