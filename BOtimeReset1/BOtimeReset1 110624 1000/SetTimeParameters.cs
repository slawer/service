using System;
using System.Runtime.Serialization;

namespace BOtimeReset1
{
    public static class ParametrConstants
    {
        public static string ConfigName = "\\Config.76f56c7b-f800-4bf6-b17f-cd2e256ee6be.bin";
        public static string NoValidBO = "Блок не определён";
        public static string ErrorSaveConfig = "Возникли ошибки при сохранении конфигурации";
        public static string ErrorLoadConfig = "Возникли ошибки при восстановлении конфигурации";
        public static string ErrorNotConfig = "Не найден файл с конфигурацией";
        public static int MaxAddress = 31;
        public static int MinAddress = 1;
        public static int TopStep = 23;
        public static int TopStep2 = 13;
    }

    /// <summary>
    /// Класс, содержащий настройки программы
    /// </summary>
    [Serializable]
    public class SetTimeParameters
    {
        int jBO1 = 1; string sBO1 = "Блок Отображения № 1"; bool bBO1 = false;
        int jBO2 = 2; string sBO2 = "Блок Отображения № 2"; bool bBO2 = false;
        int jBO3 = 3; string sBO3 = "Блок Отображения № 3"; bool bBO3 = false;
        int jBO4 = 4; string sBO4 = "Блок Отображения № 4"; bool bBO4 = false;
        int jBO5 = 5; string sBO5 = "Блок Отображения № 5"; bool bBO5 = false;
        int jBO6 = 6; string sBO6 = "Блок Отображения № 6"; bool bBO6 = false;
        int jBO7 = 7; string sBO7 = "Блок Отображения № 7"; bool bBO7 = false;
        
        bool Zvr = false;
        bool OldCMD = false;
        bool ResetBO = false;

        /// <summary>
        /// Флаг "Применить настройки"
        /// </summary>
        public bool CodeExit { get { return Zvr; } set { Zvr = value; } }

        /// <summary>
        /// Флаг "Старый формат команды синхронизации"
        /// </summary>
        public bool OldFormatCMD { get { return OldCMD; } set { OldCMD = value; } }

        /// <summary>
        /// Флаг "Рестарт БО после выдачи строки синхронизации"
        /// </summary>
        public bool ResetBOAfterSet { get { return ResetBO; } set { ResetBO = value; } }

        /// <summary>
        /// Проверка адреса БО на попадание в диапазон допустимых адресов
        /// </summary>
        /// <param name="number">Номер БО, адрес которого проверяется</param>
        /// <returns>true если адрес попадает в диапазон допустимых</returns>
        public bool isValidBO(int number)
        {
            int addr = getAdrBO(number);
            if ( addr < ParametrConstants.MinAddress || addr > ParametrConstants.MaxAddress) 
                return false;
            else
                return true;
        }

        /// <summary>
        /// Чтение флага разрешения синхронизации БО
        /// </summary>
        /// <param name="number">Номер БО (от 1 до 7)</param>
        /// <returns>true если синхронизация данному БО разрешена</returns>
        public bool getFlagBO(int number)
        {
            switch (number)
            {
                case 1:
                    return bBO1;
                case 2:
                    return bBO2;
                case 3:
                    return bBO3;
                case 4:
                    return bBO4;
                case 5:
                    return bBO5;
                case 6:
                    return bBO6;
                case 7:
                    return bBO7;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Чтение адреса БО
        /// </summary>
        /// <param name="number">Номер БО (от 1 до 7)</param>
        /// <returns>Адрес БО</returns>
        public int getAdrBO(int number) 
        {
            switch (number)
            {
                case 1:
                    return jBO1;
                case 2:
                    return jBO2;
                case 3:
                    return jBO3;
                case 4:
                    return jBO4;
                case 5:
                    return jBO5;
                case 6:
                    return jBO6;
                case 7:
                    return jBO7;
                default:
                    return 0;
            }
        }
            
        /// <summary>
        /// Чтение наименования БО
        /// </summary>
        /// <param name="number">Номер БО (от 1 до 7)</param>
        /// <returns>Наименование БО</returns>
        public string getNameBO(int number) 
        {
            switch (number)
            {
                case 1:
                    return sBO1;
                case 2:
                    return sBO2;
                case 3:
                    return sBO3;
                case 4:
                    return sBO4;
                case 5:
                    return sBO5;
                case 6:
                    return sBO6;
                case 7:
                    return sBO7;
                default:
                    return "";
            }
        }

        /// <summary>
        /// Установка флага разрешения синхронизации БО
        /// </summary>
        /// <param name="number">Номер БО (от 1 до 7)</param>
        /// <param name="flag">Состояние флага</param>
        public void setFlagBO(int number, bool flag)
        {
            switch (number)
            {
                case 1:
                    bBO1 = flag;
                    break;
                case 2:
                    bBO2 = flag;
                    break;
                case 3:
                    bBO3 = flag;
                    break;
                case 4:
                    bBO4 = flag;
                    break;
                case 5:
                    bBO5 = flag;
                    break;
                case 6:
                    bBO6 = flag;
                    break;
                case 7:
                    bBO7 = flag;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Установка адреса БО
        /// </summary>
        /// <param name="number">Номер БО (от 1 до 7)</param>
        /// <param name="addr">Адрес БО</param>
        public void setAdrBO(int number, int addr)
        {
            switch (number)
            {
                case 1:
                    jBO1 = addr;
                    break;
                case 2:
                    jBO2 = addr;
                    break;
                case 3:
                    jBO3 = addr;
                    break;
                case 4:
                    jBO4 = addr;
                    break;
                case 5:
                    jBO5 = addr;
                    break;
                case 6:
                    jBO6 = addr;
                    break;
                case 7:
                    jBO7 = addr;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Установка имени БО
        /// </summary>
        /// <param name="number">Номер БО (от 1 до 7)</param>
        /// <param name="name">Имя БО</param>
        public void setNameBO(int number, string name)
        {
            switch (number)
            {
                case 1:
                    sBO1 = name;
                    break;
                case 2:
                    sBO2 = name;
                    break;
                case 3:
                    sBO3 = name;
                    break;
                case 4:
                    sBO4 = name;
                    break;
                case 5:
                    sBO5 = name;
                    break;
                case 6:
                    sBO6 = name;
                    break;
                case 7:
                    sBO7 = name;
                    break;
                default:
                    break;
            }
        }
    }
}
