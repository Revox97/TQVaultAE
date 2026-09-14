using TQVaultAE.Model.Items;

namespace TQVaultAE.Model.Stashes
{
    public class ItemStash
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public int ItemCount { get; set; }

        public List<Item> Items { get; set; } = [];
    }
}
