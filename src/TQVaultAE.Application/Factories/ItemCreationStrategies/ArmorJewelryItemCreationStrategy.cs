using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class ArmorJewelryItemCreationStrategy : ItemCreationStrategy
    {
        // TODO what represents x2tagUArmor107
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item item, ArzRecord itemRecord)
        {
            try
            {
                JewelryItem result = new(item);
                result = (JewelryItem)GetGeneralItemProperties(result, itemRecord);
                result.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;

                string nameTag = itemRecord["itemNameTag"]?.Get<string>(0) ?? string.Empty;
                result.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                result.Properties = GetItemAttributes(itemRecord);
                result.Requirements = GetItemRequirements(itemRecord);

                string bitmapPath = itemRecord["bitmap"]?.Get<string>(0) ?? string.Empty;
                result.Icon = await GetIconAsync(bitmapPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.
                result.Size = GetItemSize(result);

                return result;
            }
            catch(Exception ex)
            {
                // Item Creation failed.
                return item;
            }
        }
    }
}
