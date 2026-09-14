using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class WeaponItem : EquipableItem
    {
        public WeaponItemType WeaponType { get; set; }

        public WeaponItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
            WeaponType = GetWeaponTypeFromClass(Class);
        }

        private static WeaponItemType GetWeaponTypeFromClass(ItemClass itemClass)
        {
            return itemClass switch
            {
                ItemClass.WeaponArmor_Shield => WeaponItemType.Shield,
                ItemClass.WeaponHunting_Bow => WeaponItemType.Bow,
                ItemClass.WeaponHunting_RangedOneHand => WeaponItemType.RangedOneHand,
                ItemClass.WeaponHunting_Spear => WeaponItemType.Spear,
                ItemClass.WeaponMagical_Staff => WeaponItemType.Staff,
                ItemClass.WeaponMelee_Axe => WeaponItemType.Axe,
                ItemClass.WeaponMelee_Mace => WeaponItemType.Mace,
                ItemClass.WeaponMelee_Sword => WeaponItemType.Sword,
                _ => default
            };
        }
    }
}
