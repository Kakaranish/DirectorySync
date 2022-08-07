namespace DirectorySync.Console.Types;

public class FilePath : IEquatable<FilePath>
{
    public DirectoryPath DirectoryPath { get; }
    public string Filename { get; }

    public string FullPath => $"{DirectoryPath.Path}/{Filename}";
    
    private FilePath(DirectoryPath directoryPath, string filename)
    {
        DirectoryPath = directoryPath;
        Filename = filename;
    }

    public static FilePath From(DirectoryPath directoryPath, string filename)
    {
        return new FilePath(directoryPath, filename);
    }

    #region Objects comparison

    public bool Equals(FilePath? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return DirectoryPath.Equals(other.DirectoryPath) && Filename == other.Filename;
    }

    public override string ToString()
    {
        return FullPath;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FilePath) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DirectoryPath, Filename);
    }

    public static bool operator ==(FilePath? filePath1, FilePath? filePath2)
    {
        if (filePath1 is null)
            return filePath2 is null;
        return filePath1.Equals(filePath2);
    }

    public static bool operator !=(FilePath? filePath1, FilePath? filePath2)
    {
        return !(filePath1 == filePath2);
    }

    #endregion
}