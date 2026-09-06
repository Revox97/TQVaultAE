using TQVaultAE.IO.FileReadingStrategies;

namespace TQVaultAE.IO
{
    public class FileReader
    {
        private readonly IFileReadingStrategy _fileReadingStrategy;

        public FileReader()
        {
            if (OperatingSystem.IsOSPlatform("windows"))
            {
                _fileReadingStrategy = new WindowsFileReadingStrategy();
            }
            else
            {
                // TODO temporary until Linux support is added
                _fileReadingStrategy = null!;
            }
        }

        public async Task<byte[]> ReadBytesAsync(string path)
        {
            return await _fileReadingStrategy.ReadAsync(path);
        }
    }
}
