using DirectorySync.Console.Types;

namespace DirectorySync.Console.Services;

public interface IFilesystemChangeDetector
{
    IEnumerable<FilesystemChange> GetChanges(FilesystemMetadata referenceFilesystemMetadata,
        FilesystemMetadata targetFilesystemMetadata);
}

public class FilesystemChangeDetector : IFilesystemChangeDetector
{
    public IEnumerable<FilesystemChange> GetChanges(FilesystemMetadata referenceFilesystemMetadata,
        FilesystemMetadata targetFilesystemMetadata)
    {
        var fsFileMetadataGroupedByPath = referenceFilesystemMetadata.FilesystemFileMetadataCollection
            .Concat(targetFilesystemMetadata.FilesystemFileMetadataCollection)
            .GroupBy(x => x.FilePathRelativeToFilesystem);

        var changes = new List<FilesystemChange>();
        foreach (var fileMetadataForPath in fsFileMetadataGroupedByPath)
        {
            var groupSize = fileMetadataForPath.Count();
            if (groupSize == 1)
            {
                var fileMetadata = fileMetadataForPath.Single();
                
                var changeType = fileMetadata.FilesystemRoot == referenceFilesystemMetadata.RootPath
                    ? FilesystemChangeType.Removed
                    : FilesystemChangeType.Added;

                yield return new FilesystemChange(changeType, fileMetadata.FilePathRelativeToFilesystem);
            }
            else if (groupSize == 2)
            {
                var fileSizeChanged = fileMetadataForPath.ElementAt(0).FileMetadata.Size 
                                      != fileMetadataForPath.ElementAt(1).FileMetadata.Size; 
                if (fileSizeChanged)
                {
                    yield return new FilesystemChange(FilesystemChangeType.ContentChanged, 
                        fileMetadataForPath.First().FilePathRelativeToFilesystem);
                }
            }
            else
            {
                throw new InvalidOperationException("Detected group with invalid size");
            }
        }
    }
}