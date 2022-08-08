namespace DirectorySync.Console.Types;

public class FilesystemChange
{
    public FilesystemChangeType ChangeType { get; }
    public FilePath PathRelativeToFilesystem { get; }

    public FilesystemChange(FilesystemChangeType changeType, FilePath pathRelativeToFilesystem)
    {
        ChangeType = changeType;
        PathRelativeToFilesystem = pathRelativeToFilesystem;
    }
}