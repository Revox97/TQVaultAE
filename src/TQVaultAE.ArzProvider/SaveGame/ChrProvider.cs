using TQVaultAE.TitanQuestDataProviders.Decoders;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.SaveGame
{
    // winsys.dxb and winsys.dxg seem to be the statsh files. Which seem to share the same format as .chr files
    public class ChrProvider : IChrProvider
    {
        public async Task<ChrFile> ReadAsync(string path)
        {
            using FileStream fStream = File.OpenRead(path);
            return await ChrDecoder.DecodeAsync(fStream, path);
        }
    }
}
