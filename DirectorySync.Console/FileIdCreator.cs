using DirectorySync.Console.Types;

namespace DirectorySync.Console;

public interface IFileIdCreator
{
    FileId Create(string filename, long fileSize);
}

public class FileIdCreator : IFileIdCreator
{
    private readonly Hasher _hasher;

    public FileIdCreator(Hasher hasher)
    {
        _hasher = hasher;
    }

    public FileId Create(string filename, long fileSize)
    {
        return new(_hasher.Hash(filename + fileSize));
    }
}