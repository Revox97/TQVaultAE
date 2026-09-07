using TQVaultAE.IO.FileReadingStrategies;

namespace TQVaultAE.IO
{
    /// <summary>
    /// Represents a reader, that reads files from the file system.
    /// </summary>
    public class FileReader
    {
        private readonly IFileReadingStrategy _fileReadingStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReader"/> class.
        /// </summary>
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

        /// <summary>
        /// Reads all bytes form a file located within <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The file location.</param>
        /// <returns>The byte content of the file provided in <paramref name="path"/>.</returns>
        public async Task<byte[]> ReadBytesAsync(string path)
        {
            return await _fileReadingStrategy.ReadAsync(path);
        }
    }
}
