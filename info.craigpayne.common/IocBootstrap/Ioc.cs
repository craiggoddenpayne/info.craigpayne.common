using Android.Content;
using PicoContainer.Defaults;

namespace info.craigpayne.common.IocBootstrap
{
    public class Ioc
    {
        public static DefaultPicoContainer IocContainer { get; set; }

        public static Ioc GetContext()
        {
            return new Ioc();
        }

        public static void Setup(Context context)
        {
            var ioc = new Ioc();
            IocContainer = new DefaultPicoContainer();
            var thisContext = new ContextContainer(context);
            IocContainer.RegisterComponentInstance(typeof (IContext), thisContext);

            var installer = new IocInstaller();
            installer.InstallContainersFrom(typeof(Ioc).Assembly, ioc);
            IocContainer.Start();
        }

        public TType Get<TType>()
        {
            return (TType)IocContainer.GetComponentInstanceOfType(typeof(TType));
        }

        public void Register<TKey, TValue>()
            where TValue : TKey
        {
            IocContainer.RegisterComponentImplementation(typeof (TKey), typeof(TValue));
        }
    }
}