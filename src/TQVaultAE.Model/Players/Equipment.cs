using TQVaultAE.Model.Items;

namespace TQVaultAE.Model.Players
{
    /// <summary>
    /// Represents the equipment of a <see cref="Player"/>.
    /// </summary>
    public class Equipment
    {
        public Item? Head { get; set; }
        public ArmorItem? Arms { get; set; }
        public ArmorItem? Legs { get; set; }
        public ArmorItem? Body { get; set; }
        public WeaponItem? PrimaryWeaponSetOne { get; set; }
        public WeaponItem? PrimaryWeaponSetTwo { get; set; }
        public WeaponItem? SecundaryWeaponSetOne { get; set; }
        public WeaponItem? SecundaryWeaponSetTwo { get; set; }
        public JewelryItem? Amulet { get; set; }
        public JewelryItem? RingOne { get; set; }
        public JewelryItem? RingTwo { get; set; }
        public ArtifactItem? Artifact { get; set; }
    }
}
