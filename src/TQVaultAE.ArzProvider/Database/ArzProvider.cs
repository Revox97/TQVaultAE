using TQVaultAE.IO;
using TQVaultAE.TitanQuestDataProviders.Decoders;
using TQVaultAE.TitanQuestDataProviders.Model;

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
            byte[] content = await new FileReader().ReadBytesAsync(path).ConfigureAwait(false);
            return await ArzDecoder.DecodeAsync(content, path);
        }
    }
}
