using System;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;

namespace Platform
{
    /// <summary>
    /// Класс выполняющий загрузку EPROM устройства из файла
    /// </summary>
    class EF2XMLOLDLoader : IEFLoader 
    {
        /// <summary>
        /// Загружает EPROM устройства из файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns>Загруженный EPROM устройства или же null, если загрузить EPROM не удалось</returns>
        public Eprom Load(string filePath)
        {
            Eprom eprom = null;
            XmlTextReader reader = null;
            try
            {
                eprom = new Eprom();
                reader = new XmlTextReader(filePath);

                int pageIndex = -1;
                int lineIndex = -1;

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            switch (reader.Name)
                            {
                                case "Page":

                                    pageIndex += 1;
                                    lineIndex = -1;
                                    break;

                                case "Value":

                                    lineIndex += 1;
                                    break;
                            }
                            break;

                        case XmlNodeType.Text:

                            string lineValue = reader.Value;
                            int offset = lineIndex * 16;

                            for (int i = 0; i < lineValue.Length / 2; i++)
                            {
                                string sByte = lineValue.Substring(i * 2, 2);
                                eprom[pageIndex][offset] = (byte)(int.Parse(sByte, NumberStyles.AllowHexSpecifier));
                                offset += 1;
                            }
                            break;

                        case XmlNodeType.EndElement:

                            break;
                    }
                }
                return eprom;
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
    }
}