using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    internal class LegendaryEquipmentComponent : IEquipmentComponent
    {
        public ItemRarity Rarity { get; set; } = ItemRarity.Legendary;

        public ItemRequirements Requirements => throw new NotImplementedException();

        public int NumberCharmSlots => 0;

        public List<Item> Charms => [];

        public List<ItemAttribute> Attributes => throw new NotImplementedException();

        public Affix? Prefix => null;

        public Affix? Suffix => null!;

        public bool CanEquipCharm() => false;
    }
}
