using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal class QuestItemCreationStrategy : ItemCreationStrategy
    {
        [SupportedOSPlatform("windows")]
        internal override async Task<Item> CreateAsync(Item itemBase, ArzRecord itemRecord)
        {
            try
            {
                itemBase = GetGeneralItemProperties(itemBase, itemRecord);
                itemBase.Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f;

                // There seem to be multiple types of quest items, staffs have description as name
                string nameTag = itemRecord["itemText"]?.Get<string>(0) ?? string.Empty;
                itemBase.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                string descriptionTag = $"{nameTag[..^2]}Desc{nameTag[^2..]}";
                itemBase.Description = await new GameLocalizationService().GetLocalizedValueByTag(descriptionTag).ConfigureAwait(false) ?? string.Empty;
                itemBase.Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord));

                string bitmapPath = itemRecord["bitmap"]?.Get<string>(0) ?? string.Empty;
                itemBase.Icon = await GetIconAsync(bitmapPath).ConfigureAwait(false) ?? null!; // TODO use default bitmap in case reading fails.
                itemBase.Size = GetItemSize(itemBase);

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
