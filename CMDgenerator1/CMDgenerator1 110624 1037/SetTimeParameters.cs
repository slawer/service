using System;
using System.Runtime.Serialization;

namespace CMDgenerator1
{
    public static class ParametrConstants
    {
        public static string ConfigName = "\\Config.5f62098f-1e95-4ba5-9da1-4f6d9ddbcd6f.bin";
        public static string NoValidBO = "Команда не определена";
        public static string ErrorSaveConfig = "Возникли ошибки при сохранении конфигурации";
        public static string ErrorLoadConfig = "Возникли ошибки при восстановлении конфигурации";
        public static string ErrorNotConfig = "Не найден файл с конфигурацией";
        public static int MaxAddress = 31;
        public static int MinAddress = 1;
        public static int TopStep = 30;
        public static int TopStep2 = 33;
        public static string hexCodeString = "0123456789abcdefABCDEF";
    }

    /// <summary>
    /// Класс, содержащий настройки программы
    /// </summary>
    [Serializable]
    public class SetTimeParameters
    {
        int jBO1 = 1; string sBO1 = "Команда управления № 1"; string sCMD1 = "012002B020"; // bool bBO1 = false;
        int jBO2 = 2; string sBO2 = "Команда управления № 2"; string sCMD2 = "012002B020"; // bool bBO2 = false;
        int jBO3 = 3; string sBO3 = "Команда управления № 3"; string sCMD3 = "012002B020"; // bool bBO3 = false;
        int jBO4 = 4; string sBO4 = "Команда управления № 4"; string sCMD4 = "012002B020"; // bool bBO4 = false;
        int jBO5 = 5; string sBO5 = "Команда управления № 5"; string sCMD5 = "012002B020"; // bool bBO5 = false;
        int jBO6 = 6; string sBO6 = "Команда управления № 6"; string sCMD6 = "012002B020"; // bool bBO6 = false;
        int jBO7 = 7; string sBO7 = "Команда управления № 7"; string sCMD7 = "012002B020"; // bool bBO7 = false;
        
        bool Zvr = false;

        /// <summary>
        /// Флаг "Применить настройки"
        /// </summary>
        public bool CodeExit { get { return Zvr; } set { Zvr = value; } }

        /// <summary>
        /// Проверка адреса Устройства на попадание в диапазон допустимых адресов и текста команды на корректность
        /// </summary>
        /// <param name="number">Номер Устройства, адрес которого проверяется</param>
        /// <returns>true если адрес попадает в диапазон допустимых и команда корректна</returns>
        public bool isValidBO(int number)
        {
            int addr = getAdrBO(number);
            if ( addr < ParametrConstants.MinAddress || addr > ParametrConstants.MaxAddress) 
                return false;
            else
                return true;
        }

        /// <summary>
        /// Чтение адреса Устройства
        /// </summary>
        /// <param name="number">Номер Устройства (от 1 до 7)</param>
        /// <returns>Адрес Устройства</returns>
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
        /// Чтение наименования команды
        /// </summary>
        /// <param name="number">Номер Устройства (от 1 до 7)</param>
        /// <returns>Наименование команды</returns>
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
        /// Чтение текста команды
        /// </summary>
        /// <param name="number">Номер Устройства (от 1 до 7)</param>
        /// <returns>Текст команды</returns>
        public string getNameCMD(int number)
        {
            switch (number)
            {
                case 1:
                    return sCMD1;
                case 2:
                    return sCMD2;
                case 3:
                    return sCMD3;
                case 4:
                    return sCMD4;
                case 5:
                    return sCMD5;
                case 6:
                    return sCMD6;
                case 7:
                    return sCMD7;
                default:
                    return "";
            }
        }

        /// <summary>
        /// Установка адреса Устройства
        /// </summary>
        /// <param name="number">Номер Устройства (от 1 до 7)</param>
        /// <param name="addr">Адрес Устройства</param>
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
        /// Установка имени команды
        /// </summary>
        /// <param name="number">Номер Устройства (от 1 до 7)</param>
        /// <param name="name">Имя команды</param>
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

        /// <summary>
        /// Установка текста команды
        /// </summary>
        /// <param name="number">Номер Устройства (от 1 до 7)</param>
        /// <param name="name">Текст команды</param>
        public void setNameCMD(int number, string name)
        {
            switch (number)
            {
                case 1:
                    sCMD1 = name;
                    break;
                case 2:
                    sCMD2 = name;
                    break;
                case 3:
                    sCMD3 = name;
                    break;
                case 4:
                    sCMD4 = name;
                    break;
                case 5:
                    sCMD5 = name;
                    break;
                case 6:
                    sCMD6 = name;
                    break;
                case 7:
                    sCMD7 = name;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Проверка текста команды
        /// </summary>
        /// <param name="number">Номер Устройства (от 1 до 7)</param>
        /// <returns>true если команда корректна</returns>
        public bool isValidCMD(int number)
        {
            string name = this.getNameCMD(number);
            
            if (name.Length == 0) return false;
            if ((name.Length % 2) != 0) return false;

            char s,s1;
            for (int j = 0; j < name.Length; j++)
            {
                s = name[j];

                int j1;
                for (j1 = 0; j1 < ParametrConstants.hexCodeString.Length; j1++)
                {
                    s1 = ParametrConstants.hexCodeString[j1];
                    if (s1 == s) break;
                }
                if(j1 == ParametrConstants.hexCodeString.Length) return false;
            }
                
            return true;
        }
    }
}
