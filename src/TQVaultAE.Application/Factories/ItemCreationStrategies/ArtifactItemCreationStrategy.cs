using System.Runtime.Versioning;
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
                    Name = itemRecord["FileDescription"]?.Get<string>(0) ?? string.Empty,
                    ArtifactClassification = itemRecord["artifactClassification"]?.Get<ArtifactClassification>(0) ?? default,
                    Properties = GetItemAttributes(itemRecord),
                };

                result = (ArtifactItem)GetGeneralItemProperties(item, itemRecord);

                string bitmapPath = itemRecord["artifactBitmap"]?.Get<string>(0) ?? string.Empty;
                item.Icon = await GetIconAsync(bitmapPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.
                item.Size = GetItemSize(item);

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
