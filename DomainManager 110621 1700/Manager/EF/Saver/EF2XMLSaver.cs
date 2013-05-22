using System;
using System.IO;
using System.Xml;

namespace Platform
{
    /// <summary>
    /// Класс реализующий интерфейс записа EPROM устройства в файл
    /// </summary>
    class EF2XMLSaver : IEFSaver
    {
        /// <summary>
        /// Выполняет сохранение EPROM устройства в файл
        /// </summary>
        /// <param name="filePath">URI файла</param>
        /// <param name="eprom">EPROM устройства который необходимо сохранить</param>
        public void Save(string filePath, Eprom eprom)
        {
            XmlTextWriter writer = null;

            try
            {
                writer = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteStartElement("eprom");

                foreach (Page page in eprom.Pages)
                {
                    writer.WriteStartElement("page");
                    foreach (Line line in page.Lines)
                    {
                        writer.WriteElementString("line", line.ToString());
                    }
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
    }
}