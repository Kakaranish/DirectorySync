namespace DirectorySync.Console.Types;

public record FilesystemMetadata(
    DirectoryPath RootPath,
    IReadOnlyCollection<FileInFilesystemMetadata> FilesMetadata
);