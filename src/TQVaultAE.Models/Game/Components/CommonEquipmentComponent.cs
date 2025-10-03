using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    public class CommonEquipmentComponent : IEquipmentComponent
    {
        public ItemRarity Rarity { get; set; } = ItemRarity.Common;

        public ItemRequirements Requirements => throw new NotImplementedException();

        // TODO some atlantis items support two
        public int NumberCharmSlots => 1;

        public List<Item> Charms => [];

        public List<ItemAttribute> Attributes => [];

        public Affix? Prefix => throw new NotImplementedException();

        public Affix? Suffix => throw new NotImplementedException();

        public bool CanEquipCharm() => true;
    }
}
