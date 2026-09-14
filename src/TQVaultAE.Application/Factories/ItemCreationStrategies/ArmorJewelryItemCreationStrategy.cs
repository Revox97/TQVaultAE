using System.Collections.ObjectModel;
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
        internal override async Task<Item> CreateAsync(Item itemBase, ArzRecord itemRecord)
        {
            try
            {
                JewelryItem item = new(itemBase);
                item = (JewelryItem)GetGeneralItemProperties(item, itemRecord);
                item.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;

                string nameTag = itemRecord["itemNameTag"]?.Get<string>(0) ?? string.Empty;
                item.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                item.Properties = new ObservableCollection<ItemProperty>(GetItemAttributes(itemRecord));
                item.Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord));

                string bitmapPath = itemRecord["bitmap"]?.Get<string>(0) ?? string.Empty;
                item.Icon = await GetIconAsync(bitmapPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.
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
