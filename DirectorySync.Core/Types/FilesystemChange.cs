namespace DirectorySync.Core.Types;

public record FilesystemChange(
    FilesystemChangeType ChangeType,
    FilePath PathRelativeToFilesystem
);