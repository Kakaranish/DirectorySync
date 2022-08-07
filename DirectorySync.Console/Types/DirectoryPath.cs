using System.Text.RegularExpressions;

namespace DirectorySync.Console.Types;

public class DirectoryPath : IEquatable<DirectoryPath>
{
    public string Path { get; }

    private DirectoryPath(string path)
    {
        Path = path;
    }

    public static DirectoryPath From(string path)
    {
        path = path.Replace("\\", "/");
        path = Regex.Replace(path, "/{2,}", "/");
        path = Regex.Replace(path, "\\{2,}", "\\");
        path = path.Trim('/');

        return new DirectoryPath(path);
    }

    #region Objects comparison
    
    public bool Equals(DirectoryPath? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path == other.Path;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((DirectoryPath) obj);
    }

    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }
    
    public static bool operator ==(DirectoryPath? directoryPath1, DirectoryPath? directoryPath2)
    {
        if (directoryPath1 is null)
            return directoryPath2 is null;
        return directoryPath1.Equals(directoryPath2);
    }

    public static bool operator !=(DirectoryPath? directoryPath1, DirectoryPath? directoryPath2)
    {
        return !(directoryPath1 == directoryPath2);
    }
    
    #endregion
}