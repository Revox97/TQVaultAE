using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Events.Handlers
{
    internal class MainWindowChangedEventHandler : IEventHandler
    {
        private readonly List<IMainWindowChangedObserver> _observers = [];

        internal MainWindowChangedEventHandler() { }

        void IEventHandler.AddObserver(IEventObserver observer)
        {
            if (observer is IMainWindowChangedObserver mObserver && !_observers.Contains(mObserver))
                _observers.Add(mObserver);
        }

        void IEventHandler.RemoveObserver(IEventObserver observer)
        {
            if (observer is IMainWindowChangedObserver mObserver)
                _observers.Remove(mObserver);
        }

        void IEventHandler.Invoke(object sender, IEvent args)
        {
            if (args is not MainWindowChangedEvent sArgs)
                throw new ArgumentException($"Main window changed event must be called with {typeof(MainWindowChangedEvent)}.");

            try
            {
                foreach (IMainWindowChangedObserver observer in _observers)
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
    }
}
