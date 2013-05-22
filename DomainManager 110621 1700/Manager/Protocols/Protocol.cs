using System;

namespace Platform
{
    /// <summary>
    /// Перечесление, содержащее версии протокола
    /// </summary>
    public enum ProtocolVersion
    {
        /// <summary>
        /// Первая версия
        /// </summary>
        x100
    }

    class Protocol
    {
        protected Protocol()
        {
        }

        // ------ одиночка -------

        /// <summary>
        /// Получить протокол
        /// </summary>
        /// <param name="version">версия протокола</param>
        /// <returns></returns>
        public static IProtocol GetProtocol(ProtocolVersion version)
        {
            switch (version)
            {
                case ProtocolVersion.x100:

                    return VersionX100.CreateProtocol();

                default:

                    return null;
            }
        }
    }
}