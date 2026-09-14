using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
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
                OneShotItem item = new(itemBase);
                item = (OneShotItem)GetGeneralItemProperties(item, itemRecord);
                item.Cost = itemRecord["itemCost"]?.Get<int>(0) ?? 0;
                item.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;

                string nameTag = itemRecord["description"]?.Get<string>(0) ?? string.Empty;
                item.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                string descriptionTag = itemRecord["itemText"]?.Get<string>(0) ?? string.Empty;
                item.Description = await new GameLocalizationService().GetLocalizedValueByTag(descriptionTag).ConfigureAwait(false) ?? string.Empty;

                item.Properties = new ObservableCollection<ItemProperty>(GetItemAttributes(itemRecord));
                item.Bonuses = GetBonuses(itemRecord);

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

        private static List<OneShotBonus> GetBonuses(ArzRecord itemRecord)
        {
            List<ArzRecordProperty> validProperties = [.. itemRecord.Properties.Where(x => x.IsValueRelevant && x.Name.StartsWith("bonus"))];

            List<OneShotBonus> bonuses = [];

            foreach(ArzRecordProperty property in validProperties)
            {
                OneShotBonusType type;
                try
                {
                    type = property.Name.GetEnumValue<OneShotBonusType>();
                }
                catch(Exception ex)
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
