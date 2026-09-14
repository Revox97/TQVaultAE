using System.Drawing;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    // TODO Find more meaningful name
    public class ItemEquipmentItem : Item
    {
        public ItemEquipmentItem(Item item)
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

        public override Color Color => TitanQuestColors.Blue;
    }
}
