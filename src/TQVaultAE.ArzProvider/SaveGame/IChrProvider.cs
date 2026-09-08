using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.SaveGame
{
    public interface IChrProvider
    {
        Task<ChrFile> ReadAsync(string path);
    }
}
