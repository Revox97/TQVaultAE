using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    public class BrokenEquipmentComponent : IEquipmentComponent
    {
        public ItemRarity Rarity { get; set; } = ItemRarity.Broken;

        public ItemRequirements Requirements { get; set; } = default;

        // TODO some Atlantis items can have two (Should not apply for broken)
        public int NumberCharmSlots => 1;

        public List<Item> Charms => [];

        public List<ItemAttribute> Attributes { get; set; } = [];

        public Affix? Prefix { get; set; }
        public Affix? Suffix { get; set; }

        public bool CanEquipCharm() => true;
    }
}
