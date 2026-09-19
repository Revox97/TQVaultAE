using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class ArtifactFormularItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item itemBase, ArzRecord itemRecord)
        {
            try
            {
                ArtifactFormularItem item = new(itemBase)
                {
                    Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                    TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty,
                    Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default,
                    Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord)),
                    Cost = itemRecord["itemCost"]?.Get<int>(0) ?? 0,
                    Name = await GetLocalizedValueAsync(itemRecord, "description"),
                    Icon = await GetIconAsync(itemRecord, "artifactFormulaBitmapName") ?? null!,
                    GameDlc = await GetGameDlcAsync(itemRecord, "description"),
                };

                item.Size = GetItemSize(item.Icon);
                return item;

                // TODO Get component items (reagentiBaseName)
                // TODO Get artifact create cost (artifactCreationCost)
            }
            catch (Exception ex)
            {
                // Item Creation failed.
                return itemBase;
            }
        }
    }
}
