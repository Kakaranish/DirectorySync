namespace DirectorySync.Core.Types;

public class FilesystemDirectory
{
    public string Name { get; }
    public Dictionary<string, FilesystemDirectory> ChildDirectoriesByName { get; } = new();
    public List<FilesystemFile> Files { get; } = new();

    public FilesystemDirectory(string name)
    {
        Name = name;
    }

    public FilesystemDirectory GetOrCreateChildDir(string childDirectoryName)
    {
        if (!ChildDirectoriesByName.TryGetValue(childDirectoryName, out var targetDir))
        {
            targetDir = new FilesystemDirectory(childDirectoryName);
            ChildDirectoriesByName.Add(childDirectoryName, targetDir);
        }

        return targetDir;
    }
}