using System;
using System.Collections.Generic;
using System.Text;

namespace Calibration
{
    public enum LastError
    {
        Success,
        Error,
        Default
    }

    /// <summary>
    /// Таблица калибровки
    /// </summary>
    public class CalibrationTable
    {
        private byte name = 0x00;   // имя таблицы
        private byte size = 0x00;   // размер таблицы

        private List<CalibrationKoefs> koefs = null;    // калибровочные коэффициетны
        private List<Parameter> parameters = null;      // калибровочный значения

        private List<Parameter> savedTable = null;      // сохраненная таблица калибровки

        /// <summary>
        /// Инициализирует новый экземпляр класса с параметрами по умолчанию
        /// </summary>
        public CalibrationTable()
            : this(0x00, 0x00)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными параметрами
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="size">Размер таблицы в байтах</param>
        public CalibrationTable(byte name, byte size)
        {
            this.name = name;
            this.size = size;

            parameters = new List<Parameter>();
            koefs = new List<CalibrationKoefs>();
        }

        /// <summary>
        /// Определяет имя таблицы
        /// </summary>
        public byte Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Определяет размер таблицы
        /// </summary>
        public byte Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// Определяет точки калибровки
        /// </summary>
        public List<Parameter> Parameters
        {
            get { return parameters; }
        }

        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        public ushort MaxCalibration
        {
            get
            {
                ushort max = ushort.MinValue;

                for (int i = 1; i < parameters.Count - 1; i++)
                {
                    if (parameters[i].Calibrated > max) max = parameters[i].Calibrated;
                }
                return max;
            }
        }

        /// <summary>
        /// Максимальное значение сигнала
        /// </summary>
        public ushort MaxPhysic
        {
            get
            {
                ushort max = ushort.MinValue;

                for (int i = 1; i < parameters.Count - 1; i++)
                {
                    if (parameters[i].Physical > max) max = parameters[i].Physical;
                }
                return max;
            }
        }

        /// <summary>
        /// Вычисляет коэффициэнты таблицы калибровки
        /// </summary>
        public void CalculateKoef()
        {
            if (parameters.Count >= 2)
            {
                koefs.Clear();
                for (int index = 0; index < parameters.Count - 1; index++)
                {
                    double param = parameters[index].Calibrated;
                    double param1 = parameters[index + 1].Calibrated;

                    double sig = parameters[index].Physical;
                    double sig1 = parameters[index + 1].Physical;

                    double a = (param1 - param) / (sig1 - sig);


                    double paramB = parameters[index + 1].Calibrated;
                    double paramB1 = parameters[index + 1].Physical;

                    double b = paramB - (paramB1 * a);

                    CalibrationKoefs k = new CalibrationKoefs(a, b);
                    koefs.Add(k);
                }
            }
        }

