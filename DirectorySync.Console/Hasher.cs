using System.Security.Cryptography;
using System.Text;

namespace DirectorySync.Console;

public class Hasher : IDisposable
{
    private readonly Lazy<SHA256> _shaHasher = new(SHA256.Create);

    public string Hash(string toHash)
    {
        var toHashBytes = Encoding.UTF8.GetBytes(toHash);
        
        var resultBytes =  _shaHasher.Value.ComputeHash(toHashBytes);
        return string.Concat(Array.ConvertAll(resultBytes, h => h.ToString("X2")));
    }

    public void Dispose()
    {
        if (!_shaHasher.IsValueCreated) return;
        _shaHasher.Value.Dispose();
    }
}