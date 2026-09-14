using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class ItemEquipmentItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item itemBase, ArzRecord itemRecord)
        {
            try
            {
                ItemEquipmentItem item = new(itemBase)
                {
                    Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                    TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty,
                    Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default,
                    Name = await GetLocalizedValueAsync(itemRecord, "description"),
                    Description = await GetLocalizedValueAsync(itemRecord, "itemText"),
                    Icon = await GetIconAsync(itemRecord, "bitmap") ?? null!,
                };

                item.Size = GetItemSize(item.Icon);
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
