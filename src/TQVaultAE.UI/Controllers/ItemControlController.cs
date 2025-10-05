using System.Windows;
using System.Windows.Input;
using TQVaultAE.UI.Components;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Controllers
{
    internal class ItemControlController(ItemControl instance, ItemControlModel model)
    {
        private readonly ItemControl _instance = instance;
        private readonly ItemControlModel _model = model;

        private ItemDetailWindow? _detailsWindow = null;

        internal void ShowItemDetailsWindow()
        {
            Point openingPosition = GetDetailsWindowPosition();

            _detailsWindow = new ItemDetailWindow(_model.Item)
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = openingPosition.X,
                Top = openingPosition.Y,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            _detailsWindow.Show();
        }

        internal void CloseItemDetailsWindow() => _detailsWindow?.Close();

        internal void ShowItemDragWindow()
        {
            Point openingLocation = GetItemControlPosition();
            
            new ItemDragWindow(_model.Item)
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = openingLocation.X,
                Top = openingLocation.Y,
                Width = _instance.ActualWidth,
                Height = _instance.ActualHeight,
            }.Show();
        }

        private Point GetDetailsWindowPosition()
        {
            Point itemControlPosition = GetItemControlPosition();
            itemControlPosition.X += _instance.ActualWidth + 10;
            itemControlPosition.Y -= 5;

            return itemControlPosition;
        }

        private Point GetItemControlPosition()
        {
            return _instance.PointToScreen(new Point(0, 0));
        }
    }
}
