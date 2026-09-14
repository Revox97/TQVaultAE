using TQVaultAE.FileFormats.Arz;

namespace TQVaultAE.Application.Contracts
{
    public interface ITitanQuestDatabaseService
    {
        Task InitializeAsync();
        Task<ArzRecord> GetRecordByPathAsync(string path);
    }
}
