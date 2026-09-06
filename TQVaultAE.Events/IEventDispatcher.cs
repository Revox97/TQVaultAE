using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Events
{
    public interface IEventDispatcher
    {
        void AddObserver(IEventObserver observer);
        void RemoveObserver(IEventObserver observer);
        void Dispatch(object sender, IEvent args);
    }
}
