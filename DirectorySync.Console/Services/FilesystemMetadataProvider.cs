using DirectorySync.Console.Types;

namespace DirectorySync.Console.Services;

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
        var filesMetadata = GetFilesMetadata(rootDirectoryPath).ToList();
        
        return new FilesystemMetadata(rootDirectoryPath, filesMetadata);
    }

    private IEnumerable<FileInFilesystemMetadata> GetFilesMetadata(DirectoryPath rootDirectoryPath)
    {
        var fileInfos = Directory.GetFiles(rootDirectoryPath.Path, "*", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f));
        foreach (var fileInfo in fileInfos)
        {
            var fileMetadata = new FileMetadata(
                FileId: _fileIdCreator.Create(fileInfo.Name, fileInfo.Length),
                Path: GetAbsoluteFilePath(fileInfo),
                Size: fileInfo.Length);
            
            yield return FileInFilesystemMetadata.Create(rootDirectoryPath, fileInfo.FullName, fileMetadata);
        }

    }

    private static FilePath GetAbsoluteFilePath(FileInfo fileInfo)
    {
        if(fileInfo.DirectoryName is null) 
            throw new ArgumentException($"{nameof(fileInfo.DirectoryName)} is not expected to be null here");
        return FilePath.From(DirectoryPath.From(fileInfo.DirectoryName), fileInfo.Name);
    }
}