using DirectorySync.Core.Types;

namespace DirectorySync.Core.Services;

public interface IFilesystemMetadataProvider
{
    FilesystemMetadata GetFor(DirectoryPath rootDirectoryPath);
}

public class FilesystemMetadataProvider : IFilesystemMetadataProvider
{
    public FilesystemMetadata GetFor(DirectoryPath rootDirectoryPath)
    {
        var filesMetadata = GetFilesMetadata(rootDirectoryPath).ToList();
        
        return new FilesystemMetadata(rootDirectoryPath, filesMetadata);
    }

    private static IEnumerable<FilesystemFileMetadata> GetFilesMetadata(DirectoryPath rootDirectoryPath)
    {
        var fileInfos = Directory.GetFiles(rootDirectoryPath.Path, searchPattern: "*.*", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f));
        foreach (var fileInfo in fileInfos)
        {
            var fileMetadata = FileMetadata.From(fileInfo);
            yield return FilesystemFileMetadata.Create(rootDirectoryPath, fileInfo.FullName, fileMetadata);
        }
    }
}