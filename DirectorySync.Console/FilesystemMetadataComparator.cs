using DirectorySync.Console.Types;

namespace DirectorySync.Console;

public record FileChangeDetails(
    FileMetadata? ReferenceFileMetadata,
    FileMetadata? TargetFileMetadata,
    FileChangeType ChangeType
);

public enum FileChangeType
{
    Moved,
    Removed
}

public class FilesystemMetadataComparator
{
    public IReadOnlyCollection<FileChangeDetails> Compare(FilesystemMetadata referenceFilesystemMetadata,
        FilesystemMetadata targetFilesystemMetadata)
    {
        var changes = new List<FileChangeDetails>();
        var targetFilesMetadataById = (IReadOnlyDictionary<FileId, FileMetadata>) targetFilesystemMetadata
            .FilesMetadata.ToDictionary(f => f.FileId);
        
        foreach (var referenceFileMetadata in referenceFilesystemMetadata.FilesMetadata)
        {
            if (targetFilesMetadataById.TryGetValue(referenceFileMetadata.FileId, out var targetFileMetadata))
            {
                if (targetFileMetadata.Path != referenceFileMetadata.Path)
                {
                    changes.Add(new FileChangeDetails(referenceFileMetadata, targetFileMetadata, FileChangeType.Moved));
                }
            }
            else
            {
                changes.Add(new FileChangeDetails(referenceFileMetadata, null, FileChangeType.Removed));
            }
        }
        
        return changes;
    }
}