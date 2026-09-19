using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
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
                TalismanItem item = new(itemBase)
                {
                    TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty,
                    Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default,
                    Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                    Name = await GetLocalizedValueAsync(itemRecord, "description"),
                    Description = await GetLocalizedValueAsync(itemRecord, "itemText"),
                    Properties = new ObservableCollection<ItemProperty>(GetItemAttributes(itemRecord)),
                    Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord)),
                    IconIncomplete = await GetIconAsync(itemRecord, "shardBitmap") ?? null!,
                    IconComplete = await GetIconAsync(itemRecord, "relicBitmap") ?? null!,
                };

                item.Size = GetItemSize(item.Icon);
                return item;
            }
            catch (Exception ex)
            {
                // Item Creation failed.
                return itemBase;
            }
        }
    }
}
