using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    public class GearItem : EquipableItem
    {
        public int Armor { get; set; } = -1;

        public GearType GearType { get; set; }

        public override bool HasVisualAccent
        {
            get
            {
                return Rarity >= ItemRarity.Rare; // TODO And can be worn
            }
        }
    }
}
