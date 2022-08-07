namespace DirectorySync.Console.Types;

// public record FilesystemMetadata (
//     DirectoryPath RootPath,
//     IEnumerable<FileMetadata> FilesMetadata
// );

public class FilesystemMetadata
{
    public DirectoryPath RootPath { get; }
    public IEnumerable<FileMetadata> FilesMetadata { get; }

    public FilesystemMetadata(DirectoryPath rootPath, IEnumerable<FileMetadata> filesMetadata)
    {
        RootPath = rootPath;
        FilesMetadata = filesMetadata;
    }
}
