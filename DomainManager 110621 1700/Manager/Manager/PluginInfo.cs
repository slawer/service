using System;

namespace Platform
{
    /// <summary>
    /// Класс содержащий информацию о плагине
    /// </summary>
    class PluginInfo
    {
        public int number = 0;
        public string Name = string.Empty;
        public string Author = string.Empty;
        public string Description = string.Empty;
        public Version version;

        public PluginInfo(Version v, int n)
        {
            number = n;
            version = v;
        }
    }
}
