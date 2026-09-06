using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Events.Handlers
{
    internal interface IEventHandler
    {
        void Invoke(object sender, IEvent args);
        void AddObserver(IEventObserver observer);
        void RemoveObserver(IEventObserver observer);
    }
}
