using System.Drawing;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    // TODO Move Affixes in here. Shouldn't be used anywhere else
    public class EquipableItem : Item
    {
        public bool HidePrefixName { get; set; }

        public bool HideSuffixName { get; set; }

        public string BaseName { get; set; } = string.Empty;

        public override string Name
        {
            get
            {
                string result = "";

                if (!HidePrefixName && Prefix is not null)
                    result += Prefix.Name + " ";

                result += BaseName;

                if (!HideSuffixName && Suffix is not null)
                    result += " " + Suffix.Name;

                return result;
            }
        }

        public override Color AccentColor => Color.FromArgb(0x10, Color);

        public override Color Color
        {
            get
            {
                return Classification switch
                {
                    ItemClassification.Legendary => TitanQuestColors.Purple,
                    ItemClassification.Epic => TitanQuestColors.Blue,
                    ItemClassification.Rare => TitanQuestColors.YellowGreen,
                    ItemClassification.Common => GetAccentColorByAffixCount(),
                    ItemClassification.Broken => TitanQuestColors.DarkGray,
                    _ => TitanQuestColors.Red
                };
            }
        }

        private Color GetAccentColorByAffixCount()
        {
            int affixCount = 0;

            if (Prefix is not null)
                affixCount++;

            if (Suffix is not null)
                affixCount++;

            return affixCount switch
            {
                2 => TitanQuestColors.Green,
                1 => TitanQuestColors.Yellow,
                0 or _ => TitanQuestColors.Silver,
            };
        }

        public override bool ShowIconAccent
        {
            get
            {
                return Classification switch
                {
                    ItemClassification.Legendary or ItemClassification.Epic or ItemClassification.Rare => true,
                    ItemClassification.Common => ShowIconAccentByAffixCount(),
                    ItemClassification.Common or ItemClassification.Broken => false,
                    _ => false
                };
            }
        }

        private bool ShowIconAccentByAffixCount() => Prefix is not null || Suffix is not null;
    }
}
