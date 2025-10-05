using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using TQVaultAE.UI.Components;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Controllers
{
    internal class ItemControlController
    {
        private readonly ItemControl _instance;
        private readonly ItemControlModel _model;

        private readonly DragController _dragController;

        private ItemDetailWindow? _detailsWindow = null;
        private Popup? _itemDragPopup;

        public ItemControlController(ItemControl instance, ItemControlModel model)
        {
            _instance = instance;
            _model = model;

            _dragController = DragController.GetInstance();
            _dragController.MouseMoved += MouseMoved;
        }

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

        private Vector _popupOffsetFromMouse;

        internal void ShowItemDragWindow()
        {
            Point mouseScreen = GetMouseScreenPosition();
            Vector popupSize = new(_instance.ActualWidth, _instance.ActualHeight);

            _popupOffsetFromMouse = new Vector(
                -popupSize.X / 2,
                -popupSize.Y / 2
            );

            _itemDragPopup = new Popup
            {
                Placement = PlacementMode.Absolute,
                AllowsTransparency = true,
                StaysOpen = true,
                IsHitTestVisible = true,
                Width = _instance.ActualWidth,
                Height = _instance.ActualHeight,
                Child = new Border()
                {
                    Background = new SolidColorBrush(Colors.Transparent),
                    Padding = new Thickness(5),
                    Child = new Image()
                    {
                        Source = _model.Item.Icon
                    }
                },
                HorizontalOffset = mouseScreen.X + _popupOffsetFromMouse.X,
                VerticalOffset = mouseScreen.Y + _popupOffsetFromMouse.Y
            };

            _instance.Container.Children.Add(_itemDragPopup);
            _itemDragPopup.IsOpen = true;
            
            _dragController.SetItem(_model.Item);
        }

        private void MouseMoved(object sender, MouseMovedEventArgs e)
        {
            if (_itemDragPopup is null)
                return;

            Point mouseScreen = GetMouseScreenPosition();

            _itemDragPopup.HorizontalOffset = mouseScreen.X + _popupOffsetFromMouse.X;
            _itemDragPopup.VerticalOffset = mouseScreen.Y + _popupOffsetFromMouse.Y;
        }

        private static Point GetMouseScreenPosition()
        {
            Point mousePos = Mouse.GetPosition(Application.Current.MainWindow);
            return Application.Current.MainWindow.PointToScreen(mousePos);
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
