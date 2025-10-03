using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    public class BrokenEquipmentComponent : IEquipmentComponent
    {
        public ItemRarity Rarity { get; set; } = ItemRarity.Broken;

        public ItemRequirements Requirements => throw new NotImplementedException();

        // TODO some Atlantis items can have two (Should not apply for broken)
        public int NumberCharmSlots => 1;

        public List<Item> Charms => [];

        public List<ItemAttribute> Attributes => [];

        public Affix? Prefix => null;

        public Affix? Suffix => null;

        public bool CanEquipCharm() => true;
    }
}
