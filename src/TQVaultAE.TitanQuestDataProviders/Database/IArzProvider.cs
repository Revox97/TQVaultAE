using TQVaultAE.FileFormats.Arz;

namespace TQVaultAE.TitanQuestDataProviders.Database
{
    public interface IArzProvider
    {
        Task<ArzFile> ReadAsync(string path);
    }
}
