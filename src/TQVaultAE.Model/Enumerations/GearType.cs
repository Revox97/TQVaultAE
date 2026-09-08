using TQVaultAE.Model.Attributes;

namespace TQVaultAE.Model.Enumerations
{
    // TODO Figure out, whether there is a reason for these  ridiculous enum values and the resulting int overflow at << 32
    [Flags]
    public enum GearType
    {
        Undefined = 0,
        //[GearTypeDescription(Item.ICLASS_HEAD, "head")]
        Head = 1,
        //[GearTypeDescription(Item.ICLASS_UPPERBODY, "upperBody")]
        Torso = 2,
        //[GearTypeDescription(Item.ICLASS_FOREARM, "forearm")]
        Arm = 4,
        //[GearTypeDescription(Item.ICLASS_LOWERBODY, "lowerBody")]
        Leg = 8,
        //[GearTypeDescription(Item.ICLASS_RING, "ring")]
        Ring = 16,
        //[GearTypeDescription(Item.ICLASS_AMULET, "amulet")]
        Amulet = 32,
        //[GearTypeDescription(Item.ICLASS_ARTIFACT, "")]
        Artifact = 64,
        //[GearTypeDescription(Item.ICLASS_SPEAR, "spear")]
        Spear = 128,
        //[GearTypeDescription(Item.ICLASS_STAFF, "staff")]
        Staff = 256,
        //[GearTypeDescription(Item.ICLASS_RANGEDONEHAND, "bow")]
        Thrown = 512,
        //[GearTypeDescription(Item.ICLASS_BOW, "bow")]
        Bow = 1024,
        //[GearTypeDescription(Item.ICLASS_SWORD, "sword")]
        Sword = 2048,
        //[GearTypeDescription(Item.ICLASS_MACE, "mace")]
        Mace = 4096,
        //[GearTypeDescription(Item.ICLASS_AXE, "axe")]
        Axe = 8192,
        //[GearTypeDescription(Item.ICLASS_SHIELD, "shield")]
        Shield = 16384,
        //Unique = 268435456;
        MonsterInfrequent = 536870912,
        ForMage = 1073741824,
        ForMelee = 1 << 31,
        Jewellery = Ring | Amulet,
        AllArmor = Head | Torso | Arm | Leg,
        AllWeapons = Spear | Staff | Thrown | Bow | Sword | Mace | Axe,
        AllWearable = AllWeapons | AllArmor | Jewellery | Shield,
    }
}
