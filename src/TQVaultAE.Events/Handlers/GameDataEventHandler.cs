using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Events.Handlers
{
    internal class GameDataEventHandler : IEventHandler
    {
        private readonly List<IGameDataObserver> _observers = [];

        public void AddObserver(IEventObserver observer)
        {
            if (observer is IGameDataObserver mObserver && !_observers.Contains(mObserver))
                _observers.Add(mObserver);
        }

        public void RemoveObserver(IEventObserver observer)
        {
            if (observer is IGameDataObserver mObserver)
                _observers.Remove(mObserver);
        }

        public void Invoke(object sender, IEvent args)
        {
            if (args is not GameDataLoadedEvent sArgs)
                throw new ArgumentException($"Game data loaded event must be called with {typeof(GameDataLoadedEvent)}.");

            try
            {
                foreach (IGameDataObserver observer in _observers)
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
