using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class ArmorItem : EquipableItem
    {
        public ArmorItemType ArmorType { get; set; }

        public ArmorItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
            ArmorType = GetArmorTypeFromClass(Class);
        }

        private static ArmorItemType GetArmorTypeFromClass(ItemClass itemClass)
        {
            return itemClass switch
            {
                ItemClass.ArmorProtective_Head => ArmorItemType.Head,
                ItemClass.ArmorProtective_Forearm => ArmorItemType.Arms,
                ItemClass.ArmorProtective_LowerBody => ArmorItemType.Legs,
                ItemClass.ArmorProtective_UpperBody => ArmorItemType.Body,
                _ => default
            };
        }
    }
}
