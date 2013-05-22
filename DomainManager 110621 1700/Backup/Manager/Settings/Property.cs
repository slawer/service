using System;

namespace Platform
{
    /// <summary>
    /// Класс описывающий свойство параметра
    /// </summary>
    public class Property
    {
        // ---- Данные класса -------

        private string nameProperty;
        private string valueProperty;

        // ------- Конструктор -------

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Имя свойства</param>
        /// <param name="Value">Значение свойства</param>
        public Property(string Name, string Value)
        {
            nameProperty = Name;
            valueProperty = Value;
        }

        // ------ Свойства ------

        /// <summary>
        /// Имя свойства
        /// </summary>
        public string Name
        {
            get { return nameProperty; }
        }

        /// <summary>
        /// Значение свойства
        /// </summary>
        public string Value
        {
            get { return valueProperty; }
        }
    }
}