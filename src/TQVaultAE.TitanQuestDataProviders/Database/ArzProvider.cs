using TQVaultAE.FileFormats.Arz;
using TQVaultAE.IO;
using TQVaultAE.TitanQuestDataProviders.Decoders;

namespace TQVaultAE.TitanQuestDataProviders.Database
{
    /// <summary>
    /// Represents an provider, which reads, decodes and provides <see cref="ArzFile"/>s.
    /// </summary>
    public class ArzProvider : IArzProvider
    {
        /// <summary>
        /// Reads and decodes the provided <paramref name="path"/> into an <see cref="ArzFile"/>.
        /// </summary>
        /// <param name="path">The path, in which the file is located.</param>
        /// <returns>The decoded <see cref="ArzFile"/>, that has been provided in <paramref name="path"/>.</returns>
        public async Task<ArzFile> ReadAsync(string path)
        {
            if (!File.Exists(path))
                throw new IOException($"File '{path}' does not exist.");

            using FileStream stream = File.OpenRead(path);
            return await ArzDecoder.DecodeAsync(stream, path).ConfigureAwait(false);
        }
    }
}
