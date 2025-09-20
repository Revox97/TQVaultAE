using TQVaultAE.Models.Game;

namespace TQVaultAE.Models
{
    internal class ItemsPanelModel(List<Item> items)
    {
        public UI.SortDirection SortDirection { get; set; }

        public List<Item> Items { get; set; } = items;
    }
}
