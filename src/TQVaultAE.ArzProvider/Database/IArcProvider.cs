using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.Database
{
    public interface IArcProvider
    {
        Task<ArcFile> ReadAsync(string path);
    }
}
