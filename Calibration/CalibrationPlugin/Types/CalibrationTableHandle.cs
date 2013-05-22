using System;
using System.Collections.Generic;

namespace Calibration
{
    /// <summary>
    /// Описатель калибровочного канала
    /// </summary>
    public class CalibrationTableHandle
    {
        private byte nameParameter = 0xff;
        private byte offsetParameter = 0xff;

        private CalibrationTable calibrationTable = null;

        /// <summary>
        /// Размер в байтах, который занимает данный описатель
        /// </summary>
        public const int SizeInTable = 0x02;

        /// <summary>
        /// Инициализирует новый экземпляр класса с параметрами по умолчанию
        /// </summary>
        public CalibrationTableHandle()
            : this(0xff, 0xff)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными параметрами
        /// </summary>
        /// <param name="name">Имя канала</param>
        /// <param name="offset">Смещение канала в битах</param>
        public CalibrationTableHandle(byte name, byte offset)
        {
            nameParameter = name;
            offsetParameter = offset;
        }

        /// <summary>
        /// Определяет имя калибровочного канала
        /// </summary>
        public byte Name
        {
            get { return nameParameter; }
            set { nameParameter = value; }
        }

        /// <summary>
        /// Определяет смещение калибровочного канала в таблице калибровки
        /// </summary>
        public byte Offset
        {
            get { return offsetParameter; }
            set { offsetParameter = value; }
        }

        /// <summary>
        /// Определяет смещение в пакете
        /// </summary>
        public byte OffsetInPacket
        {
            get { return (byte)(offsetParameter >> 3); }
        }

        /// <summary>
        /// Определяет таблицу калибровки для текущего описателя калибровочного параметра
        /// </summary>
        public CalibrationTable CalibrationTable
        {
            get { return calibrationTable; }
            set { calibrationTable = value; }
        }
    }
}