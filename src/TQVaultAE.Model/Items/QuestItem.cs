using System.Drawing;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class QuestItem : Item
    {
        public QuestItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
        }

        public override Color Color => TitanQuestColors.Purple;
    }
}
