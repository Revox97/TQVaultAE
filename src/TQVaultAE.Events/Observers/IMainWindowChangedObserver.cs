using TQVaultAE.Events.Events;

namespace TQVaultAE.Events.Observers
{
    /// <summary>
    /// Represents an observer for the <see cref="MainWindowChangedEvent"/>.
    /// </summary>
    public interface IMainWindowChangedObserver : IEventObserver
    {
        /// <summary>
        /// Notifies the <see cref="IMainWindowChangedObserver"/> about changes to the main window.
        /// </summary>
        /// <param name="sender">The sender of the <see cref="MainWindowChangedEvent"/>.</param>
        /// <param name="event">The <see cref="MainWindowChangedEvent"/>.</param>
        void Notify(object sender, MainWindowChangedEvent @event);
    }
}
