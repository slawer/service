using System;
using Platform;
using Calibration;

namespace Calibration.CalibrationPlugin.GUI
{
    public class ObjectCurrentState
    {
        // данные класса

        private Eprom eprom = null;

        private CalibrationTableHandle[] handles = null;
        private CalibrationTableHandle selectedHandle = null;

        private Sync syncker = null;
        private bool packetTurnOn = false;

        public ObjectCurrentState()
        {
            syncker = new Sync();
        }

        public bool PacketsTurnOn
        {
            get { return packetTurnOn; }
            set { packetTurnOn = value; }
        }

        // свойства класса

        /// <summary>
        /// Хранит Eprom
        /// </summary>
        public Eprom Eprom
        {
            get { return eprom; }
            set
            {
                if (!syncker.Blocked)
                {
                    eprom = value;
                    syncker.Relese();
                }
                else
                    throw new Exception();
            }
        }

        /// <summary>
        /// Определяет таблицу описателей калибруемых каналов
        /// </summary>
        public CalibrationTableHandle[] CalibrationTableHandles
        {
            get { return handles; }
            set
            {
                if (!syncker.Blocked)
                {
                    handles = value;
                    syncker.Relese();
                }
                else
                    throw new Exception();
            }
        }

        /// <summary>
        /// Определяет выбранный калибровочный канал
        /// </summary>
        public CalibrationTableHandle SelectedCalibrationTable
        {
            get { return selectedHandle; }

            set 
            {
                if (!syncker.Blocked)
                {
                    selectedHandle = value;
                    syncker.Relese();
                }
                else
                    throw new Exception();
            }
        }
    }
}