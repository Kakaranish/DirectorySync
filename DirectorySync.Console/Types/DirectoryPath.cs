using System.Text.RegularExpressions;

namespace DirectorySync.Console.Types;

public class DirectoryPath : ValueObject
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Path;
    }
}