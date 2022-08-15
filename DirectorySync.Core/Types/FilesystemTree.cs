using DirectorySync.Core.Types;

namespace DirectorySync.Core.Types2;

public class FilesystemTree
{
    private const string RootDirectoryName = ".";
    
    public DirectoryPath FilesystemFullPath { get; }
    public FilesystemDirectory RootDirectory { get; }

    private readonly List<FilePath> _allRelativeFilePaths = new();
    public IReadOnlyList<FilePath> AllRelativeFilePaths => _allRelativeFilePaths.AsReadOnly();

    private FilesystemTree(DirectoryPath filesystemFullPath, FilesystemDirectory rootDirectory)
    {
        FilesystemFullPath = filesystemFullPath;
        RootDirectory = rootDirectory;
    }

    public static FilesystemTree Initialize(DirectoryPath fullPath)
    {
        var rootDirectory = new FilesystemDirectory(RootDirectoryName);
        
        return new FilesystemTree(fullPath, rootDirectory);
    }
    
    public void AddFile(string absoluteFilePath)
    {
        const string relativeToPathChar = ".";
        
        var filePath = FilePath.FromAbsolutePath(absoluteFilePath);
        var relativeDirectoryPath = DirectoryPath.From(
            Path.GetRelativePath(FilesystemFullPath.Path, filePath.DirectoryPath.Path));
        var relativeFilePath = new FilePath(relativeDirectoryPath, filePath.Filename);

        var splitDirectories = relativeFilePath.DirectoryPath.Path
            .Split('/')
            .Where(p => p != relativeToPathChar);
        
        var currentDir = RootDirectory;
        foreach (var directory in splitDirectories)
        {
            var childDir = currentDir.GetOrCreateChildDir(directory);
            currentDir = childDir;
        }
        currentDir.Files.Add(new FilesystemFile(relativeFilePath.Filename));

        _allRelativeFilePaths.Add(relativeFilePath);
    }
}