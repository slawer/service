using System;

namespace Platform
{
    /// <summary>
    /// Класс описывающий страницу EPROM устройства
    /// </summary>
    public class Page
    {
        // ---- строки данных -----

        private byte[] page;
        // ------ конструктор -------

        public Page()
        {
            page = new byte[256];
            for (int i = 0; i < page.Length; i++) page[i] = 0xff;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public Page(byte v)
        {
            page = new byte[256];
            for (int i = 0; i < page.Length; i++) page[i] = v;
        }

        // ---- свойства -----

        /// <summary>
        /// Получить байт по его смещению на странице
        /// </summary>
        /// <param name="offset">Смещение</param>
        /// <returns>Байт</returns>
        public byte this[int offset]
        {
            get
            {
                if (offset > -1 && offset < page.Length)
                {
                    return page[offset];
                }
                else
                    throw new IndexOutOfRangeException();
            }

            set
            {
                if (offset > -1 && offset < page.Length)
                {
                    page[offset] = value;
                }
                else
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Список строк страницы
        /// </summary>
        public Line[] Lines
        {
            get
            {
                Line[] lns = new Line[16];
                for (int i = 0; i < 16; i++)
                {
                    lns[i] = new Line();
                    Array.Copy(page, i * 16, lns[i].line, 0, 16);
                }
                return lns;
            }
        }
    }
}