using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    public class Equipment
    {
        private Weapon _weaponOne;

        public Weapon WeaponOne
        {
            get => _weaponOne;
            set
            {
                if (value.RequiredEquipmentSlots == RequiredEquipmentSlots.One)
                {
                    _weaponOne = value;
                    return;
                }

                _weaponOne = value;
                _shieldOne = value;
            }
        }

        private Weapon _weaponTwo;

        public Weapon WeaponTwo
        {
            get => _weaponTwo;
            set
            {
                if (value.RequiredEquipmentSlots == RequiredEquipmentSlots.One)
                {
                    _weaponTwo = value;
                    return;
                }

                _weaponTwo = value;
                _shieldTwo = value;
            }
        }

        private Weapon _shieldOne;

        public Weapon ShieldOne
        {
            get => _shieldOne;
            set
            {
                if (value.RequiredEquipmentSlots == RequiredEquipmentSlots.One)
                {
                    _shieldOne = value;
                    return;
                }

                _weaponOne = value;
                _shieldOne = value;
            }
        }

        private Weapon _shieldTwo;

        public Weapon ShieldTwo
        {
            get => _shieldTwo;
            set
            {
                if (value.RequiredEquipmentSlots == RequiredEquipmentSlots.One)
                {
                    _shieldTwo = value;
                    return;
                }

                _weaponTwo = value;
                _shieldTwo = value;
            }
        }

        public GearItem Head { get; set; }

        public GearItem Torso { get; set; }

        public GearItem Arms { get; set; }

        public GearItem Legs { get; set; }

        public GearItem RingOne { get; set; }

        public GearItem RingTwo { get; set; }

        public GearItem Charm { get; set; }

        public GearItem Artifact { get; set; }
    }
}