        /// <summary>
        /// Хрен знает что это вычисляет, но что то вычисляет
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int CalculateFromInPacket(ushort parameter)
        {
            for (int i = 0; i < parameters.Count - 1; i++)
            {
                if (parameters[i].Calibrated <= parameter && parameter <= parameters[i + 1].Calibrated)
                {
                    double par = parameter;
                    double sig = (par - koefs[i].b);

                    if (koefs[i].a == 0)
                    {
                        sig = koefs[i].b;
                    }
                    else
                        sig = sig / koefs[i].a;

                    try
                    {
                        return Convert.ToInt32(sig);
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// добавить точку в таблицу
        /// </summary>
        /// <param name="parameter">Добавляемая точка</param>
        /// <returns></returns>
        public LastError InsertPoint(Parameter parameter)
        {
            for (int i = 0; i < parameters.Count - 1; i++)
            {
                if (parameters[i].Physical <= parameter.Physical && parameter.Physical <= parameters[i + 1].Physical)
                {
                    if (parameters[i].Calibrated <= parameter.Calibrated && parameter.Calibrated <= parameters[i + 1].Calibrated)
                    {
                        parameters.Insert(i + 1, parameter);
                        return LastError.Success;
                    }
                }
            }
            return LastError.Error;
        }

        /// <summary>
        /// Сбросить таблицу калибровки
        /// </summary>
        public void ResetTable()
        {
            parameters.Clear();
            Parameter[] prams = { new Parameter(0, 0), new Parameter(ushort.MaxValue, ushort.MaxValue) };
            parameters.AddRange(prams);
        }

        /// <summary>
        /// Сохранить таблицу калибровки
        /// </summary>
        public void SaveTable()
        {
            if (parameters != null)
            {
                if (savedTable == null) savedTable = new List<Parameter>();

                savedTable.Clear();
                savedTable.AddRange(parameters.ToArray());
            }
        }

        /// <summary>
        /// Верниться к исходной таблице калибровки
        /// </summary>
        public void RestoreTable()
        {
            if (savedTable != null)
            {
                if (parameters != null)
                {
                    parameters.Clear();
                    parameters.AddRange(savedTable);
                }
            }
        }

        /// <summary>
        /// Расчитать крайние точки
        /// </summary>
        public void CalculateExtremePoints()
        {
            try
            {
                if (parameters != null)
                {
                    if (parameters.Count >= 4)
                    {
                        // вычисляем первую крайнюю точку
                        double a = (((double)parameters[2].Calibrated - (double)parameters[1].Calibrated) /
                            ((double)parameters[2].Physical - (double)parameters[1].Physical));

                        double b = (double)parameters[1].Calibrated - (a * (double)parameters[1].Physical);

                        int c = 0;
                        if (b < 0)
                        {
                            c = -(int)Math.Round((b / a));

                            if (c < parameters[1].Physical)
                            {
                                parameters[0].Physical = (ushort)c;
                            }
                            else
                                parameters[0].Physical = 0;

                            parameters[0].Calibrated = 0;

                        }
                        else
                        {
                            parameters[0].Physical = 0;
                            parameters[0].Calibrated = (ushort)b;
                        }

                        // вычисляем вторую крайнюю точку
                        
                        a = (((double)parameters[parameters.Count - 2].Calibrated - (double)parameters[parameters.Count - 3].Calibrated) /
                                ((double)parameters[parameters.Count - 2].Physical - (double)parameters[parameters.Count - 3].Physical));

                        b = (double)parameters[parameters.Count - 3].Calibrated - (a * (double)parameters[parameters.Count - 3].Physical);

                        if (a == 0)
                        {
                            parameters[parameters.Count - 1].Physical = ushort.MaxValue;
                            parameters[parameters.Count - 1].Calibrated = (ushort)Math.Round(b);
                        }
                        else
                        {

                            c = (int)Math.Round(((double)ushort.MaxValue - b) / a);

                            if (c > (int)ushort.MaxValue)
                            {
                                ushort param = (ushort)Math.Round(a * (double)ushort.MaxValue + b);

                                parameters[parameters.Count - 1].Physical = ushort.MaxValue;
                                parameters[parameters.Count - 1].Calibrated = param;
                            }
                            else
                            {
                                if (parameters[parameters.Count - 2].Physical != (ushort)c)
                                {
                                    parameters[parameters.Count - 1].Physical = (ushort)c;
                                }
                                else
                                    parameters[parameters.Count - 1].Physical = ushort.MaxValue;

                                parameters[parameters.Count - 1].Calibrated = ushort.MaxValue;
                            }
                        }                        
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// привести таблицу к состоянию без расчета крайних точек
        /// </summary>
        public void RestoreExtremePoints()
        {
            if (parameters != null)
            {
                if (parameters.Count >= 2)
                {
                    parameters[0].Physical = 0;
                    parameters[0].Calibrated = 0;

                    parameters[parameters.Count - 1].Physical = ushort.MaxValue;
                    parameters[parameters.Count - 1].Calibrated = ushort.MaxValue;
                }
            }
        }

        /// <summary>
        /// Определяет равныли крайние точки значению по умолчанию
        /// </summary>
        public bool IsExtremCalculated
        {
            get
            {
                if (parameters != null)
                {
                    if (parameters.Count >= 2)
                    {
                        if ( (parameters[0].Calibrated == 0) && (parameters[0].Physical == 0))
                        {
                            if (parameters[parameters.Count - 1].Calibrated == ushort.MaxValue && parameters[parameters.Count - 1].Physical == ushort.MaxValue)
                            {
                                return false;
                            }
                            else
                                return true;
                        }
                        else
                            return true;
                    }
                    else
                        throw new Exception("Количество параметров в таблице калибровке мало!");
                }
                throw new Exception("Не создана таблица калибровки");
            }
        }

        /// <summary>
        /// Поределяет корректны ли расчитаны крайние точки
        /// </summary>
        public bool IsValidExtremPoints
        {
            get
            {
                if (IsExtremCalculated)
                {
                    SaveTable();

                    CalculateKoef();
                    CalculateExtremePoints();

                    if (savedTable.Count >= 2)
                    {
                        if (savedTable[0].Calibrated == parameters[0].Calibrated && savedTable[0].Physical == parameters[0].Physical)
                        {
                            if (savedTable[savedTable.Count - 1].Calibrated == parameters[parameters.Count - 1].Calibrated &&
                                    savedTable[savedTable.Count - 1].Physical == parameters[parameters.Count - 1].Physical)
                            {
                                RestoreTable();
                                return true;
                            }
                            RestoreTable();
                            return false;
                        }
                        else
                        {
                            RestoreTable();
                            return false;                            
                        }
                    }
                    return false;
                }
                else
                    throw new Exception("Нету крайних точек");
            }
        }

        /// <summary>
        /// Сбросить крайние точки в значение по умолчанию
        /// </summary>
        public void ClearExtremPoints()
        {
            if (parameters != null)
            {
                if (parameters.Count >= 2)
                {
                    parameters[0].Calibrated = ushort.MinValue; parameters[0].Physical = ushort.MinValue;
                    parameters[parameters.Count - 1].Calibrated = ushort.MaxValue;
                    parameters[parameters.Count - 1].Physical = ushort.MaxValue;
                }
            }
        }
    }

    /// <summary>
    /// Калибровочный параметр
    /// </summary>
    public class Parameter
    {
        private ushort physical = 0x0000;       // физическое значени
        private ushort calibrated = 0x0000;     // откалиброванное значение        

        /// <summary>
        /// Инициализирует новый экземпляр класса с параметрами по умолчанию
        /// </summary>
        public Parameter()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными параметрами
        /// </summary>
        /// <param name="Physical">Физическое значени</param>
        /// <param name="Calibrated">Откалиброванное значение</param>
        public Parameter(ushort Physical, ushort Calibrated)
        {
            physical = Physical;
            calibrated = Calibrated;
        }

        /// <summary>
        /// Определяет физическое значени
        /// </summary>
        public ushort Physical
        {
            get { return physical; }
            set { physical = value; }
        }

        /// <summary>
        /// Определяет откалиброванное значение
        /// </summary>
        public ushort Calibrated
        {
            get { return calibrated; }
            set { calibrated = value; }
        }
    }

    /// <summary>
    /// Калибровочный коэффициэнт
    /// </summary>
    public class CalibrationKoefs
    {
        public double a;
        public double b;

        public CalibrationKoefs(double A, double B) { a = A; b = B; }
    }
}