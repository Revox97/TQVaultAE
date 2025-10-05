using System.Diagnostics;
using System.Windows;
using TQVaultAE.Models.Game;

namespace TQVaultAE.UI.Controllers
{
    public delegate void ItemDraggedChangedEventHandler(object source, ItemDraggedEventArgs args);

    public class ItemDraggedEventArgs(bool newValue) : EventArgs
    {
        public bool IsItemDragged { get; } = newValue;
    }

    public delegate void ItemMovedEventHandler(object source, ItemMovedEventArgs args);

    public class ItemMovedEventArgs(Item item, Point currentLocation) : EventArgs
    {
        public Item Item { get; } = item;
        public Point CurrentLocation { get; } = currentLocation;
    }

    public delegate void MouseMovedEventHandler(object source, MouseMovedEventArgs args);

    public class MouseMovedEventArgs(Point position) : EventArgs
    {
        public Point Position { get; } = position;
    }

    internal class DragController
    {
        private static DragController? s_instance;
        private static readonly object s_instanceLock = new();

        public event ItemDraggedChangedEventHandler ItemDraggedChanged;
        public event ItemMovedEventHandler ItemMoved;
        public event MouseMovedEventHandler MouseMoved;

        private Item? _currentItem = null;

        internal static DragController GetInstance()
        {
            if (s_instance is null)
            {
                lock (s_instanceLock)
                    s_instance ??= new DragController();
            }

            return s_instance;
        }

        private Point _mousePosition;

        public void UpdateMousePosition(Point position)
        {
            _mousePosition = position;
            MouseMoved?.Invoke(this, new MouseMovedEventArgs(position));
        }

        public void SetItem(Item item)
        {
            _currentItem = item;
            IsItemDragged = true;
        }

        public void UnsetItem()
        {
            _currentItem = null;
            IsItemDragged = false;
        }

        // TODO used by drag window to notify the controller
        public void MoveItem(Point newLocation)
        {
            if (_currentItem is null)
                return;

            ItemMoved?.Invoke(this, new ItemMovedEventArgs(_currentItem, newLocation));
        }

        private DragController() { }

        private bool _isItemDragged = false;
        public bool IsItemDragged
        {
            get => _isItemDragged;
            set
            {
                if (_isItemDragged != value)
                {
                    _isItemDragged = value;
                    ItemDraggedChanged.Invoke(this, new(value));
                }
            }
        }
    }
}
