using TQVaultAE.TitanQuestDataProviders.Decoders;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.SaveGame
{
    public class ChrProvider : IChrProvider
    {
        public async Task<ChrFile> ReadAsync(string path)
        {
            using FileStream fStream = File.OpenRead(path);
            return await ChrDecoder.DecodeAsync(fStream, path);
        }
    }
}
