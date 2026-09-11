using System.Drawing;
using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.FileFormats.Tex;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal abstract class ItemCreationStrategy
    {
        protected const int CellVerticyLength = 32;

        internal abstract Task<Item> CreateAsync(Item item, ArzRecord itemRecord);

        protected static Item GetGeneralItemProperties(Item item, ArzRecord itemRecord)
        {
            item.TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty;


            // TODO Seems not to be a standard value
            item.Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default;

            return item;
        }

        protected virtual List<ItemRequirement> GetItemRequirements(ArzRecord itemRecord)
        {
            List<ArzRecordProperty> validProperties = [.. itemRecord.Properties.Where(x => x.IsValueRelevant && x.Name.EndsWith("Requirement"))];

            List<ItemRequirement> itemRequirements = [];
            foreach(ArzRecordProperty property in validProperties)
            {
                ItemRequirementType type;
                try
                {
                    type = property.Name.GetEnumValue<ItemRequirementType>();
                }
                catch(Exception ex)
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
            foreach(ArzRecordProperty property in validProperties)
            {
                ItemPropertyType type;
                try
                {
                    type = property.Name.GetEnumValue<ItemPropertyType>();
                }
                catch(Exception ex)
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
        protected virtual async Task<Bitmap?> GetIconAsync(string bitmapPath)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(bitmapPath);

                TexFile texFile = await new GameIconService().GetTexFileByTagAsync(bitmapPath).ConfigureAwait(false);
                return texFile?.ToBitmap();
            }
            catch(Exception ex)
            {
                // Loading .tex failed.
                return null;
            }
        }

        [SupportedOSPlatform("windows")]
        protected static Size GetItemSize(Item item)
        {
            if (item.Icon is not null)
            {
                int cellWidth = item.Icon.Width / CellVerticyLength;
                int cellHeight = item.Icon.Height / CellVerticyLength;
                return new(cellWidth, cellHeight);
            }

            return new Size(1, 1);
        }
    }
}
