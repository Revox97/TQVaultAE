using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    internal class LegendaryEquipmentComponent : IEquipmentComponent
    {
        public ItemRarity Rarity { get; set; } = ItemRarity.Legendary;

        public ItemRequirements Requirements { get; set; } = default;

        public int NumberCharmSlots => 0;

        public List<Item> Charms => [];

        public List<ItemAttribute> Attributes { get; set; } = [];

        public Affix? Prefix { get; set; }
        public Affix? Suffix { get; set; }

        public bool CanEquipCharm() => false;
    }
}
