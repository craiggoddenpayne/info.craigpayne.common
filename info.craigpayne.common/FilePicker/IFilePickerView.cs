using Android.Views;

namespace info.craigpayne.common.FilePicker
{
    public interface IFilePickerView
    {
        event FilePickerView.DirectoryChangedEventHandler DirectoryChanged;
        View GetView();
        View GetView(string directory);
    }
}