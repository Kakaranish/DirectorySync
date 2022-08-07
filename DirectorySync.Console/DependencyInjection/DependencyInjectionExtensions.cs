using DirectorySync.Console.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DirectorySync.Console.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterStartupDependencies(this IServiceCollection services)
    {
        services.AddTransient<Hasher>();
        services.AddTransient<IFilesystemMetadataProvider, FilesystemMetadataProvider>();
        services.AddTransient<IFileIdCreator, FileIdCreator>();
        services.AddTransient<IFilesystemChangeDetector, FilesystemChangeDetector>();
        
        return services;
    }
}