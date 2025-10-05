using System.Windows;
using TQVaultAE.Models.Game;
using TQVaultAE.UI.Controllers;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI
{
    /// <summary>
    /// Interaction logic for ItemDragWindow.xaml
    /// </summary>
    public partial class ItemDragWindow : Window
    {
        private readonly DragController _dragController;
        private readonly ItemDragWindowModel _model;

        public ItemDragWindow(Item item)
        {
            InitializeComponent();
            _dragController = DragController.GetInstance();
            _model = new ItemDragWindowModel(item);
            _dragController.SetItem(item);
        }
    }
}
