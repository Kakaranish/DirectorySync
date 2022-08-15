using System.Collections.Generic;
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

public class MainWindowViewModel : ViewModelBase
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

    private readonly IFilesystemMetadataProvider _filesystemMetadataProvider;
    public ObservableCollection<string> Paths { get; } = new(new List<string>{"1", "2", "3"});

    private string _selectedPath;
    
    private ObservableCollection<FilesystemItemViewModel> _items = new(new []
    {
        new FilesystemItemViewModel
        {
            Title = "Item 1"
        },
        new FilesystemItemViewModel
        {
            Title = "Item 2",
            IsDirectory = false
        },
        new FilesystemItemViewModel
        {
            Title = "Item 3",
            Items = new ObservableCollection<FilesystemItemViewModel>()
            {
                new FilesystemItemViewModel
                {
                    Title = "Subitem"
                },
                new FilesystemItemViewModel
                {
                    Title = "Subitem 2",
                    IsDirectory = false
                }
            }
        }
    });

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
        
        var filesystemTreeCreator = new FilesystemTreeCreator();
        var filesystemTree = filesystemTreeCreator.Create(rootDirectoryPath);
        
        Items = ToFilesystemToViewModel(filesystemTree);
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