using System;

namespace Platform
{
    /// <summary>
    /// Класс описывающий EPROM устройства
    /// </summary>
    public class Eprom
    {
        Page[] pages;

        public Eprom()
        {
            pages = new Page[7];
            for (int i = 0; i < pages.Length; i++) pages[i] = new Page();
        }

        public Eprom(byte v)
        {
            pages = new Page[7];
            for (int i = 0; i < pages.Length; i++) pages[i] = new Page(v);
        }

        /// <summary>
        /// Возвращяет значение байта
        /// </summary>
        /// <param name="offset">Смещение (абсолютное, то есть начинается с нуля)</param>
        /// <returns>Значение байта по указанному смещению</returns>
        public byte GetByte(int offset)
        {
            if (offset < 0 || offset > 1792)
            {
                throw new ArgumentOutOfRangeException("offset", "Выходит за допустимый диапазон");
            }

            int page = (int)(offset / 256) - 1;
            int offs = (int)(offset % 256);

            if (page < 0) page = 0;
            return pages[page][offs];
        }

        /// <summary>
        /// Устанавливает значение байта
        /// </summary>
        /// <param name="offset">Смещение (абсолютное, то есть начинается с нуля)</param>
        /// <returns>Значение байта по указанному смещению которое было до установки нового</returns>
        public byte SetByte(int offset, byte val)
        {
            if (offset < 0 || offset > 1792)
            {
                throw new ArgumentOutOfRangeException("offset", "Выходит за допустимый диапазон");
            }

            int page = (int)(offset / 256) - 1;
            int offs = (int)(offset % 256);

            byte last = pages[page][offs];
            pages[page][offs] = val;

            return last;
        }

        /// <summary>
        /// Получить страницу по ее номеру
        /// </summary>
        /// <param name="index">Номер страницы</param>
        /// <returns>Запрашиваемая страница EPROM-а</returns>
        public Page this[int index]
        {
            get
            {
                if (index > -1 && index < pages.Length)
                {
                    return pages[index];
                }
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Получить страницы
        /// </summary>
        public Page[] Pages
        {
            get { return pages; }
        }
    }
}