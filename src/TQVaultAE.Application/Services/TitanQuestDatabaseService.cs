using TQVaultAE.Application.Contracts;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.TitanQuestDataProviders.Database;

namespace TQVaultAE.Application.Services
{
    public class TitanQuestDatabaseService : ITitanQuestDatabaseService
    {
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TQVaultTestData", "database.arz");

        private static ArzFile? s_database;

        public async Task InitializeAsync()
        {
            s_database ??= await new ArzProvider().ReadAsync(_dbPath).ConfigureAwait(false);
        }

        public async Task<ArzRecord> GetRecordByPathAsync(string path)
        {
            if (s_database is null)
                await InitializeAsync();

            return s_database!.GetRecordByPath(path);
        }
    }
}
