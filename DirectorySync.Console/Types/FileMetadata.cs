namespace DirectorySync.Console.Types;

public class FileMetadata
{
    public FilePath Path { get; }
    public long Size { get; }

    private FileMetadata(FilePath path, long size)
    {
        Path = path;
        Size = size;
    }

    public static FileMetadata From(FileInfo fileInfo)
    {
        return new FileMetadata(GetAbsoluteFilePath(fileInfo), fileInfo.Length);
    }
    
    private static FilePath GetAbsoluteFilePath(FileInfo fileInfo)
    {
        if(fileInfo.DirectoryName is null) 
            throw new ArgumentException($"{nameof(fileInfo.DirectoryName)} is not expected to be null here");
        
        return new FilePath(DirectoryPath.From(fileInfo.DirectoryName), fileInfo.Name);
    }
}