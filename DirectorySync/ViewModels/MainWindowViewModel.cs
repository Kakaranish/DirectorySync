using System.Threading.Tasks;

namespace DirectorySync.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public FilesystemViewModel ReferenceFilesystemViewModel { get; }
    public FilesystemViewModel TargetFilesystemViewModel { get; }

    public MainWindowViewModel(FilesystemViewModel referenceFilesystemViewModel,
        FilesystemViewModel targetFilesystemViewModel)
    {
        ReferenceFilesystemViewModel = referenceFilesystemViewModel;
        TargetFilesystemViewModel = targetFilesystemViewModel;
    }

    public override Task LoadAsync()
    {
        return Task.CompletedTask;
    }
}