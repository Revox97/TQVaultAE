using TQVaultAE.TitanQuestDataProviders.Decoders;
using TQVaultAE.TitanQuestDataProviders.Model;

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
