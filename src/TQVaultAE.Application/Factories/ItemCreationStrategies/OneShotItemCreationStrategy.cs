using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class OneShotItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item itemBase, ArzRecord itemRecord)
        {
            try
            {
                OneShotItem item = new(itemBase)
                {
                    TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty,
                    Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default,
                    Cost = itemRecord["itemCost"]?.Get<int>(0) ?? 0,
                    Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                    Name = await GetLocalizedValueAsync(itemRecord, "description"),
                    Description = await GetLocalizedValueAsync(itemRecord, "itemText"),
                    Properties = new ObservableCollection<ItemProperty>(GetItemAttributes(itemRecord)),
                    Bonuses = GetBonuses(itemRecord),
                    Icon = await GetIconAsync(itemRecord, "bitmap") ?? null!,
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

        private static List<OneShotBonus> GetBonuses(ArzRecord itemRecord)
        {
            List<ArzRecordProperty> validProperties = [.. itemRecord.Properties.Where(x => x.IsValueRelevant && x.Name.StartsWith("bonus"))];

            List<OneShotBonus> bonuses = [];

            foreach (ArzRecordProperty property in validProperties)
            {
                OneShotBonusType type;
                try
                {
                    type = property.Name.GetEnumValue<OneShotBonusType>();
                }
                catch (Exception ex)
                {
                    continue;
                }

                OneShotBonus bonusResult = new(type, property.Get<float>(0));
                bonuses.Add(bonusResult);
            }

            return bonuses;
        }
    }
}
