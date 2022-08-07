namespace DirectorySync.Console.Types;

public record FileMetadata(
    FileId FileId,
    FilePath Path,
    long Size
);