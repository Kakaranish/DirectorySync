namespace DirectorySync.Console.Types;

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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DirectoryPath;
        yield return Filename;
    }
}