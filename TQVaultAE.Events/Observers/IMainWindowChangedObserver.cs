using TQVaultAE.Events.Events;

namespace TQVaultAE.Events.Observers
{
    public interface IMainWindowChangedObserver : IEventObserver
    {
        void Notify(object sender, MainWindowChangedEvent @event);
    }
}
