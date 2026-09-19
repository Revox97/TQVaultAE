using System.Runtime.Versioning;
using Avalonia;
using Avalonia.Media.Imaging;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.FileFormats.Tex;
using TQVaultAE.Localisation;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal abstract class ItemCreationStrategy
    {
        protected const int CellVerticyLength = 32;

        internal abstract Task<Item> CreateAsync(Item item, ArzRecord itemRecord);

        protected virtual async Task<Affix?> GetCompleteAffixAsync(Affix? affixBase)
        {
            if (affixBase is null)
                return affixBase;

            ArzRecord affixRecord = await new TitanQuestDatabaseService().GetRecordByPathAsync(affixBase.Path);

            Affix affix = new()
            {
                Path = affixBase.Path,
                Name = await GetLocalizedValueAsync(affixRecord, "lootRandomizerName"),
                Properties = GetItemAttributes(affixRecord), // TODO The tag property needs to be filtered.
                Requirements = GetItemRequirements(affixRecord),
                MarketAdjustmentPercent = affixRecord["marketAdjustmentPercent"]?.GetSingle(0) ?? 0.0f
            };

            //affix.Format = await GetLocalizedValueAsync(affixRecord, "characterBaseAttackSpeedTag"); // TODO These will for sure be different every time.
            return affix;
        }

        protected virtual async Task<string> GetLocalizedValueAsync(ArzRecord itemRecord, string itemNamePropertyName)
        {
            string nameTag = itemRecord[itemNamePropertyName]?.Get<string>(0) ?? string.Empty;
            return await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;
        }

        protected virtual List<ItemRequirement> GetItemRequirements(ArzRecord itemRecord)
        {
            List<ArzRecordProperty> validProperties = [.. itemRecord.Properties.Where(x => x.IsValueRelevant && x.Name.EndsWith("Requirement"))];

            List<ItemRequirement> itemRequirements = [];
            foreach (ArzRecordProperty property in validProperties)
            {
                ItemRequirementType type;
                try
                {
                    type = property.Name.GetEnumValue<ItemRequirementType>();
                }
                catch (Exception ex)
                {
                    continue;
                }

                ItemRequirement requirementResult = new(type, property.Get<int>(0));
                itemRequirements.Add(requirementResult);
            }

            return itemRequirements;
        }

        protected virtual List<ItemProperty> GetItemAttributes(ArzRecord itemRecord)
        {
            List<ArzRecordProperty> validProperties = [.. itemRecord.Properties.Where(x => x.IsValueRelevant &&
            (
                   x.Name.StartsWith("offensive")
                || x.Name.StartsWith("defensive")
                || x.Name.StartsWith("retaliation")
                || x.Name.StartsWith("skill")
                || x.Name.StartsWith("character")
            ))];

            List<ItemProperty> itemProperties = [];
            foreach (ArzRecordProperty property in validProperties)
            {
                ItemPropertyType type;
                try
                {
                    type = property.Name.GetEnumValue<ItemPropertyType>();
                }
                catch (Exception ex)
                {
                    continue;
                }

                ItemProperty propertyResult = new()
                {
                    Type = type,
                    Value = property.Get<float>(0)
                };

                itemProperties.Add(propertyResult);
            }

            return itemProperties;
        }

        [SupportedOSPlatform("windows")]
        protected virtual async Task<Bitmap?> GetIconAsync(ArzRecord itemRecord, string bitmapPathPropertyName)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(bitmapPathPropertyName);
                string bitmapPath = itemRecord[bitmapPathPropertyName]?.Get<string>(0) ?? string.Empty;

                TexFile texFile = await new GameIconService().GetTexFileByTagAsync(bitmapPath).ConfigureAwait(false);
                return texFile?.ToBitmap();
            }
            catch (Exception ex)
            {
                // Loading .tex failed.
                return null;
            }
        }

        [SupportedOSPlatform("windows")]
        protected static Size GetItemSize(Bitmap icon)
        {
            if (icon is not null)
            {
                int cellWidth = (int)(icon.Size.Width / CellVerticyLength);
                int cellHeight = (int)(icon.Size.Height / CellVerticyLength);
                return new(cellWidth, cellHeight);
            }

            return new Size(1, 1);
        }
    }
}
