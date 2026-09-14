using System.Drawing;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class OneShotItem : Item
    {
        public List<OneShotBonus> Bonuses { get; set; } = [];

        public OneShotItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
            CanStack = true;
        }

        public override bool ShowIconAccent => false;

        public override Color Color
        {
            get
            {
                // TODO Verify colors
                return Class switch
                {
                    ItemClass.OneShot_PotionHealth => TitanQuestColors.Red,
                    ItemClass.OneShot_PotionMana => TitanQuestColors.Blue,
                    ItemClass.OneShot_Scroll => TitanQuestColors.Khaki,
                    ItemClass.OneShot_Scroll_Eternal => TitanQuestColors.Red,
                    ItemClass.OneShot_Dye => TitanQuestColors.DarkGray,
                    _ => TitanQuestColors.Red,
                };
            }
        }

        public override Color AccentColor
        {
            get
            {
                // TODO Verify colors
                return Class switch
                {
                    ItemClass.OneShot_PotionHealth
                        or ItemClass.OneShot_PotionMana
                        or ItemClass.OneShot_Scroll_Eternal
                        or ItemClass.OneShot_Dye
                        => Color.FromArgb(0x10, TitanQuestColors.Silver),
                    ItemClass.OneShot_Scroll => Color.FromArgb(0x10, TitanQuestColors.Khaki),
                    _ => TitanQuestColors.Red,
                };
            }
        }
    }
}
