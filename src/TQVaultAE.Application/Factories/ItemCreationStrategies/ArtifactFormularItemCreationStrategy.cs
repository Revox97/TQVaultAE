using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
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
                itemBase = GetGeneralItemProperties(itemBase, itemRecord);
                itemBase.Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord));
                itemBase.Cost = itemRecord["itemCost"]?.Get<int>(0) ?? 0;

                string nameTag = itemRecord["description"]?.Get<string>(0) ?? string.Empty;
                itemBase.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                string bitmapPath = itemRecord["artifactFormulaBitmapName"]?.Get<string>(0) ?? string.Empty;
                itemBase.Icon = await GetIconAsync(bitmapPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.
                itemBase.Size = GetItemSize(itemBase);

                // TODO Get component items (reagentiBaseName)
                // TODO Get artifact create cost (artifactCreationCost)

                return itemBase;
            }
            catch(Exception ex)
            {
                // Item Creation failed.
                return itemBase;
            }
        }
    }
}
