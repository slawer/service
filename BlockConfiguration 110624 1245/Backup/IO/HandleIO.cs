using System;
using Platform;

namespace BlockConfiguration.IO
{
    public class HandleIO
    {
        private Eprom eprom = null;                 // считанный Eprom с устройства
        private Block block = null;                 // Запакованные данные, которые были считанны с Eprom устройсва
        private Version programmVersion = null;     // версия программы устройства
        private ushort crc16 = 0xff;                // контрольная суппа Eprom устройства

        /// <summary>
        /// Считанный Eprom с устройства
        /// </summary>
        public Eprom Eprom
        {
            get { return eprom; }
            set { eprom = value; }
        }

        /// <summary>
        /// Блок отображения
        /// </summary>
        public Block VisionBlock
        {
            get { return block; }
            set { block = value; }
        }

        /// <summary>
        /// Версия программы устройства
        /// </summary>
        public Version ProgrammVersion
        {
            get { return programmVersion; }
            set { programmVersion = value; }
        }

        /// <summary>
        /// Контрольная суппа Eprom устройства
        /// </summary>
        public ushort CRC16
        {
            get { return crc16; }
            set { crc16 = value; }
        }

        /// <summary>
        /// Код последней ошибки
        /// </summary>
        public int LastError
        {
            get { return programmVersion.Build; }
        }

        /// <summary>
        /// Количество аварийных перезапусков
        /// </summary>
        public int CountRestart
        {
            get { return programmVersion.Revision; }
        }
    }
}