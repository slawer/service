using System;
using System.Drawing;

namespace Platform
{
    public interface IPlugin
    {
        // ----- свойства -------

        /// <summary>
        /// Имя плагина
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Автор плагина
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Описание плагина
        /// </summary>
        string Description { get; }


        /// <summary>
        /// Версия плагина
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Свойство определяющее, необходимо ли выполнять посылку плагину пакеты на обработку
        /// </summary>
        bool Send { get; }

        // ------ настройка свойств для интерфейса ----

        /// <summary>
        /// Строка, которая будет отображаться в главном меню, при нажатии на котором будет активизирован данный плагин 
        /// </summary>
        string MainMenuString { get; }

        /// <summary>
        /// Строка, которая будет отображаться в контекстном, при нажатии на котором будет активизирован данный плагин 
        /// </summary>
        string ContextMenuString { get; }

        /// <summary>
        /// Иконка плагина
        /// </summary>
        Icon Icon { get; }

        /// <summary>
        /// Строка, которая будет отображаться рялом с иконкой плагина
        /// </summary>
        string FaceString { get; }

        // ---- методы -------

        /// <summary>
        /// Метод , выполняющий обработку поступившего пакета
        /// </summary>
        /// <param name="packet">Пакет</param>
        void Process(Packet packet);

        /// <summary>
        /// Метод, вызываемый платформой, после того как плагин будет загруженн
        /// </summary>
        /// <param name="application">Указатель на сервисы, которые предоставляет платформа плагину</param>
        void Initialize(IApplication application);

        /// <summary>
        /// Метод вызываемый перед тем как плагин будет выгружен
        /// </summary>
        void Dispose();

        /// <summary>
        /// Метод, вызываемый при выборе плагина
        /// </summary>
        void Activate();
    }
}