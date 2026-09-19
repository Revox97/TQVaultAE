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
                    Name = await GetLocalizedValueAsync(itemRecord, "description"),
                    Description = await GetLocalizedValueAsync(itemRecord, "itemText"),
                    GameDlc = await GetGameDlcAsync(itemRecord, "description"),
                };

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
