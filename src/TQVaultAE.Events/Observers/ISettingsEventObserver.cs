using TQVaultAE.Events.Events;

namespace TQVaultAE.Events.Observers
{
    /// <summary>
    /// Represents an observer for the <see cref="SettingsEvent"/>.
    /// </summary>
    public interface ISettingsEventObserver : IEventObserver
    {
        /// <summary>
        /// Notifies the <see cref="ISettingsEventObserver"/> about the <see cref="SettingsEvent"/>.
        /// </summary>
        /// <param name="sender">The sender of the <see cref="SettingsEvent"/>.</param>
        /// <param name="event">The <see cref="SettingsEvent"/>.</param>
        void Notify(object sender, SettingsEvent @event);
    }
}
