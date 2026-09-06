using TQVaultAE.Arz.Model;

namespace TQVaultAE.TitanQuestDataProviders.Database
{
    public interface IArzProvider
    {
        Task<ArzFile> ReadAsync(string path);
    }
}
