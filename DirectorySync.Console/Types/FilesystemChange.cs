namespace DirectorySync.Console.Types;

public enum FilesystemChangeType
{
    Removed,
    Added
}

public record FilesystemChange(
    FilesystemChangeType ChangeType,
    FilePath PathRelativeToFilesystem
);