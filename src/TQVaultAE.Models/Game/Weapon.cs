using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    public class Weapon : EquipableItem
    {
        public RequiredEquipmentSlots RequiredEquipmentSlots { get; set; }

        public override bool HasVisualAccent
        {
            get
            {
                return Rarity >= ItemRarity.Rare; // TODO And can be worn
            }
        }
    }
}
