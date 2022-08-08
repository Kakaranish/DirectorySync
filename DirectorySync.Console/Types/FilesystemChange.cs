namespace DirectorySync.Console.Types;

public record FilesystemChange(
    FilesystemChangeType ChangeType,
    FilePath PathRelativeToFilesystem
);