using Avalonia;
using Avalonia.Media.Imaging;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.FileFormats.Tex;
using TQVaultAE.Localisation;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.Items.ItemProperties;

namespace TQVaultAE.Application.Factories.ItemCreationStrategies
{
    internal abstract class ItemCreationStrategy
    {
        protected const int CellVerticyLength = 32;

        internal abstract Task<Item> CreateAsync(Item item, ArzRecord itemRecord);

        protected virtual async Task<Affix?> GetCompleteAffixAsync(Affix? affixBase)
        {
            if (affixBase is null)
                return affixBase;

            ArzRecord affixRecord = await new TitanQuestDatabaseService().GetRecordByPathAsync(affixBase.Path);

            Affix affix = new()
            {
                Path = affixBase.Path,
                Name = await GetLocalizedValueAsync(affixRecord, "lootRandomizerName"),
                Properties = GetItemProperties(affixRecord), // TODO The tag property needs to be filtered.
                Requirements = GetItemRequirements(affixRecord),
                MarketAdjustmentPercent = affixRecord["marketAdjustmentPercent"]?.GetSingle(0) ?? 0.0f
            };

            //affix.Format = await GetLocalizedValueAsync(affixRecord, "characterBaseAttackSpeedTag"); // TODO These will for sure be different every time.
            return affix;
        }

        protected virtual async Task<string> GetLocalizedValueAsync(ArzRecord itemRecord, string itemNamePropertyName)
        {
            string nameTag = itemRecord[itemNamePropertyName]?.Get<string>(0) ?? string.Empty;
            return await new GameLocalizationService().GetLocalizedValueByTagAsync(nameTag).ConfigureAwait(false) ?? string.Empty;
        }


        protected virtual async Task<GameDlc> GetGameDlcAsync(ArzRecord itemRecord, string itemNamePropertyName)
        {
            string nameTag = itemRecord[itemNamePropertyName]?.Get<string>(0) ?? string.Empty;

            if (nameTag.StartsWith("x4"))
                return GameDlc.EternalEmbers;

            if (nameTag.StartsWith("x3"))
                return GameDlc.Atlantis;

            if (nameTag.StartsWith("x2"))
                return GameDlc.Ragnarok;

            if (nameTag.StartsWith('x'))
                return GameDlc.ImmortalThrone;

            return GameDlc.TitanQuest;
        }

        protected virtual List<ItemRequirement> GetItemRequirements(ArzRecord itemRecord)
        {
            List<ArzRecordProperty> validProperties = [.. itemRecord.Properties.Where(x => x.IsValueRelevant && x.Name.EndsWith("Requirement"))];

            List<ItemRequirement> itemRequirements = [];

            // TODO Many requirements seem not to be within the main database.
            // Figure out, where  they are comming from.
            // Maybe this is a calculation on some stats.
            foreach (ArzRecordProperty property in validProperties)
            {
                ItemRequirementType type;
                try
                {
                    type = property.Name.GetEnumValue<ItemRequirementType>();
                }
                catch (Exception ex)
                {
                    continue;
                }

                ItemRequirement requirementResult = new(type, property.Get<int>(0));
                itemRequirements.Add(requirementResult);
            }

            return itemRequirements;
        }

        protected virtual async Task<List<ItemSkillAugment>> GetSkillAugmentsAsync(ArzRecord itemRecord)
        {
            List<ItemSkillAugment> itemSkillAugments = [];
            int augmentAllLevel = itemRecord["augmentAllLevel"]?.Get<int>(0) ?? 0;

            if (augmentAllLevel > 0)
            {
                itemSkillAugments.Add(new ItemSkillAugment(string.Empty, augmentAllLevel)
                {
                    SkillName = "all Skills" // TODO localize
                });
            }

            List<ArzRecordProperty?> skillAugmentNames = [.. itemRecord.Properties.Where(x => x.Name.StartsWith("augmentSkillName"))];

            foreach (ArzRecordProperty skillAugmentName in skillAugmentNames.Where(x => x is not null && x.IsValueRelevant).Cast<ArzRecordProperty>())
            {
                string matchingPropertyName = $"augmentSkillLevel{skillAugmentName.Name[^1..]}";
                ArzRecordProperty? skillAugmentLevel = itemRecord.Properties.SingleOrDefault(x => x.Name == matchingPropertyName);

                if (skillAugmentLevel is not null && skillAugmentLevel.IsValueRelevant)
                {
                    string? skillResourcePath = skillAugmentName.GetString();
                    int skillValue = skillAugmentLevel.GetInt32();

                    if (skillResourcePath is null)
                        continue;

                    ArzRecord skillRecord = await new TitanQuestDatabaseService().GetRecordByPathAsync(skillResourcePath).ConfigureAwait(false);
                    string skillName = await new GameLocalizationService().GetLocalizedValueByTagAsync(skillRecord["skillDisplayName"]?.Get<string>(0) ?? string.Empty) ?? string.Empty;

                    if (string.IsNullOrEmpty(skillName))
                        continue;

                    itemSkillAugments.Add(new ItemSkillAugment(skillResourcePath, skillValue)
                    {
                        SkillName = skillName
                    });
                }
            }

            return itemSkillAugments;
        }

        protected virtual List<ItemProperty> GetItemProperties(ArzRecord itemRecord)
        {
            return new ItemPropertiesFactory().CreateProperties(itemRecord);
        }

        protected virtual async Task<Bitmap?> GetIconAsync(ArzRecord itemRecord, string bitmapPathPropertyName)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(bitmapPathPropertyName);
                string bitmapPath = itemRecord[bitmapPathPropertyName]?.Get<string>(0) ?? string.Empty;

                TexFile texFile = await new GameIconService().GetTexFileByTagAsync(bitmapPath).ConfigureAwait(false);
                return texFile?.ToBitmap();
            }
            catch (Exception ex)
            {
                // Loading .tex failed.
                return null;
            }
        }

        protected static Size GetItemSize(Bitmap icon)
        {
            if (icon is not null)
            {
                int cellWidth = (int)(icon.Size.Width / CellVerticyLength);
                int cellHeight = (int)(icon.Size.Height / CellVerticyLength);
                return new(cellWidth, cellHeight);
            }

            return new Size(1, 1);
        }
    }
}
