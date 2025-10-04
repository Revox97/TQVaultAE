namespace TQVaultAE.IO.Records
{
    public partial class RecordId
    {
        private bool? _IsHardCoreDungeonEE;

        /// <summary>
        /// This <see cref="RecordId"/> leads to the EE Hardcore Dungeon.
        /// </summary>
        public bool IsHardCoreDungeonEE
        {
            get
            {
                _IsHardCoreDungeonEE ??= Normalized.Contains(@"\HCDUNGEON\");
                return _IsHardCoreDungeonEE.Value;
            }
        }

        private readonly string[] _hcDungeonRelic = ["03_X4_ESSENCEOFORDER_CHARM", "03_X4_ESSENCEOFCHAOS"];

        private bool? _isRelic;

        /// <summary>
        /// This <see cref="RecordId"/> leads to a Relic content.
        /// </summary>
        public bool IsRelic
        {
            get
            {
                _isRelic ??= (Dlc == GameDlc.TitanQuest && Normalized.Contains(@"RELICS") && !IsCharm) // Is base game
                          || Normalized.Contains(@"\RELICS\") // Is part of an extension
                          || (IsHardCoreDungeonEE && _hcDungeonRelic.Any(n => Normalized.Contains(n))); // items that break the rule
                return _isRelic.Value;
            }
        }

        private bool? _isCharm;

        /// <summary>
        /// This <see cref="RecordId"/> leads to a Charm content.
        /// </summary>
        public bool IsCharm
        {
            get
            {
                _isCharm ??= (Dlc == GameDlc.TitanQuest && Normalized.Contains(@"ANIMALRELICS")) // Is base game
                          || Normalized.Contains(@"\CHARMS\")// Is part of an extension
                          || (IsHardCoreDungeonEE && Normalized.Contains(@"GOLDENSCARAB"));// items that break the rule
                return _isCharm.Value;
            }
        }

        private bool? _isPotion;

        /// <summary>
        /// This <see cref="RecordId"/> leads to a Potion content.
        /// </summary>
        public bool IsPotion
        {
            get
            {
                _isPotion ??= Normalized.Contains(@"ONESHOT\POTION");
                return _isPotion.Value;
            }
        }

        private bool? _isQuestItem;

        /// <summary>
        /// This <see cref="RecordId"/> leads to a Quest Item content.
        /// </summary>
        public bool IsQuestItem
        {
            get
            {
                _isQuestItem ??= (Dlc == GameDlc.TitanQuest && Normalized.Contains(@"QUEST")) // Is base game
                              || Normalized.Contains(@"QUESTS");// Is part of an extension
                return _isQuestItem.Value;
            }
        }

        private bool? _isArtifact;

        /// <summary>
        /// This <see cref="RecordId"/> leads to an Artifact content.
        /// </summary>
        public bool IsArtifact
        {
            get
            {
                _isArtifact ??= !IsFormulae && Normalized.Contains(@"\ARTIFACTS\");
                return _isArtifact.Value;
            }
        }

        private bool? _isFormula;

        /// <summary>
        /// This <see cref="RecordId"/> leads to an Arcane Formulae content.
        /// </summary>
        public bool IsFormulae
        {
            get
            {
                _isFormula ??= Normalized.Contains(@"\ARCANEFORMULAE\");
                return _isFormula.Value;
            }
        }

        private bool? _isParchment;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Parchment content.
        /// </summary>
        public bool IsParchment
        {
            get
            {
                _isParchment ??= Normalized.Contains(@"PARCHMENT");
                return _isParchment.Value;
            }
        }

        private bool? _isScroll;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Scroll content.
        /// </summary>
        public bool IsScroll
        {
            get
            {
                _isScroll ??= Normalized.Contains(@"\SCROLLS\");
                return _isScroll.Value;
            }
        }

        private bool? _isItem;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Items content.
        /// </summary>
        public bool IsItem
        {
            get
            {
                _isItem ??= Normalized.Contains(@"\ITEM\") || Normalized.Contains(@"\ITEMS\");
                return _isItem.Value;
            }
        }

        private bool? _isEquipmentWeapon;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Weapon content.
        /// </summary>
        public bool IsEquipmentWeapon
        {
            get
            {
                _isEquipmentWeapon ??= Normalized.Contains(@"\EQUIPMENTWEAPON");// Exist with an S
                return _isEquipmentWeapon.Value;
            }
        }

        private bool? _isEquipmentWeaponAxe;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Axe content.
        /// </summary>
        public bool IsEquipmentWeaponAxe
        {
            get
            {
                _isEquipmentWeaponAxe ??= IsEquipmentWeapon && Normalized.Contains(@"\AXE\");
                return _isEquipmentWeaponAxe.Value;
            }
        }

        private bool? _isEquipmentWeaponBow;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Bow content.
        /// </summary>
        public bool IsEquipmentWeaponBow
        {
            get
            {
                _isEquipmentWeaponBow ??= IsEquipmentWeapon && Normalized.Contains(@"\BOW\");
                return _isEquipmentWeaponBow.Value;
            }
        }

        private bool? _isEquipmentWeaponMace;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Bow content.
        /// </summary>
        public bool IsEquipmentWeaponMace
        {
            get
            {
                _isEquipmentWeaponMace ??= IsEquipmentWeapon && Normalized.Contains(@"\CLUB\");
                return _isEquipmentWeaponMace.Value;
            }
        }

        private bool? _isEquipmentWeaponSpear;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Spear content.
        /// </summary>
        public bool IsEquipmentWeaponSpear
        {
            get
            {
                _isEquipmentWeaponSpear ??= IsEquipmentWeapon && Normalized.Contains(@"\SPEAR\");
                return _isEquipmentWeaponSpear.Value;
            }
        }

        private bool? _isEquipmentWeaponStaff;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Staff content.
        /// </summary>
        public bool IsEquipmentWeaponStaff
        {
            get
            {
                _isEquipmentWeaponStaff ??= IsEquipmentWeapon && Normalized.Contains(@"\STAFF\");
                return _isEquipmentWeaponStaff.Value;
            }
        }

        private bool? _isEquipmentWeaponThrown;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Thrown content.
        /// </summary>
        public bool IsEquipmentWeaponThrown
        {
            get
            {
                _isEquipmentWeaponThrown ??= IsEquipmentWeapon && Normalized.Contains(@"\1HRANGED\");
                return _isEquipmentWeaponThrown.Value;
            }
        }

        private bool? _isEquipmentWeaponSword;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Sword content.
        /// </summary>
        public bool IsEquipmentWeaponSword
        {
            get
            {
                _isEquipmentWeaponSword ??= IsEquipmentWeapon && Normalized.Contains(@"\SWORD\");
                return _isEquipmentWeaponSword.Value;
            }
        }

        private bool? _isEquipmentShield;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Shields content.
        /// </summary>
        public bool IsEquipmentShield
        {
            get
            {
                _isEquipmentShield ??= Normalized.Contains(@"\EQUIPMENTSHIELD") || (IsEquipmentWeapon && Normalized.Contains(@"\SHIELD\"));
                return _isEquipmentShield.Value;
            }
        }

        private bool? _isEquipmentRing;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Rings content.
        /// </summary>
        public bool IsEquipmentRing
        {
            get
            {
                _isEquipmentRing ??= Normalized.Contains(@"\EQUIPMENTRING") || Normalized.Contains(@"\EQUIPMENTARMOR\RING\");
                return _isEquipmentRing.Value;
            }
        }

        private bool? _isEquipmentHelm;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Helm content.
        /// </summary>
        public bool IsEquipmentHelm
        {
            get
            {
                _isEquipmentHelm ??= Normalized.Contains(@"\EQUIPMENTHELM") || Normalized.Contains(@"\EQUIPMENTARMOR\HELM\");
                return _isEquipmentHelm.Value;
            }
        }

        private bool? _isEquipmentGreaves;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Greaves content.
        /// </summary>
        public bool IsEquipmentGreaves
        {
            get
            {
                _isEquipmentGreaves ??= Normalized.Contains(@"\EQUIPMENTGREAVES") || Normalized.Contains(@"\EQUIPMENTARMOR\GREAVES\");
                return _isEquipmentGreaves.Value;
            }
        }

        private bool? _isEquipmentTorso;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Torso content.
        /// </summary>
        public bool IsEquipmentTorso
        {
            get
            {
                _isEquipmentTorso ??= (Dlc == GameDlc.TitanQuest && Normalized.Contains(@"\EQUIPMENTARMOR\"))
                                   || (Dlc != GameDlc.TitanQuest && Normalized.Contains(@"\EQUIPMENTARMOR\TORSO\"));
                return _isEquipmentTorso.Value;
            }
        }

        private bool? _isEquipmentAmulet;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Amulet content.
        /// </summary>
        public bool IsEquipmentAmulet
        {
            get
            {
                _isEquipmentAmulet ??= Normalized.Contains(@"\EQUIPMENTAMULET") || Normalized.Contains(@"\EQUIPMENTARMOR\AMULET\");
                return _isEquipmentAmulet.Value;
            }
        }

        private bool? _isEquipmentArmband;

        /// <summary>
        /// This <see cref="RecordId"/> leads to Equipment Armband content.
        /// </summary>
        public bool IsEquipmentArmband
        {
            get
            {
                _isEquipmentArmband ??= Normalized.Contains(@"\EQUIPMENTARMBAND") || Normalized.Contains(@"\EQUIPMENTARMOR\ARMBAND\");
                return _isEquipmentArmband.Value;
            }
        }
    }
}
