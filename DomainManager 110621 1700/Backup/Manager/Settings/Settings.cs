using System;
using System.Xml;
using System.Collections.Generic;

namespace Platform
{
    /// <summary>
    /// Перечесление опций сохранения параметров
    /// </summary>
    public enum SaveOptions
    {
        /// <summary>
        /// Нужно создать новый файл конфигурации
        /// </summary>
        CreateNewXml,

        /// <summary>
        /// Нужно модифицировать имеющийся файл конфигурации
        /// </summary>
        ModifyCurrentXml
    }

    /// <summary>
    /// Класс для работы с конфигурацией
    /// </summary>
    public class Settings // Класс одиночка
    {
        // ----- Данные класса -------

        private string settingsPath;
        private List<Parameter> parameters;

        // -------- Свойства ---------

        /// <summary>
        /// список параметров
        /// </summary>
        public Parameter[] Parameters
        {
            get
            {
                return parameters.ToArray();
            }
        }

        /// <summary>
        /// URI файла , в котором храниться конфигурация
        /// </summary>
        public string SettingsFilePath
        {
            get { return settingsPath; }
            set { settingsPath = value; }
        }

        // ------ Конструктор ----------

        protected Settings()
        {
            parameters = new List<Parameter>();
        }

        // ---- Управление параметрами -----

        /// <summary>
        /// Добавить параметр
        /// </summary>
        /// <param name="parameter">Добавляемый параметр</param>
        public void Insert(Parameter parameter)
        {
            parameters.Add(parameter);
        }

        // ------ Создать конфигурацтю -----

        /// <summary>
        /// Получить класс конфигурации
        /// </summary>
        /// <returns></returns>
        public static Settings CreateNewSettings()
        {
            return new Settings();
        }

        // ----- Загрузка --------

        /// <summary>
        /// Загрузка конфигурации из файла
        /// </summary>
        /// <param name="settingsFilePath">URI файла конфигурации</param>
        /// <returns></returns>
        public static Settings Load(string settingsFilePath)
        {
            XmlTextReader reader = null;
            Settings settings = new Settings();
            
            try
            {
                Parameter parameter = null;
                reader = new XmlTextReader(settingsFilePath);

                while (reader.Read())
                {                    
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            parameter = new Parameter(reader.Name);
                            while (reader.MoveToNextAttribute())
                            {
                                Property property = new Property(reader.Name, reader.Value);
                                parameter.Insert(property);
                            }
                            break;

                        case XmlNodeType.Text:

                            if (parameter != null) 
                                parameter.Value = reader.Value;

                            break;

                        case XmlNodeType.EndElement:

                            if (settings != null)
                            {
                                if (parameter != null)
                                {
                                    settings.Insert(parameter);
                                    parameter = null;
                                }
                            }
                            break;
                    }
                }
                settings.SettingsFilePath = settingsFilePath;
                return settings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        // ------ Сохранение -------

        /// <summary>
        /// Сохранение конфигурации в файл
        /// </summary>
        /// <param name="FilePath">URI файла</param>
        /// <param name="Options">Опции сохранения</param>
        public void Save(string FilePath, SaveOptions Options)
        {
            switch (Options)
            {
                case SaveOptions.CreateNewXml:

                    CreateNewSettings(FilePath);
                    break;

                case SaveOptions.ModifyCurrentXml:

                    ModifySettings();
                    break;
            }
        }

        // ----- Создание нового фала ---------
        
        protected void CreateNewSettings(string filePath)
        {
            XmlTextWriter writer = null;
            try
            {
                writer = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteStartElement("configuration");
                foreach (Parameter parameter in parameters)
                {
                    writer.WriteStartElement(parameter.Name);
                    foreach (Property property in parameter.Properties)
                    {
                        writer.WriteAttributeString(property.Name, property.Value);
                    }
                    writer.WriteString(parameter.Value);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        // ------ Замещение старого файла ----------

        protected void ModifySettings()
        {
            XmlDocument settings = new XmlDocument();
            try
            {
                XmlElement root = settings.CreateElement("configuration");
                foreach (Parameter parameter in Parameters)
                {
                    XmlElement element = settings.CreateElement(parameter.Name);
                    foreach (Property property in parameter.Properties)
                    {
                        XmlAttribute attribute = settings.CreateAttribute(parameter.Name);
                        attribute.Value = parameter.Value;

                        element.Attributes.Append(attribute);
                    }
                    element.InnerText = parameter.Value;
                    root.AppendChild(element);
                }
                settings.AppendChild(root);
                settings.Save(settingsPath);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}