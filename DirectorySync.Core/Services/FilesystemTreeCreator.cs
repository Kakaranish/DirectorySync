using DirectorySync.Core.Types;

namespace DirectorySync.Core.Types2;

public class FilesystemTreeCreator
{
    public FilesystemTree Create(DirectoryPath filesystemRoot)
    {
        var fileInfos = Directory.GetFiles(filesystemRoot.Path, searchPattern: "*.*", SearchOption.AllDirectories)
            .Select(f => new FileInfo(f));

        var filesystemTree = FilesystemTree.Initialize(filesystemRoot);
        foreach (var fileInfo in fileInfos)
        {
            filesystemTree.AddFile(fileInfo.FullName);
        }

        return filesystemTree;
    }
}