using System;
using System.Collections.Generic;

namespace Platform
{
    /// <summary>
    /// Класс определяющий параметр конфигурации
    /// </summary>
    public class Parameter
    {
        // --- Данные класса -----

        private string nameParameter;
        private string valueParameter;

        private List<Property> properties;

        // ------ Конструктор ------

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя параметра</param>
        public Parameter(string name)
        {
            nameParameter = name;
            properties = new List<Property>();
        }

        // ----- Свойства ------

        /// <summary>
        /// Имя параметра
        /// </summary>
        public string Name
        {
            get { return nameParameter; }
        }

        /// <summary>
        /// Значение параметра
        /// </summary>
        public string Value
        {
            get { return valueParameter; }
            set { valueParameter = value; }
        }

        /// <summary>
        /// Список свойств параметра
        /// </summary>
        public Property[] Properties
        {
            get { return properties.ToArray(); }
        }

        /// <summary>
        /// Получить свойство параметра по его индексу
        /// </summary>
        /// <param name="index">Индек параметра</param>
        /// <returns>Свойство параметра</returns>
        public Property this[int index]
        {
            get 
            {
                if (index >= 0 & index < properties.Count)
                {
                    return properties[index];
                }
                else
                    throw new IndexOutOfRangeException("index out of range");
            }
        }

        // ----- Управление свойствами -----

        /// <summary>
        /// Добавить свойство
        /// </summary>
        /// <param name="property">Свойство</param>
        public void Insert(Property property)
        {
            properties.Add(property);
        }

        /// <summary>
        /// Удалить свойство
        /// </summary>
        /// <param name="property">Удаляемое свойство</param>
        public void Remove(Property property)
        {
            properties.Remove(property);
        }

    }
}