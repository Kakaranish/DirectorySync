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
        return referenceFilesystemMetadata.FilesMetadata
            .Concat(targetFilesystemMetadata.FilesMetadata)
            .GroupBy(x => x.FilePathRelativeToFilesystem)
            .Where(x => x.Count() == 1)
            .SelectMany(x => x)
            .Select(x => CreateChange(referenceFilesystemMetadata, x))
            .ToList();
    }

    private static FilesystemChange CreateChange(FilesystemMetadata referenceFilesystemMetadata, 
        FileInFilesystemMetadata difference)
    {
        var changeType = difference.FilesystemRoot == referenceFilesystemMetadata.RootPath
            ? FilesystemChangeType.Removed
            : FilesystemChangeType.Added;
        return new FilesystemChange(changeType, difference.FilePathRelativeToFilesystem);
    }
}