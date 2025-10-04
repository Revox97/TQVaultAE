using System.IO;
using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.IO.Records
{
    public partial class RecordId
    {
        private bool? _isBroken;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Broken content.
        /// </summary>
        public bool IsBroken
        {
            get
            {
                _isBroken ??= Normalized.Contains(@"\BROKEN\");
                return _isBroken.Value;
            }
        }

        private bool? _isSuffix;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Suffix content.
        /// </summary>
        public bool IsSuffix
        {
            get
            {
                _isSuffix ??= Normalized.Contains(@"\SUFFIX\");
                return _isSuffix.Value;
            }
        }

        private bool? _isPrefix;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Prefix content.
        /// </summary>
        public bool IsPrefix
        {
            get
            {
                _isPrefix ??= Normalized.Contains(@"\PREFIX\");
                return _isPrefix.Value;
            }
        }

        private bool? _isLootMagicalAffixes;

        /// <summary>
        /// This <see cref="RecordId"/> leads to LootMagicalAffixes content.
        /// </summary>
        public bool IsLootMagicalAffixes
        {
            get
            {
                _isLootMagicalAffixes ??= Normalized.Contains(@"\LOOTMAGICALAFFIXES\");
                return _isLootMagicalAffixes.Value;
            }
        }

        private bool? _isTablesWeapon;

        /// <summary>
        /// This <see cref="RecordId"/> leads to TablesUnique content.
        /// </summary>
        public bool IsTablesWeapon
        {
            get
            {
                _isTablesWeapon ??= Normalized.Contains(@"\TABLESWEAPON") || Path.GetFileName(Normalized).StartsWith(@"TABLE_WEAPON");
                return _isTablesWeapon.Value;
            }
        }

        private bool? _isTablesUnique;

        /// <summary>
        /// This <see cref="RecordId"/> leads to TablesUnique content.
        /// </summary>
        public bool IsTablesUnique
        {
            get
            {
                _isTablesUnique ??= Normalized.Contains(@"\TABLESUNIQUE");
                return _isTablesUnique.Value;
            }
        }

        private bool? _isTablesShields;

        /// <summary>
        /// This <see cref="RecordId"/> leads to TablesJewelry content.
        /// </summary>
        public bool IsTablesShields
        {
            get
            {
                _isTablesShields ??= Normalized.Contains(@"\TABLESSHIELD") || Path.GetFileName(Normalized).StartsWith(@"TABLE_SHIELD");
                return _isTablesShields.Value;
            }
        }

        private bool? _isTablesJewelry;

        /// <summary>
        /// This <see cref="RecordId"/> leads to TablesJewelry content.
        /// </summary>
        public bool IsTablesJewelry
        {
            get
            {
                _isTablesJewelry ??= Normalized.Contains(@"\TABLESJEWELRY");
                return _isTablesJewelry.Value;
            }
        }

        private bool? _isTablesArmor;

        /// <summary>
        /// This <see cref="RecordId"/> leads to TablesArmor content.
        /// </summary>
        public bool IsTablesArmor
        {
            get
            {
                _isTablesArmor ??= Normalized.Contains(@"\TABLESARMOR") || Path.GetFileName(Normalized).StartsWith(@"TABLE_ARMOR");
                return _isTablesArmor.Value;
            }
        }

        GearType? _lootTableGearType;

        /// <summary>
        /// return GearType based on file naming rules for loot table <see cref="RecordId"/>.
        /// </summary>
        public GearType LootTableGearType
        {
            get
            {
                if (!IsLootMagicalAffixes) return GearType.Undefined;

                _lootTableGearType ??= Path.GetFileName(Normalized) switch
                {
                    var x when x.StartsWith(@"ARMSMAGE") || x.StartsWith(@"ARMMAGE") => GearType.Arm | GearType.ForMage,
                    var x when x.StartsWith(@"ARMSMELEE") || x.StartsWith(@"ARMMELEE") => GearType.Arm | GearType.ForMelee,
                    var x when x.StartsWith(@"HEADMAGE") => GearType.Head | GearType.ForMage,
                    var x when x.StartsWith(@"HEADMELEE") => GearType.Head | GearType.ForMelee,
                    var x when x.StartsWith(@"LEGSMAGE") || x.StartsWith(@"LEGMAGE") => GearType.Leg | GearType.ForMage,
                    var x when x.StartsWith(@"LEGSMELEE") || x.StartsWith(@"LEGMELEE") => GearType.Leg | GearType.ForMelee,
                    var x when x.StartsWith(@"TORSOMAGE") => GearType.Torso | GearType.ForMage,
                    var x when x.StartsWith(@"TORSOMELEE") => GearType.Torso | GearType.ForMelee,
                    var x when x.StartsWith(@"RING") => GearType.Ring,
                    var x when x.StartsWith(@"AMULET") => GearType.Amulet,
                    var x when x.StartsWith(@"SHIELD") => GearType.Shield,
                    var x when x.StartsWith(@"AXE") => GearType.Axe,
                    var x when x.StartsWith(@"BOW") => GearType.Bow,
                    var x when x.StartsWith(@"CLUB") => GearType.Mace,
                    var x when x.StartsWith(@"ROH") => GearType.Thrown,
                    var x when x.StartsWith(@"SPEAR") => GearType.Spear,
                    var x when x.StartsWith(@"STAFF") => GearType.Staff,
                    var x when x.StartsWith(@"SWORD") => GearType.Sword,
                    // For Broken Affixes
                    var x when x.StartsWith(@"TABLE_ARMOR") => GearType.AllArmor,
                    var x when x.StartsWith(@"TABLE_SHIELD") => GearType.Shield,
                    var x when x.StartsWith(@"TABLE_WEAPONSCLUB") => GearType.Mace,
                    var x when x.StartsWith(@"TABLE_WEAPONSMETAL") => GearType.Sword | GearType.Axe | GearType.Thrown,
                    var x when x.StartsWith(@"TABLE_WEAPONSWOOD") => GearType.Spear | GearType.Staff | GearType.Bow,
                    //RECORDS\XPACK4\ITEM\LOOTMAGICALAFFIXES\SUFFIX\TABLESARMOR\CHINAMONSTERSUFFIX_L05.DBR
                    //RECORDS\XPACK4\ITEM\LOOTMAGICALAFFIXES\SUFFIX\TABLESARMOR\EGYPTMONSTERSUFFIX_L05.DBR
                    var x when x.Contains(@"MONSTER") => GearType.MonsterInfrequent,
                    _ => GearType.Undefined,
                };
                return _lootTableGearType.Value;
            }
        }
    }
}
