using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class ArtifactItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item item, ArzRecord itemRecord)
        {
            try
            {
                ArtifactItem result = new(item)
                {
                    Level = itemRecord["itemLevel"]?.Get<int>(0) ?? 0,
                    Cost = itemRecord["cost"]?.Get<int>(0) ?? 0,
                    Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                    ArtifactClassification = itemRecord["artifactClassification"]?.Get<ArtifactClassification>(0) ?? default,
                    Properties = GetItemAttributes(itemRecord),
                };

                result = GetGeneralItemProperties(result, itemRecord) as ArtifactItem ?? throw new InvalidCastException("Item is not of type ArtifactItem.");

                string nameTag = itemRecord["description"]?.Get<string>(0) ?? string.Empty;
                item.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                //string descriptionTag = itemRecord["description"]?.Get<string>(0) ?? string.Empty;
                //item.Name = await new GameLocalizationService().GetLocalizedValueByTag(descriptionTag).ConfigureAwait(false) ?? string.Empty;

                string bitmapPath = itemRecord["artifactBitmap"]?.Get<string>(0) ?? string.Empty;
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
    }
}
