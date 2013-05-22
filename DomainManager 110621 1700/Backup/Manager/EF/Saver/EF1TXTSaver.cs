using System;
using System.IO;

namespace Platform
{
    /// <summary>
    /// Класс выполняющий сохранение EPROM устройства в файл в текстовом формате
    /// </summary>
    class EF1TXTSaver : IEFSaver
    {
        /// <summary>
        /// Выполняет сохранение EPROM устройства в файл
        /// </summary>
        /// <param name="filePath">URI файла</param>
        /// <param name="eprom">EPROM устройства который необходимо сохранить</param>
        public void Save(string filePath, Eprom eprom)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(filePath);

                int pageNumber = 0;
                int lineNumber = -1;

                foreach (Page page in eprom.Pages)
                {
                    pageNumber += 1;
                    lineNumber = -1;

                    string pageString = string.Format("{0:d2}", pageNumber);
                    foreach (Line line in page.Lines)
                    {
                        lineNumber += 1;
                        string totalpageString = pageString + string.Format("{0:X}", lineNumber) + "0   ";
                        
                        string lineStringValue = string.Empty;
                        foreach (var item in line.line) lineStringValue += string.Format("{0:X2}", item) + " ";
                        
                        writer.WriteLine(totalpageString + lineStringValue);
                    }
                    writer.WriteLine(" ");
                }
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