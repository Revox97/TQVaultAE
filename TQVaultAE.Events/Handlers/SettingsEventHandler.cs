using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Events.Handlers
{
    internal class SettingsEventHandler : IEventHandler
    {
        private readonly List<ISettingsEventObserver> _observers = [];

        internal SettingsEventHandler() { }

        void IEventHandler.Invoke(object sender, IEvent args)
        {
            if (args is not SettingsEvent sArgs)
                throw new ArgumentException($"Settings event must be called with {typeof(SettingsEvent)}.");

            try
            {
                foreach (ISettingsEventObserver observer in _observers)
                {
                    try
                    {
                        observer.Notify(sender, sArgs);
                    }
                    catch (Exception ex)
                    {
                        // TODO updating observer failed, add handling
                    }
                }

            }
            catch (Exception ex)
            {
                // TODO implement error handling
            }
        }

        void IEventHandler.AddObserver(IEventObserver observer)
        {
            if (observer is ISettingsEventObserver sObserver && !_observers.Contains(sObserver))
                _observers.Add(sObserver);
        }

        void IEventHandler.RemoveObserver(IEventObserver observer)
        {
            if (observer is ISettingsEventObserver sObserver)
                _observers.Remove(sObserver);
        }
    }
}
