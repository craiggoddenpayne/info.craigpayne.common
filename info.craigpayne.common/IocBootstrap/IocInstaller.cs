using System;
using System.Linq;
using System.Reflection;

namespace info.craigpayne.common.IocBootstrap
{
    public class IocInstaller
    {
        public void InstallContainersFrom(Assembly assembly, Ioc ioc)
        {
            var type = typeof(IIocInstaller);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(type.IsAssignableFrom);

            foreach (var atype in types)
            {
                if (atype.IsAbstract || atype.IsInterface)
                    continue;

                ((IIocInstaller)Activator.CreateInstance(atype)).Install(ioc);
            }
        }
    }
}