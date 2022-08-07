using Microsoft.Extensions.DependencyInjection;

namespace DirectorySync.Console;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterStartupDependencies(this IServiceCollection services)
    {
        services.AddTransient<Hasher>();
        services.AddTransient<IFilesystemMetadataProvider, FilesystemMetadataProvider>();
        services.AddTransient<IFileIdCreator, FileIdCreator>();
        services.AddTransient<FilesystemMetadataComparator>(); // TODO: Add interface
        
        return services;
    }
}