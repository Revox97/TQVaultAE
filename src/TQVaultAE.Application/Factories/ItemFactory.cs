using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;
using TQVaultAE.TitanQuestDataProviders.Database;
using TQVaultAE.TitanQuestDataProviders.Model;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Application.Services;

namespace TQVaultAE.Application.Factories
{
    /// <summary>
    /// Represents a factory, that creates an  <see cref="Item"/>.
    /// </summary>
    public class ItemFactory
    {
        // TODO Make dynamic, hardcoded for testing purposes.
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TQVaultTestData", "database.arz");
        private static ArzFile? s_database;

        public async Task<Item> GetCompleteItemAsync(Item item)
        {
            string itemDbPath = item.ResourcePath;

            s_database ??= await new ArzProvider().ReadAsync(_dbPath).ConfigureAwait(false);

            ArzRecord itemRecord = s_database.GetRecordByPath(itemDbPath);
            return await CreateItemByClass(item, itemRecord).ConfigureAwait(false);
        }

        private async Task<Item> CreateItemByClass(Item item, ArzRecord itemRecord)
        {
            ItemClass? itemClass = itemRecord["Class"]?.Get<ItemClass>(0);

            if (itemClass is null)
                return item;

            return itemClass switch
            {
                ItemClass.ArmorProtective_Head or ItemClass.ArmorProtective_LowerBody => await CreateArmorItem(item, itemRecord).ConfigureAwait(false),
                ItemClass.ItemArtifact => await CreateArtifactItem(item, itemRecord).ConfigureAwait(false),
                _ => item
            };
        }

        private async Task<Item> CreateArtifactItem(Item item, ArzRecord itemRecord)
        {
            ArtifactItem result = new(item)
            {
                TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty,
                Level = itemRecord["itemLevel"]?.Get<int>(0) ?? 0,
                Cost = itemRecord["cost"]?.Get<int>(0) ?? 0,
                Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default,
                Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                Name = itemRecord["FileDescription"]?.Get<string>(0) ?? string.Empty,
                ArtifactClassification = itemRecord["artifactClassification"]?.Get<ArtifactClassification>(0) ?? default,
                Properties = GetProperties(itemRecord),
            };

            string bitmapPath = itemRecord["bitmap"]?.Get<string>(0) ?? string.Empty;
            // TODO Read bitmap from .tex file
            //Bitmap bitmap = ReadBitmap(bitmapPath);
            //item.Icon = bitmap;

            return result;
        }

        private static async Task<Item> CreateArmorItem(Item item, ArzRecord itemRecord)
        {
            // TODO Get names from C:\Program Files (x86)\Steam\steamapps\common\Titan Quest Anniversary Edition\Texts using the correct localization
            item.Properties = GetProperties(itemRecord);
            item.TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty;
            item.Level = itemRecord["itemLevel"]?.Get<int>(0) ?? 0;
            item.Cost = itemRecord["cost"]?.Get<int>(0) ?? 0;
            item.Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default;
            item.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;
            // TODO Get item requirements, that are not 0.0f. Seem to end with Requirement:
            // item.Requirements = GetRequirements(itemRecord);

            string bitmapPath = itemRecord["bitmap"]?.Get<string>(0) ?? string.Empty;
            // TODO Read bitmap from .tex file
            //Bitmap bitmap = ReadBitmap(bitmapPath);
            //item.Icon = bitmap;

            await new GameLocalizationService().GetLocalizationAsync().ConfigureAwait(false);

            return item;
        }

        // TODO Add overload to get all item properties (min, max, modifier) that are not 0.0f -> seem to start with:
        // - offensive
        // - defensive
        // - retaliation
        // - skill
        private static List<ItemProperty> GetProperties(ArzRecord itemRecord)
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
    }
}
