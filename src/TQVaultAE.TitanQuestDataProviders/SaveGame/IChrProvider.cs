using TQVaultAE.FileFormats.Chr;

namespace TQVaultAE.TitanQuestDataProviders.SaveGame
{
    public interface IChrProvider
    {
        Task<ChrFile> ReadAsync(string path);
    }
}
