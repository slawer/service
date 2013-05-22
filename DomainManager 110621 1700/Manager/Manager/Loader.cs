using System;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Collections.Generic;

namespace Platform
{
    public static class Loader
    {
        /// <summary>
        /// Загрузка плагина
        /// </summary>
        /// <param name="path">URI к плагину</param>
        /// <returns></returns>
        public static List<Managed> LoadPlugins(string path)
        {
            List<Managed> managed = new List<Managed>();
            foreach (string dllName in Directory.GetFiles(path, "*.dll"))
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(dllName);
                if (assemblyName != null)
                {
                    Managed m = GetManaged(assemblyName);
                    if (m != null)
                    {
                        managed.Add(m);
                    }
                }
            }
            return managed;
        }

        // ------------------ 

        private static Managed GetManaged(AssemblyName assemblyName)
        {
            AppDomain Domain = AppDomain.CreateDomain(Generator.GenerateDomainName());
            if (Domain != null)
            {
                Assembly assembly = Domain.Load(assemblyName);
                if (assembly != null)
                {
                    IPlugin plugin = GetPlugin(assembly);
                    if (plugin != null)
                    {
                        Managed m = new Managed(plugin);

                        m.domain = Domain;
                        m.plugin = plugin;

                        return m;
                    }
                }
            }
            AppDomain.Unload(Domain);
            return null;
        }

        // -------- 

        private static IPlugin GetPlugin(Assembly assembly)
        {
            foreach (Type t in assembly.GetTypes())
            {
                foreach (Type i in t.GetInterfaces())
                {
                    if (i.Equals(Type.GetType("Platform.IPlugin")))
                    {
                        return Activator.CreateInstance(t) as IPlugin;
                    }
                }
            }
            return null;
        }
    }
}