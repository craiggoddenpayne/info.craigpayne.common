using PicoContainer;

namespace info.craigpayne.common.IocBootstrap
{
    public interface IIocInstaller
    {
        void Install(Ioc ioc);
    }
}