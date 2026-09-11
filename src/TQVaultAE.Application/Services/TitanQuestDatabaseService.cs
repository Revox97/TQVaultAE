using TQVaultAE.Application.Contracts;
using TQVaultAE.Application.Factories;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items;
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

        public async Task<Item> GetCompleteItemAsync(Item item)
        {
            if (s_database is null)
                await InitializeAsync();

            // TODO this call chain should be the other way aroung
            return await new ItemFactory().GetCompleteItemAsync(item).ConfigureAwait(false);
        }
    }
}
