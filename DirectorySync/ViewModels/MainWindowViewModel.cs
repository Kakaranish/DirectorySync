using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DirectorySync.Commands;
using DirectorySync.Core.Services;
using DirectorySync.Core.Types;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DirectorySync.ViewModels;

public class File
{
    public string Path { get; set; }
}

public class MainWindowViewModel : ViewModelBase
{
    private readonly IFilesystemMetadataProvider _filesystemMetadataProvider;
    public ObservableCollection<string> Paths { get; } = new(new List<string>{"1", "2", "3"});

    private string _selectedPath;
    public string SelectedPath
    {
        get => _selectedPath;
        set
        {
            _selectedPath = value;
            RaisePropertyChanged();
        }
    }

    public DelegateCommand ChooseReferenceFilesystemCommand { get; }

    public MainWindowViewModel(IFilesystemMetadataProvider filesystemMetadataProvider)
    {
        _filesystemMetadataProvider = filesystemMetadataProvider;
        ChooseReferenceFilesystemCommand = new DelegateCommand(ChooseReferenceFilesystem);
    }

    private void ChooseReferenceFilesystem(object? obj)
    {
        var dialog = new CommonOpenFileDialog();
        var assemblyDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
        dialog.InitialDirectory = assemblyDir;
        dialog.IsFolderPicker = true;

        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
        {
            SelectedPath = dialog.FileName;
        }
        
        // TODO: Handle Cancel action

        var rootDirectoryPath = DirectoryPath.From(dialog.FileName);
        var filesystemMetadata = _filesystemMetadataProvider.GetFor(rootDirectoryPath);
    }

    public override Task LoadAsync()
    {
        var x = 10;
        
        return Task.CompletedTask;
    }
}