using System.Windows;
using DirectorySync.ViewModels;

namespace DirectorySync;

public partial class MainWindow : Window
{
    private readonly MainWindowViewModel _mainWindowViewModel;

    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();

        _mainWindowViewModel = mainWindowViewModel;
        DataContext = _mainWindowViewModel;
        
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        await _mainWindowViewModel.LoadAsync(); 
    }
}