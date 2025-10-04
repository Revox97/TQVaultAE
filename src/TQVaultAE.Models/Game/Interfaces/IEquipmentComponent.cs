using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game.Interfaces
{
    public interface IEquipmentComponent
    {
        ItemRarity Rarity { get; set; }
		ItemRequirements Requirements { get; set; }
        int NumberCharmSlots { get; }
        List<Item> Charms { get; }
        List<ItemAttribute> Attributes { get; }
        Affix? Prefix { get; }
        Affix? Suffix { get; }
        bool CanEquipCharm();
    }
}
