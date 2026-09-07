using TQVaultAE.IO;
using TQVaultAE.TitanQuestDataProviders.Decoders;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.Database
{
    public class ArcProvider : IArcProvider
    {
        public async Task<ArcFile> ReadAsync(string path)
        {
            byte[] content = await new FileReader().ReadBytesAsync(path).ConfigureAwait(false);
            return await ArcDecoder.DecodeAsync(content, path);
        }
    }
}
