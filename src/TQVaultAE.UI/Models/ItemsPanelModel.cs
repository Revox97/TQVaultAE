using TQVaultAE.Models.PlayerData;
using TQVaultAE.Models.UI;

namespace TQVaultAE.UI.Models
{
    internal class ItemsPanelModel(List<Item> items)
    {
        public SortDirection SortDirection { get; set; }

        public List<Item> Items { get; set; } = items;
    }
}
