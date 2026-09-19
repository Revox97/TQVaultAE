using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Localisation;
using TQVaultAE.Model.Enumerations;
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
                QuestItem item = new(itemBase)
                {
                    TemplateName = itemRecord["templateName"]?.Get<string>(0) ?? string.Empty,
                    Classification = itemRecord["itemClassification"]?.Get<ItemClassification>(0) ?? default,
                    Scale = itemRecord["scale"]?.Get<float>(0) ?? 0.0f,
                    Icon = await GetIconAsync(itemRecord, "bitmap") ?? null!,
                    Requirements = new ObservableCollection<ItemRequirement>(GetItemRequirements(itemRecord)),
                };

                // There seem to be multiple types of quest items, staffs have description as name
                string nameTag = itemRecord["description"]?.Get<string>(0) ?? string.Empty;
                item.Name = await new GameLocalizationService().GetLocalizedValueByTag(nameTag).ConfigureAwait(false) ?? string.Empty;

                string descriptionTag = itemRecord["itemText"]?.Get<string>(0) ?? string.Empty;
                item.Description = await new GameLocalizationService().GetLocalizedValueByTag(descriptionTag).ConfigureAwait(false) ?? string.Empty;

                item.Size = GetItemSize(item.Icon);
                return item;
            }
            catch (Exception ex)
            {
                // Item Creation failed.
                return itemBase;
            }
        }
    }
}
