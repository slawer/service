using System;
using System.Collections.Generic;

namespace BlockConfiguration.IO
{
    /// <summary>
    /// Тип индикатора
    /// </summary>
    public enum IndicatorType
    {
        /// <summary>
        /// Столбик, 32 деления
        /// </summary>
        Column32,

        /// <summary>
        /// Столбик, 32 деления, биполярный
        /// </summary>
        Column32Bipolar,

        /// <summary>
        /// 3-х значный
        /// </summary>
        ThreeDigit,

        /// <summary>
        /// 4-х значный
        /// </summary>
        FourDigit,

        /// <summary>
        /// 5-значный
        /// </summary>
        FiveDigit,
        
        /// <summary>
        /// Часы
        /// </summary>
        Clock,

        /// <summary>
        /// По умолчанию, нет типа
        /// </summary>
        Default
    }

    /// <summary>
    /// Номер разъема
    /// </summary>
    public enum IndicatorJack
    {
        XP10_CS1, XP10_CS2,
        XP11_CS1, XP11_CS2,
        XP12_CS1, XP12_CS2,
        XP13_CS1, XP13_CS2,
        XP14_CS1, XP14_CS2,
        XP15_CS1, XP15_CS2,
        XP16_CS1, XP16_CS2,
        XP17_CS1, XP17_CS2,

        Default
    }

    /// <summary>
    /// Класс опысывающий смещение двухбитного кода
    /// </summary>
    public class IndicatorOffset
    {
        private byte offsetOfByte = 0;
        private byte offsetOfBits = 0;

        /// <summary>
        /// Инициализирует новый экземпляр класса с параметрами по умолчанию
        /// </summary>
        public IndicatorOffset()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        /// <param name="byteOffset">Значение байта содержащего биты двухбитного кода</param>
        /// <param name="bitsOffset">Смещение битов в указанном байте</param>
        public IndicatorOffset(byte byteOffset, byte bitsOffset)
        {
            offsetOfByte = byteOffset;
            offsetOfBits = bitsOffset;
        }

        /// <summary>
        /// Значение байта
        /// </summary>
        public byte OffsetOfByte
        {
            get { return (byte)(offsetOfByte >> 3); }
            set { offsetOfByte = (byte)(value << 3); }
        }

        /// <summary>
        /// Значение битов
        /// </summary>
        public byte OffsetOfBits
        {
            get { return offsetOfBits; }
            set { offsetOfBits = value; }
        }

        /// <summary>
        /// Итоговое значение
        /// </summary>
        public byte TotalOffset
        {
            get
            {

                byte off__pp = (byte)offsetOfByte;
                //off__pp = (byte)(off__pp << 3);
                off__pp |= (byte)offsetOfBits;

                return off__pp;
            }

            set
            {
                OffsetOfByte = (byte)((value & 0xf8) >> 3);
                OffsetOfBits = (byte)(value & 7);
            }
        }
    }

    /// <summary>
    /// Реализует представление модели индикатора
    /// </summary>
    public class Indicator
    {
        private byte con = 0;           // управляет работой индикатора
        private byte address = 0;       // сетевой адрес обрабатываемых пакетов
        private byte offset = 0;        // смещение индуцируемого значения
        private byte pointPos = 0;      // полодежение дясятичной точки на индикаторе

        private IndicatorOffset offsetThr = null;   // смещение превышения порогов индуцируемым значением
        private IndicatorOffset offsetPp = null;    // Смещение положения десятичной точки на индикаторе

        private IndicatorType indicatorType = IndicatorType.Default;    // тип индикатора

        private float fact = 0.0f;
        private int correctOffset = 0;

        private int thr_min = 0;
        private int thr_max = 0;

        private IndicatorJack jack = IndicatorJack.Default;

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// </summary>
        public Indicator()
        {
            offsetPp = new IndicatorOffset();
            offsetThr = new IndicatorOffset();
        }

        /// <summary>
        /// Управляет работой индикатора
        /// </summary>
        public byte Con
        {
            get { return con; }
            set { con = value; }
        }

        /// <summary>
        /// Сетевой адресс
        /// </summary>
        public byte Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Смещение индуцируемого значения
        /// </summary>
        public byte Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Положение дясятичной точки на индикаторе
        /// </summary>
        public byte PointPosition
        {
            get { return pointPos; }
            set { pointPos = value; }
        }

        /// <summary>
        /// Смещение превышения порогов индуцируемым значением
        /// </summary>
        public IndicatorOffset OffsetThr
        {
            get { return offsetThr; }
        }

        /// <summary>
        /// Смещение положения десятичной точки на индикаторе
        /// </summary>
        public IndicatorOffset OffsetPp
        {
            get { return offsetPp; }
        }

        /// <summary>
        /// Тип индикатора
        /// </summary>
        public IndicatorType IndicatorType
        {
            get { return indicatorType; }
            set { indicatorType = value; }
        }

        /// <summary>
        /// Коэффициент коррекции входных данных
        /// </summary>
        public float Fact
        {
            get { return fact; }
            set { fact = value; }
        }

        /// <summary>
        /// Коррекция смещения входных данных
        /// </summary>
        public int CorrectOffset
        {
            get { return correctOffset; }
            set { correctOffset = value; }
        }

        /// <summary>
        /// Значение нижнего порога
        /// </summary>
        public int Thr_MIN
        {
            get { return thr_min; }
            set { thr_min = value; }
        }

