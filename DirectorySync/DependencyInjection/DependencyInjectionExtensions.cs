using DirectorySync.Core.Services;
using DirectorySync.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DirectorySync.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterStartupDependencies(this IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<MainWindow>();
        
        services.AddTransient<IFilesystemMetadataProvider, FilesystemMetadataProvider>();
        services.AddTransient<IFilesystemChangeDetector, FilesystemChangeDetector>();
        
        return services;
    }
}