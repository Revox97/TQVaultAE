using System.Collections.ObjectModel;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.Items.ItemProperties;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class WeaponItemCreationStrategy : ItemCreationStrategy
    {
        // TODO Get ItemSkills
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
                    Properties = new ObservableCollection<ItemProperty>(GetItemProperties(itemRecord)),
                    Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord)),
                    Icon = await GetIconAsync(itemRecord, "bitmap") ?? null!,
                    HidePrefixName = itemRecord["hidePrefixName"]?.Get<bool>(0) ?? false,
                    HideSuffixName = itemRecord["hideSuffixName"]?.Get<bool>(0) ?? false,
                    GameDlc = await GetGameDlcAsync(itemRecord, "itemNameTag"),
                    Description = await GetLocalizedValueAsync(itemRecord, "itemText"),
                };

                string attackSpeedPropertyValue = itemRecord["characterBaseAttackSpeedTag"]?.Get<string>(0) ?? string.Empty;
                item.AttackSpeed = attackSpeedPropertyValue.GetEnumValue<AttackSpeed>();

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
