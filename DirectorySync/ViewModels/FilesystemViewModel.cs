using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DirectorySync.Commands;
using DirectorySync.Core.Services;
using DirectorySync.Core.Types;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DirectorySync.ViewModels;

public class FilesystemViewModel : ViewModelBase
{
    public ObservableCollection<FilesystemItemViewModel> Items
    {
        get => _items;
        set
        {
            _items = value;
            RaisePropertyChanged();
        }
    }

    private string? _selectedPath;

    private ObservableCollection<FilesystemItemViewModel> _items = new();

    public string? SelectedPath
    {
        get => _selectedPath;
        set
        {
            _selectedPath = value;
            RaisePropertyChanged();
            RaisePropertyChanged(nameof(SelectedPathViewValue));
        }
    }

    public string SelectedPathViewValue => _selectedPath ?? "Click here to select fs root";

    public DelegateCommand SelectPathCommand { get; }

    public FilesystemViewModel()
    {
        SelectPathCommand = new DelegateCommand(SelectPath);
    }

    private void SelectPath(object? obj)
    {
        var dialog = new CommonOpenFileDialog();
        var assemblyDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
        dialog.InitialDirectory = assemblyDir;
        dialog.IsFolderPicker = true;

        try
        {
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SelectedPath = dialog.FileName;
            }

            var rootDirectoryPath = DirectoryPath.From(dialog.FileName);
        
            var filesystemTreeCreator = new FilesystemTreeCreator();
            var filesystemTree = filesystemTreeCreator.Create(rootDirectoryPath);
        
            Items = ToFilesystemToViewModel(filesystemTree);
        }
        catch (InvalidOperationException e)
        {
            if (e.Message.Contains("dialog was canceled")) return;
            throw;
        }
    }

    private ObservableCollection<FilesystemItemViewModel> ToFilesystemToViewModel(FilesystemTree filesystemTree)
    {
        var current = filesystemTree.RootDirectory;

        var root = new FilesystemItemViewModel(); 
        DoSth(root, current);
        
        return root.Items;
    }

    private void DoSth(FilesystemItemViewModel currentFsItem, FilesystemDirectory currentDir)
    {
        var directories = currentDir.ChildDirectoriesByName
            .OrderBy(x => x.Key)
            .Select(x => x.Value);
        foreach (var directory in directories)
        {
            var fsDirectoryItem = new FilesystemItemViewModel
            {
                Title = directory.Name,
                IsDirectory = true
            };
            currentFsItem.Items.Add(fsDirectoryItem);
            
            DoSth(fsDirectoryItem, directory);
        }
        
        foreach (var currentFile in currentDir.Files)
        {
            currentFsItem.Items.Add(new FilesystemItemViewModel
            {
                Title = currentFile.Name,
                IsDirectory = false
            });
        }
    }

    public override Task LoadAsync()
    {
        return Task.CompletedTask;
    }
}