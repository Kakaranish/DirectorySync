namespace DirectorySync.Core.Types;

public class FilePath : ValueObject
{
    public DirectoryPath DirectoryPath { get; }
    public string Filename { get; }

    public string FullPath => $"{DirectoryPath.Path}/{Filename}";
    
    public FilePath(DirectoryPath directoryPath, string filename)
    {
        DirectoryPath = directoryPath;
        Filename = filename;
    }

    public static FilePath FromAbsolutePath(string absolutePath)
    {
        var sanitizedPath = absolutePath.Replace("\\", "/").Trim('.').Trim('/');
        var lastSlashIndex = sanitizedPath.LastIndexOf("/", StringComparison.Ordinal);
        
        var filename = sanitizedPath.Substring(lastSlashIndex + 1);
        var directoryPathStr = sanitizedPath.Substring(0, lastSlashIndex);
        
        return new FilePath(DirectoryPath.From(directoryPathStr), filename);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DirectoryPath;
        yield return Filename;
    }
}