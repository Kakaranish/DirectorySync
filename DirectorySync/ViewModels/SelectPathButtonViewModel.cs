using System;
using System.IO;
using System.Reflection;
using DirectorySync.Commands;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace DirectorySync.ViewModels;

public class SelectPathButtonViewModel : ViewModelBase
{
    private string _selectedPath;

    #region Observable properties
    
    public string SelectedPath
    {
        get => _selectedPath;
        set
        {
            _selectedPath = value;
            RaisePropertyChanged();
        }
    }
    
    #endregion

    #region Commands
    
    public DelegateCommand SelectPathCommand { get; }
    
    #endregion

    public SelectPathButtonViewModel()
    {
        SelectedPath = "Test";
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
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}