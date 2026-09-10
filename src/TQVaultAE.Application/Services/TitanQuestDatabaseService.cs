using TQVaultAE.Application.Factories;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Services
{
    public class TitanQuestDatabaseService
    {
        public async Task<Item> GetCompleteItemAsync(Item item)
        {
            return await new ItemFactory().GetCompleteItemAsync(item).ConfigureAwait(false);
        }
    }
}
