using TQVaultAE.Events.Events;
using TQVaultAE.Events.Handlers;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Events
{
    /// <summary>
    /// Represents a dispatcher, that registers / unregisters and notifies clients about TQVault <see cref="IEvent"/>s.
    /// </summary>
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, IEventHandler> _handlers = [];

        /// <summary>
        /// Initializes a new instance of <see cref="EventDispatcher"/>.
        /// </summary>
        public EventDispatcher()
        {
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            _handlers.Add(typeof(SettingsEvent), new SettingsEventHandler());
            _handlers.Add(typeof(MainWindowChangedEvent), new MainWindowChangedEventHandler());
        }

        void IEventDispatcher.AddObserver(IEventObserver observer)
        {
            foreach (IEventHandler handler in _handlers.Values)
                handler.AddObserver(observer);
        }

        void IEventDispatcher.RemoveObserver(IEventObserver observer)
        {
            foreach (IEventHandler handler in _handlers.Values)
                handler.RemoveObserver(observer);
        }

        void IEventDispatcher.Dispatch(object sender, IEvent @event)
        {
            if (!_handlers.TryGetValue(@event.GetType(), out IEventHandler? handler))
                throw new KeyNotFoundException($"Handler of type {@event.GetType().Name} does not exist.");

            try
            {
                handler.Invoke(sender, @event);
            }
            catch(Exception ex)
            {
                throw new Exception($"Invoking event {@event.GetType().Name} failed.", ex);
            }
        }
    }
}
