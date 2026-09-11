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
        internal override async Task<Item> CreateAsync(Item item, ArzRecord itemRecord)
        {
            try
            {
                OneShotItem result = new(item);
                result = (OneShotItem)GetGeneralItemProperties(result, itemRecord);
                result.Cost = itemRecord["itemCost"]?.Get<int>(0) ?? 0;
                result.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;

                string nameTag = itemRecord["description"]?.Get<string>(0) ?? string.Empty;
                result.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                string descriptionTag = itemRecord["itemText"]?.Get<string>(0) ?? string.Empty;
                result.Description = await new GameLocalizationService().GetLocalizedValueByTag(descriptionTag).ConfigureAwait(false) ?? string.Empty;

                result.Requirements = GetItemRequirements(itemRecord);
                result.Bonuses = GetBonuses(itemRecord);

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
