using Android.App;
using Android.OS;
using info.craigpayne.common.FilePicker;
using info.craigpayne.common.IocBootstrap;

namespace info.craigpayne.common
{
    [Activity(Label = "Craig Payne", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Ioc.Setup(this);

            var iocContext = Ioc.GetContext();
            var filePicker = iocContext.Get<IFilePickerView>();
            SetContentView(filePicker.GetView());
            filePicker.DirectoryChanged += directory => SetContentView(filePicker.GetView(directory));
        }
    }
}

