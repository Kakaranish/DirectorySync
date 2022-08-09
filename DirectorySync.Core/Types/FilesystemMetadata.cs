namespace DirectorySync.Core.Types;

public record FilesystemMetadata(
    DirectoryPath RootPath,
    IReadOnlyCollection<FilesystemFileMetadata> FilesystemFileMetadataCollection
);