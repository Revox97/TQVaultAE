using TQVaultAE.Models.Game;

namespace TQVaultAE.Models
{
    public class EquipmentModel
    {
        public Item? WeaponOne { get; set; }
        public Item? WeaponTwo { get; set; }
        public Item? ShieldOne { get; set; }
        public Item? ShieldTwo { get; set; }
        public Item? Head { get; set; }
        public Item? Necklace { get; set; }
        public Item? Artifact { get; set; }
        public Item? Torso { get; set; }
        public Item? Arms { get; set; }
        public Item? Legs { get; set; }
        public Item? RingOne { get; set; }

        // FUUUUU WE ARE MISSING A COLUMN HERE
        public Item? RingTwo { get; set; }
    }
}
