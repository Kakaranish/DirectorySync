namespace DirectorySync.Console.Types;

public record FilesystemMetadata(
    DirectoryPath RootPath,
    IReadOnlyCollection<FilesystemFileMetadata> FilesystemFileMetadataCollection
);