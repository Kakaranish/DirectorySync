using System.Diagnostics;

namespace DirectorySync.Console.Types;

[DebuggerDisplay("{FilePathRelativeToFilesystem}")]
public class FileInFilesystemMetadata
{
    public DirectoryPath FilesystemRoot { get; }
    public FilePath FilePathRelativeToFilesystem { get; }
    public FileMetadata FileMetadata { get; }

    private FileInFilesystemMetadata(DirectoryPath filesystemRoot, FilePath filePathRelativeToFilesystem, 
        FileMetadata fileMetadata)
    {
        FilesystemRoot = filesystemRoot;
        FilePathRelativeToFilesystem = filePathRelativeToFilesystem;
        FileMetadata = fileMetadata;
    }

    public static FileInFilesystemMetadata Create(DirectoryPath filesystemRoot, string fileAbsolutePath, 
        FileMetadata fileMetadata)
    {
        var directory = Path.GetDirectoryName(fileAbsolutePath);
        if (directory is null)
            throw new InvalidOperationException($"{nameof(directory)} is not expected to be null here");
    
        var relativeDirectoryPath = DirectoryPath.From(
            Path.GetRelativePath(filesystemRoot.Path, DirectoryPath.From(directory).Path));
        var filename = Path.GetFileName(fileAbsolutePath);
        var filePath = FilePath.From(relativeDirectoryPath, filename);
        
        return new FileInFilesystemMetadata(filesystemRoot, filePath, fileMetadata);
    }
}