using System;
using System.Windows.Forms;

namespace Platform
{
    /// <summary>
    /// Интерфей формы, взаимодействующей с платформой
    /// </summary>
    public interface IForm
    {
        /// <summary>
        /// Послать пакет по TCP
        /// </summary>
        /// <param name="packet">Пакет</param>
        void SendPacket(Packet packet);

        /// <summary>
        /// Добавить плагин на форму
        /// </summary>
        /// <param name="plugin">Плагин</param>
        /// <param name="key">Уникальный идентификатор плагина</param>
        void InsertPluginToGiu(IPlugin plugin, string key);
    }
}