using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class CharmItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item item, ArzRecord itemRecord)
        {
            try
            {
                item = GetGeneralItemProperties(item, itemRecord);
                item.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;

                string nameTag = itemRecord["description"]?.Get<string>(0) ?? string.Empty;
                item.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                item.Properties = GetItemAttributes(itemRecord);
                item.Requirements = GetItemRequirements(itemRecord);

                // TODO This needs some special implementation, maybe sub elems, that are bound to icon instead of writing icon directly.
                // Non complete relic
                string shardPath = itemRecord["shardBitmap"]?.Get<string>(0) ?? string.Empty;
                item.Icon = await GetIconAsync(shardPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.

                // compolete relic
                string bitmapPath = itemRecord["relicBitmap"]?.Get<string>(0) ?? string.Empty;
                item.Icon = await GetIconAsync(bitmapPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.
                item.Size = GetItemSize(item);

                // TODO Get valid item type e.g. speer, ring, shield, etc. properties

                return item;
            }
            catch(Exception ex)
            {
                // Item Creation failed.
                return item;
            }
        }
    }
}
