using TQVaultAE.FileFormats.Arc;
using TQVaultAE.TitanQuestDataProviders.Decoders;

namespace TQVaultAE.TitanQuestDataProviders.Database
{
    public class ArcProvider : IArcProvider
    {
        public async Task<ArcFile> ReadAsync(string path)
        {
            using FileStream stream = File.OpenRead(path);
            return await ArcDecoder.DecodeAsync(stream, path);
        }
    }
}
