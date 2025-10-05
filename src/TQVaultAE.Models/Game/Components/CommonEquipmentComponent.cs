using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    public class CommonEquipmentComponent : IEquipmentComponent
    {
        public ItemRarity Rarity { get; set; } = ItemRarity.Common;

        public ItemRequirements Requirements { get; set; } = default;

        // TODO some atlantis items support two
        public int NumberCharmSlots => 1;

        public List<Item> Charms => [];

        public List<ItemAttribute> Attributes { get; set; } = [];

        public Affix? Prefix { get; set; }
        public Affix? Suffix { get; set; }

        public bool CanEquipCharm() => true;
    }
}
