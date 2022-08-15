using DirectorySync.Controls;
using DirectorySync.Core.Services;
using DirectorySync.ViewModels;
using DirectorySync.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DirectorySync.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterStartupDependencies(this IServiceCollection services)
    {
        services.AddTransient<FilesystemViewModel>();
        services.AddTransient<FilesystemView>();
        
        services.AddTransient<SelectPathButtonViewModel>();
        services.AddTransient<SelectPathButton>();
        
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<MainWindow>();
        
        services.AddTransient<IFilesystemMetadataProvider, FilesystemMetadataProvider>();
        services.AddTransient<IFilesystemChangeDetector, FilesystemChangeDetector>();
        
        return services;
    }
}