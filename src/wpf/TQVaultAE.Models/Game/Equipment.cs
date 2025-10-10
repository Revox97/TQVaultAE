namespace TQVaultAE.Models.Game
{
    public class Equipment
    {
        private Item _weaponOne;

        public Item WeaponOne
        {
            get => _weaponOne;
            set
            {
                // TODO add replacement
                //if (value.RequiredEquipmentSlots == RequiredEquipmentSlots.One)
                if (true)
                {
                    _weaponOne = value;
                    return;
                }

                _weaponOne = value;
                _shieldOne = value;
            }
        }

        private Item _weaponTwo;

        public Item WeaponTwo
        {
            get => _weaponTwo;
            set
            {
                // TODO add replacement
                //if (value.RequiredEquipmentSlots == RequiredEquipmentSlots.One)
                if (true)
                {
                    _weaponTwo = value;
                    return;
                }

                _weaponTwo = value;
                _shieldTwo = value;
            }
        }

        private Item _shieldOne;

        public Item ShieldOne
        {
            get => _shieldOne;
            set
            {
                // TODO Add replacement
                //if (value.RequiredEquipmentSlots == RequiredEquipmentSlots.One)
                if (true)
                {
                    _shieldOne = value;
                    return;
                }

                _weaponOne = value;
                _shieldOne = value;
            }
        }

        private Item _shieldTwo;

        public Item ShieldTwo
        {
            get => _shieldTwo;
            set
            {
                // TODO Add replacement
                //if (value.RequiredEquipmentSlots == RequiredEquipmentSlots.One)
                if (true)
                {
                    _shieldTwo = value;
                    return;
                }

                _weaponTwo = value;
                _shieldTwo = value;
            }
        }

        public Item Head { get; set; }

        public Item Torso { get; set; }

        public Item Arms { get; set; }

        public Item Legs { get; set; }

        public Item RingOne { get; set; }

        public Item RingTwo { get; set; }

        public Item Charm { get; set; }

        public Item Artifact { get; set; }
    }
}
