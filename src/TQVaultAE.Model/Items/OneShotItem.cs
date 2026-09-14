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

        public override Color AccentColor => Color.FromArgb(0x10, TitanQuestColors.Silver);
    }
}
