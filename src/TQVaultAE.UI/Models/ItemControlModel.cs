using TQVaultAE.Models.PlayerData;

namespace TQVaultAE.UI.Models
{
    public class ItemControlModel(Item item)
    {
        public Item Item { get; set; } = item;
    }
}
