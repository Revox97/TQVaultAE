using System.Collections.ObjectModel;
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
        internal override async Task<Item> CreateAsync(Item itemBase, ArzRecord itemRecord)
        {
            try
            {
                ArtifactItem item = new(itemBase)
                {
                    Level = itemRecord["itemLevel"]?.Get<int>(0) ?? 0,
                    Cost = itemRecord["cost"]?.Get<int>(0) ?? 0,
                    Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                    TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty,
                    Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default,
                    ArtifactClassification = itemRecord["artifactClassification"]?.Get<ArtifactClassification>(0) ?? default,
                    Properties = new ObservableCollection<ItemProperty>(GetItemAttributes(itemRecord)),
                    Name = await GetLocalizedValueAsync(itemRecord, "description"),
                    Icon = await GetIconAsync(itemRecord, "artifactBitmap") ?? null!,
                };

                item.Size = GetItemSize(item.Icon);
                return item;
            }
            catch(Exception ex)
            {
                // Item Creation failed.
                return itemBase;
            }
        }
    }
}