        /// <summary>
        /// Значение верхнего порога
        /// </summary>
        public int Thr_MAX
        {
            get { return thr_max; }
            set { thr_max = value; }
        }

        /// <summary>
        /// Тип индикатора
        /// </summary>
        public IndicatorJack Jack
        {
            get { return jack; }
            set { jack = value; }
        }

        /// <summary>
        /// Возвращяет строку описания типа индикатора
        /// </summary>
        /// <param name="indicatorType">Тип индикатора</param>
        /// <returns>Строка, описывающая тип индикатора</returns>
        public static string GetTypeIndicatorString(IndicatorType indicatorType)
        {
            switch (indicatorType)
            {
                case IndicatorType.Column32: return "Столбик, 32 деления";
                case IndicatorType.Column32Bipolar: return "Столбик, 32 деления биполярный";
                case IndicatorType.ThreeDigit: return "3-х значный";
                case IndicatorType.FourDigit: return "4-х значный";
                case IndicatorType.FiveDigit: return "5-и значный";
                case IndicatorType.Clock: return "Часы";
                case IndicatorType.Default: return "По умолчанию";
            }
            return string.Empty;
        }

        /// <summary>
        /// Создает копию данного объекта
        /// </summary>
        /// <returns></returns>
        public Indicator Clone()
        {
            Indicator ind = new Indicator();

            ind.address = address;
            ind.con = con;
            ind.correctOffset = correctOffset;
            ind.fact = fact;
            ind.indicatorType = indicatorType;
            ind.jack = jack;
            ind.offset = offset;

            ind.offsetPp.OffsetOfBits = offsetPp.OffsetOfBits;
            ind.offsetPp.OffsetOfByte = offsetPp.OffsetOfByte;

            ind.offsetThr.OffsetOfBits = offsetThr.OffsetOfBits;
            ind.offsetThr.OffsetOfByte = offsetThr.OffsetOfByte;

            ind.pointPos = pointPos;
            ind.thr_max = thr_max;
            ind.thr_min = thr_min;

            return ind;
        }
    }

    /// <summary>
    /// Класс, реализующий команду опроса блоком отображения
    /// </summary>
    public class CmdOpros
    {
        private byte netAddress = 0x00;
        private byte sizeBuffer = 0x00;

        /// <summary>
        /// Инициализирует новый экземпляр класса с параметрами по умолчанию
        /// </summary>
        public CmdOpros()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными параметрами
        /// </summary>
        /// <param name="address">Сетевой адресс в команде опроса</param>
        /// <param name="size">Размер запрашиваемного буфера в команде опроса</param>
        public CmdOpros(byte address, byte size)
        {
            netAddress = address;
            sizeBuffer = size;
        }

        /// <summary>
        /// Сетевой адресс в запросе
        /// </summary>
        public byte Address
        {
            get { return netAddress; }
            set { netAddress = value; }
        }

        /// <summary>
        /// Размер запрашиваемого буфера в команде опроса
        /// </summary>
        public byte SizeBuffer
        {
            get { return sizeBuffer; }
            set { sizeBuffer = value; }
        }
    }
    
    /// <summary>
    /// Класс, реализующий блок отображения
    /// </summary>
    public class Block
    {
        private byte address = 0x00;
        private byte speed = 0x00;
        private byte typeCRC = 0x00;
        private byte speedopros = 0x00;

        private List<CmdOpros> cmds = null;

        private bool validateVersion = false;

        private Version version = null;
        private List<Indicator> indicators = null;

        /// <summary>
        /// Инициализирует экземпляр класса
        /// </summary>
        public Block()
        {
            version = new Version();
            indicators = new List<Indicator>();

            cmds = new List<CmdOpros>();
        }

        /// <summary>
        /// Возвращяет копию данного объекта
        /// </summary>
        /// <returns>Копия текущего блока отображения</returns>
        public Block Clone()
        {
            Block b = new Block();

            b.indicators.Clear();
            foreach (Indicator indicator in indicators)
            {
                b.indicators.Add(indicator.Clone());
            }

            foreach (CmdOpros cmd in cmds)
            {
                b.cmds.Add(cmd);
            }

            b.validateVersion = validateVersion;
            b.version = new Version(version.Major, version.Minor);

            b.address = address;
            b.speed = speed;
            b.typeCRC = typeCRC;
            b.speedopros = speedopros;

            return b;
        }

        /// <summary>
        /// Определяет список индикаторов
        /// </summary>
        public List<Indicator> Indicators
        {
            get { return indicators; }
        }

        /// <summary>
        /// Сетевой адрес устройства
        /// </summary>
        public byte Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <summary>
        /// Определяет скорость обмена
        /// </summary>
        public byte Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        /// <summary>
        /// Определяет скорость опроса
        /// </summary>
        public byte SpeedOpros
        {
            get { return speedopros; }
            set { speedopros = value; }
        }

        /// <summary>
        /// Определяет тип контрольной суммы
        /// </summary>
        public byte TypeCRC
        {
            get { return typeCRC; }
            set { typeCRC = value; }
        }

        /// <summary>
        /// Определяет список команд опроса
        /// </summary>
        public List<CmdOpros> Cmds
        {
            get { return cmds; }
        }

        /// <summary>
        /// Определяет версию программы
        /// </summary>
        public Version Version
        {
            get { return version; }
        }

        /// <summary>
        /// Лпределяет учитывать ли версию программы
        /// </summary>
        public bool ValidateVersion
        {
            get { return validateVersion; }
            set { validateVersion = value; }
        }
    }
}