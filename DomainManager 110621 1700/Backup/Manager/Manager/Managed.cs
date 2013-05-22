using System;
using System.Collections.Generic;

namespace Platform
{
    /// <summary>
    /// Структура, описающая плагин, его состояние и хранящее очередь пакетов на обработку
    /// </summary>
    public class Managed
    {
        /// <summary>
        /// домен приложения, в который занружен плагин
        /// </summary>
        public AppDomain domain;

        /// <summary>
        /// Плагин
        /// </summary>
        public IPlugin plugin;

        /// <summary>
        /// Максимальное время на обработку пакета плагином
        /// </summary>
        public long downTime = 0;

        /// <summary>
        /// Время которое плагин выполняет обработку пакета
        /// </summary>
        public long sendPacketTime = 0;

        /// <summary>
        /// Количество отправленных пакетов плагину
        /// </summary>
        public long sendPackets = 0;

        /// <summary>
        /// Количество обработанных пакетов плагином
        /// </summary>
        public long processedPackets = 0;

        /// <summary>
        /// Список пакетов, которые необходимо передать плагину на обработку
        /// </summary>
        public List<Packet> Packets;

        /// <summary>
        /// Состояние плагина
        /// </summary>
        public ManagedState State = ManagedState.Wait;
          
        /// <summary>
        /// Делегат, содержащий метод обработки пакета, который определил плагин
        /// </summary>
        public Sender InvokeAsync = null;

        /// <summary>
        /// Объек хранищий асинхронный вызов процедуры обработки пакета
        /// </summary>
        public IAsyncResult iAsync = null;

        /// <summary>
        /// Уникальный идентификатор плагина
        /// </summary>
        public string key = string.Empty;

        // ------ Конструктор -----

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="Plugin">Плагин</param>
        public Managed(IPlugin Plugin)
        {
            plugin = Plugin;
            InvokeAsync = new Sender(plugin.Process);

            Packets = new List<Packet>();
            key = Generator.GeneratePluginKey();
        }        
    }

    /// <summary>
    /// Делегат описывающий вызов процедуры обработки 
    /// </summary>
    /// <param name="packet"></param>
    public delegate void Sender(Packet packet);
    
    /// <summary>
    /// Состояние плагина
    /// </summary>
    public enum ManagedState
    {
        /// <summary>
        /// Ожидает получение пакета
        /// </summary>
        Wait,

        /// <summary>
        /// Находится в процессе обработки пакета
        /// </summary>
        Processed,

        /// <summary>
        /// Переполненна очередь пакетов ожидающих обработку
        /// </summary>
        Overflow,

        /// <summary>
        /// Не отвечает. превышен лимит времени обработки одного пакета
        /// </summary>
        NotResponding,

        /// <summary>
        /// По умолчанию
        /// </summary>
        Default
    }
}