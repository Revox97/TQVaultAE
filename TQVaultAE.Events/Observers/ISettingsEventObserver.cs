using TQVaultAE.Events.Events;

namespace TQVaultAE.Events.Observers
{
    public interface ISettingsEventObserver : IEventObserver
    {
        void Notify(object sender, SettingsEvent @event);
    }
}
