using System;

namespace Platform
{
    public static class Generator
    {
        private static int keyNumber = 1;
        private static int domainNumber = 1;

        private static string keyTemplateName = "pluginKey";
        private static string domainTamplateName = "pluginDomain";

        // ---- генерация ключа -------

        /// <summary>
        /// Генерирует уникальный описатель плагина
        /// </summary>
        /// <returns>Строка идентификатор</returns>
        public static string GeneratePluginKey()
        {
            string keyName = keyTemplateName + keyNumber.ToString();
            keyNumber += 1;
            return keyName;
        }

        /// <summary>
        /// Генерирует уникальный описатель домена
        /// </summary>
        /// <returns>Строка идентификатор</returns>
        public static string GenerateDomainName()
        {

            string domainName = (domainTamplateName + domainNumber.ToString());
            domainNumber += 1;
            return domainName;
        }

    }
}