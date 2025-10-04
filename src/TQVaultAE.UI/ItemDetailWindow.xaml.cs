using System.Windows;
using TQVaultAE.Models.Game;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI
{
    /// <summary>
    /// Interaction logic for ItemDetailWindow.xaml
    /// </summary>
    public partial class ItemDetailWindow : Window
    {
        public ItemDetailWindow(Item item)
        {
            InitializeComponent();

            DataContext = new ItemDetailWindowModel(item);
        }
    }
}
