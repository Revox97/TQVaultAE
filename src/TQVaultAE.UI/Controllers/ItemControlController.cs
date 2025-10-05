using System.Windows;
using System.Windows.Controls;
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
            Point openingPosition = GetWindowPosition();

            _detailsWindow = new ItemDetailWindow(_model.Item)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.Manual,
                Left = openingPosition.X,
                Top = openingPosition.Y,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            _detailsWindow.Show();
        }

        internal void CloseItemDetailsWindow() => _detailsWindow?.Close();

        private Point GetWindowPosition()
        {
            Point screenPoint = _instance.PointToScreen(new Point(0, 0));
            screenPoint.X += _instance.ActualWidth + 10;
            screenPoint.Y -= 5;

            return screenPoint;
        }
    }
}
