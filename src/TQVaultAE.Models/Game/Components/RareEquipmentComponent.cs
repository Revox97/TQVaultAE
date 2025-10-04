using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    public class RareEquipmentComponent : IEquipmentComponent
    {
        public ItemRarity Rarity { get; set; } = ItemRarity.Rare;

        public ItemRequirements Requirements { get; set; } = default;

        // TODO default one, some atlantis items support two slots tough
        public int NumberCharmSlots => 1;

        public List<Item> Charms => [];

        public List<ItemAttribute> Attributes => [];

        public Affix? Prefix { get; set; }
        public Affix? Suffix { get; set; }

        public bool CanEquipCharm() => true;
    }
}
