using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class ArmorItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item item, ArzRecord itemRecord)
        {
            try
            {
                item = GetGeneralItemProperties(item, itemRecord);
                item.Level = itemRecord["itemLevel"]?.Get<int>(0) ?? 0;
                item.Cost = itemRecord["cost"]?.Get<int>(0) ?? 0;
                item.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;

                string nameTag = itemRecord["itemNameTag"]?.Get<string>(0) ?? string.Empty;
                item.Name = await GameLocalizationService.GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                item.Properties = GetItemAttributes(itemRecord);
                item.Requirements = GetItemRequirements(itemRecord);

                string bitmapPath = itemRecord["bitmap"]?.Get<string>(0) ?? string.Empty;
                item.Icon = await GetIconAsync(bitmapPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.
                item.Size = GetItemSize(item);

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
