using System;

namespace Platform
{
    /// <summary>
    /// Класс, описывающий пакет , поступивший по TCP от сервера
    /// </summary>
    public class Packet
    {
        /// <summary>
        /// Строка, содержащая пакет, в виде строки
        /// </summary>
        public string packet;

        /// <summary>
        /// Время отправки/прихода пакета по TCP от/к серверу
        /// </summary>
        public DateTime dateReceived;

        /// <summary>
        /// Зарезервированние поле, определящее дополнительную информацию(зависит от конкретного плагина)
        /// </summary>
        public object Token;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="tcpPacket">Пакет в строковом представлении в HEX формате</param>
        /// <param name="dateTimeRaceive">Время создания пакета</param>
        /// <param name="token">Дополнительная информация передаваемая с пакетом</param>
        public Packet(string tcpPacket, DateTime dateTimeRaceive, object token)
        {
            packet = tcpPacket;
            dateReceived = dateTimeRaceive;
            Token = token;
        }
    }
}