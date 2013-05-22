using System;

namespace CMDgenerator1
{
    public class Property
    {
        // ---- Данные класса -------

        private string nameProperty;
        private string valueProperty;

        // ------- Конструктор -------

        public Property(string Name, string Value)
        {
            nameProperty = Name;
            valueProperty = Value;
        }

        // ------ Свойства ------

        public string Name
        {
            get { return nameProperty; }
        }

        public string Value
        {
            get { return valueProperty; }
        }
    }
}
