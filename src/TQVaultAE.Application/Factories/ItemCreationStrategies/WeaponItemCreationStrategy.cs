using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class WeaponItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item itemBase, ArzRecord itemRecord)
        {
            try
            {
                WeaponItem item = new(itemBase)
                {
                    Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default,
                    Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                    TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty,
                    BaseName = await GetLocalizedValueAsync(itemRecord, "itemNameTag"),
                    Properties = new ObservableCollection<ItemProperty>(GetItemAttributes(itemRecord)),
                    Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord)),
                    Icon = await GetIconAsync(itemRecord, "bitmap") ?? null!,
                    HidePrefixName = itemRecord["hidePrefixName"]?.Get<bool>(0) ?? false,
                    HideSuffixName = itemRecord["hideSuffixName"]?.Get<bool>(0) ?? false,
                };

                item.Prefix = await GetCompleteAffixAsync(item.Prefix);
                item.Suffix = await GetCompleteAffixAsync(item.Suffix);

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
