using info.craigpayne.common.FilePicker;
using info.craigpayne.common.IocBootstrap;

namespace info.craigpayne.common.IocRegistration
{
    public class RegisterViews: IIocInstaller
    {
        public void Install(Ioc ioc)
        {
            ioc.Register<IFilePickerView, FilePickerView>();
        }
    }
}