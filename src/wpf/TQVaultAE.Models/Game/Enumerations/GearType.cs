namespace TQVaultAE.Models.Game.Enumerations
{
    public enum GearType
    {
        Undefined = 0,
        //[GearTypeDescription(Item.ICLASS_HEAD, "head")]
        Head = 1 << 0,
        //[GearTypeDescription(Item.ICLASS_UPPERBODY, "upperBody")]
        Torso = 1 << 1,
        //[GearTypeDescription(Item.ICLASS_FOREARM, "forearm")]
        Arm = 1 << 2,
        //[GearTypeDescription(Item.ICLASS_LOWERBODY, "lowerBody")]
        Leg = 1 << 3,
        //[GearTypeDescription(Item.ICLASS_RING, "ring")]
        Ring = 1 << 4,
        //[GearTypeDescription(Item.ICLASS_AMULET, "amulet")]
        Amulet = 1 << 5,
        //[GearTypeDescription(Item.ICLASS_ARTIFACT, "")]
        Artifact = 1 << 6,
        //[GearTypeDescription(Item.ICLASS_SPEAR, "spear")]
        Spear = 1 << 7,
        //[GearTypeDescription(Item.ICLASS_STAFF, "staff")]
        Staff = 1 << 8,
        //[GearTypeDescription(Item.ICLASS_RANGEDONEHAND, "bow")]
        Thrown = 1 << 9,
        //[GearTypeDescription(Item.ICLASS_BOW, "bow")]
        Bow = 1 << 10,
        //[GearTypeDescription(Item.ICLASS_SWORD, "sword")]
        Sword = 1 << 11,
        //[GearTypeDescription(Item.ICLASS_MACE, "mace")]
        Mace = 1 << 12,
        //[GearTypeDescription(Item.ICLASS_AXE, "axe")]
        Axe = 1 << 13,
        //[GearTypeDescription(Item.ICLASS_SHIELD, "shield")]
        Shield = 1 << 14,
        //Unique = 1 << 28,
        MonsterInfrequent = 1 << 29,
        ForMage = 1 << 30,
        ForMelee = 1 << 31,
        Jewellery = Ring | Amulet,
        AllArmor = Head | Torso | Arm | Leg,
        AllWeapons = Spear | Staff | Thrown | Bow | Sword | Mace | Axe,
        AllWearable = AllWeapons | AllArmor | Jewellery | Shield,
    }
}
