using System.Collections.ObjectModel;

namespace DirectorySync.ViewModels;

public class FilesystemItemViewModel
{
    public FilesystemItemViewModel()
    {
        Items = new ObservableCollection<FilesystemItemViewModel>();
    }

    public string Title { get; set; }
    public bool IsDirectory { get; set; } = true;

    public ObservableCollection<FilesystemItemViewModel> Items { get; set; }
}