using DirectorySync.Console.Types;

namespace DirectorySync.Console;

public interface IFilesystemMetadataProvider
{
    FilesystemMetadata GetFor(DirectoryPath rootDirectoryPath);
}

public class FilesystemMetadataProvider : IFilesystemMetadataProvider
{
    private readonly IFileIdCreator _fileIdCreator;

    public FilesystemMetadataProvider(IFileIdCreator fileIdCreator)
    {
        _fileIdCreator = fileIdCreator;
    }

    public FilesystemMetadata GetFor(DirectoryPath rootDirectoryPath)
    {
        var filesMetadata = Directory.GetFiles(rootDirectoryPath.Path, "*", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f))
            .Select(fi => new FileMetadata(
                _fileIdCreator.Create(fi.Name, fi.Length),
                GetFilePath(rootDirectoryPath, fi),
                fi.Length));

        return new(rootDirectoryPath, filesMetadata);
    }

    private static FilePath GetFilePath(DirectoryPath rootDirectoryPath, FileInfo fileInfo)
    {
        var directory = Directory.GetParent(fileInfo.FullName);
        if (directory is null)
            throw new InvalidOperationException($"{nameof(directory)} is not expected to be null in this place");
        
        var directoryPath = DirectoryPath.From(
            Path.GetRelativePath(rootDirectoryPath.Path, DirectoryPath.From(directory.FullName).Path));

        return FilePath.From(directoryPath, fileInfo.Name);
    }
}