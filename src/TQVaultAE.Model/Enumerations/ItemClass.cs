using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ItemClass
    {
        ArmorJewelry_Amulet,
        ArmorJewelry_Ring,
        ArmorProtective_Forearm,
        ArmorProtective_Head,
        ArmorProtective_LowerBody,
        ArmorProtective_UpperBody,
        ItemArtifact,
        ItemArtifactFormula,
        ItemCharm,
        ItemEquipment,
        LootRandomizer,
        OneShot_Dye,
        OneShot_Scroll,
        OneShot_Scroll_Eternal,
        Quest,
        QuestItem,
        WeaponArmor_Shield,
        WeaponHunting_Bow,
        WeaponHunting_RangedOneHand,
        WeaponHunting_Spear,
        WeaponMagical_Staff,
        WeaponMelee_Axe,
        WeaponMelee_Mace,
        WeaponMelee_Sword,
    }
}
