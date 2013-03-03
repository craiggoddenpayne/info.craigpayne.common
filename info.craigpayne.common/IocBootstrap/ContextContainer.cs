using Android.Content;

namespace info.craigpayne.common.IocBootstrap
{
    public class ContextContainer : IContext
    {
        public Context Context { get; private set; }
        public ContextContainer(Context context)
        {
            Context = context;
        }
    }
}