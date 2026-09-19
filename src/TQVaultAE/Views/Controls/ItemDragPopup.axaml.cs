using Avalonia.Controls;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Views.Controls
{
    public partial class ItemDragPopup : UserControl
    {
        public Item Item { get; set; }

        // Design time
        public ItemDragPopup()
        {
            InitializeComponent();
            Item = new();
        }

        public ItemDragPopup(Item item)
        {
            InitializeComponent();

            Item = item;
            DataContext = Item;
        }
    }
}