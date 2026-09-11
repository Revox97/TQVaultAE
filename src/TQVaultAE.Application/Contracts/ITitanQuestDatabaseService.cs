using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Contracts
{
    public interface ITitanQuestDatabaseService
    {
        Task InitializeAsync();

        Task<Item> GetCompleteItemAsync(Item item);
    }
}
