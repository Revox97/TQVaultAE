using Avalonia;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Events.Events
{
    public class ItemDragEvent(ItemDragEventType type) : IEvent
    {
        public ItemDragEventType Type { get; set; } = type;
        public Item? Item { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }
        public Point MouseOffset { get; set; }
    }

    public enum ItemDragEventType
    {
        Start,
        End,
        CursorUpdate,
    }
}
