using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Events.Handlers
{
    public class ItemDragEventHandler : IEventHandler
    {
        private bool _isDraggingActive = false;

        private readonly List<IItemDragEventObserver> _observers = [];

        public void AddObserver(IEventObserver observer)
        {
            if (observer is not IItemDragEventObserver dObserver || _observers.Contains(dObserver))
                return;

            _observers.Add(dObserver);
        }

        public void Invoke(object sender, IEvent args)
        {
            if (args is not ItemDragEvent @event)
                return;

            if (@event.Type == ItemDragEventType.Start && _isDraggingActive)
                return;

            if (@event.Type is ItemDragEventType.End or ItemDragEventType.CursorUpdate && !_isDraggingActive)
                return;

            _isDraggingActive = @event.Type is ItemDragEventType.Start or ItemDragEventType.CursorUpdate;

            foreach(IItemDragEventObserver observer in _observers)
            {
                try
                {
                    observer.Notify(sender, @event);
                }
                catch(Exception ex)
                {
                    // TODO Handle exception
                }
            }
        }

        public void RemoveObserver(IEventObserver observer)
        {
            if (observer is not IItemDragEventObserver dObserver)
                return;

            _observers.Remove(dObserver);
        }
    }
}
