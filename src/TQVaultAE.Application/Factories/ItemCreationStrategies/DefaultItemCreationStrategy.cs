using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    // Used as a fallback, in case an item does not match any class, should never be called, besides there is a bug!
    internal class DefaultItemCreationStrategy : ItemCreationStrategy
    {
        internal override async Task<Item> CreateAsync(Item item, ArzRecord itemRecord)
        {
            // TODO log warning
            // TODO Create default item, that makes it obvious, that something is wrong, but does not break the UI.
            return item;
        }
    }
}
