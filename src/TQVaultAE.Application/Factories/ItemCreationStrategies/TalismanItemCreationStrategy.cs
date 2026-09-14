using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class TalismanItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item itemBase, ArzRecord itemRecord)
        {
            try
            {
                itemBase = GetGeneralItemProperties(itemBase, itemRecord);
                TalismanItem item = new(itemBase);

                itemBase.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;

                string nameTag = itemRecord["description"]?.Get<string>(0) ?? string.Empty;
                item.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                string descriptionTag = itemRecord["itemText"]?.Get<string>(0) ?? string.Empty;
                item.Description = await new GameLocalizationService().GetLocalizedValueByTag(descriptionTag).ConfigureAwait(false) ?? string.Empty;

                item.Properties = new ObservableCollection<ItemProperty>(GetItemAttributes(itemRecord));
                item.Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord));

                // Non complete relic
                string shardPath = itemRecord["shardBitmap"]?.Get<string>(0) ?? string.Empty;
                item.IconIncomplete = await GetIconAsync(shardPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.

                // compolete relic
                string bitmapPath = itemRecord["relicBitmap"]?.Get<string>(0) ?? string.Empty;
                item.IconComplete = await GetIconAsync(bitmapPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.

                item.Size = GetItemSize(item);

                return item;
            }
            catch(Exception ex)
            {
                // Item Creation failed.
                return itemBase;
            }
        }
    }
}
