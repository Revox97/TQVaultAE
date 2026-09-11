using TQVaultAE.FileFormats.Arc;

namespace TQVaultAE.TitanQuestDataProviders.Database
{
    public interface IArcProvider
    {
        Task<ArcFile> ReadAsync(string path);
    }
}
