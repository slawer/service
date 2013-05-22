using System;
using System.Collections.Generic;

namespace BOtimeReset1
{
    public class Parameter
    {
        // --- Данные класса -----

        private string nameParameter;
        private string valueParameter;

        private List<Property> properties;

        // ------ Конструктор ------

        public Parameter(string name)
        {
            nameParameter = name;
            properties = new List<Property>();
        }

        // ----- Свойства ------

        public string Name
        {
            get { return nameParameter; }
        }

        public string Value
        {
            get { return valueParameter; }
            set { valueParameter = value; }
        }

        public Property[] Properties
        {
            get { return properties.ToArray(); }
        }

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

        public void Insert(Property property)
        {
            properties.Add(property);
        }

        public void Remove(Property property)
        {
            properties.Remove(property);
        }

    }
}
