using TQVaultAE.Models.Game;

namespace TQVaultAE.Models
{
    public class ItemControlModel(Item item)
    {
        public Item Item { get; set; } = item;
    }
}
