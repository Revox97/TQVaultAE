using System.Runtime.Versioning;

namespace TQVaultAE.IO.FileReadingStrategies
{
    [SupportedOSPlatform("windows")]
    internal class WindowsFileReadingStrategy : IFileReadingStrategy
    {
        async Task<byte[]> IFileReadingStrategy.ReadAsync(string path)
        {
            return File.Exists(path)
                ? File.ReadAllBytes(path)
                : throw new FileNotFoundException($"File '{path}' does not exist.");
        }
    }
}
