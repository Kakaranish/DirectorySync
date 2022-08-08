using DirectorySync.Console.Types;

namespace DirectorySync.Console.Services;

public interface IFilesystemChangeDetector
{
    IReadOnlyCollection<FilesystemChange> GetChanges(FilesystemMetadata referenceFilesystemMetadata,
        FilesystemMetadata targetFilesystemMetadata);
}

public class FilesystemChangeDetector : IFilesystemChangeDetector
{
    public IReadOnlyCollection<FilesystemChange> GetChanges(FilesystemMetadata referenceFilesystemMetadata,
        FilesystemMetadata targetFilesystemMetadata)
    {
        var fileMetadataGroupedByPath = referenceFilesystemMetadata.FilesMetadata
            .Concat(targetFilesystemMetadata.FilesMetadata)
            .GroupBy(x => x.FilePathRelativeToFilesystem);

        var changes = new List<FilesystemChange>();
        foreach (var fileMetadataForPath in fileMetadataGroupedByPath)
        {
            var groupSize = fileMetadataForPath.Count();
            if (groupSize == 1)
            {
                var fileMetadata = fileMetadataForPath.Single();
                
                var changeType = fileMetadata.FilesystemRoot == referenceFilesystemMetadata.RootPath
                    ? FilesystemChangeType.Removed
                    : FilesystemChangeType.Added;
                
                changes.Add(new FilesystemChange(changeType, fileMetadata.FilePathRelativeToFilesystem));
            }
            else if (groupSize == 2)
            {
                if (fileMetadataForPath.ElementAt(0).FileMetadata.Size != fileMetadataForPath.ElementAt(1).FileMetadata.Size)
                {
                    changes.Add(new FilesystemChange(FilesystemChangeType.ContentChanged, 
                        fileMetadataForPath.First().FilePathRelativeToFilesystem));
                }
            }
            else
            {
                throw new InvalidOperationException("Detected group with invalid size");
            }
            
        }

        return changes;
    }
}