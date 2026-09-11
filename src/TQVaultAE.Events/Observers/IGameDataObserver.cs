using TQVaultAE.Events.Events;

namespace TQVaultAE.Events.Observers
{
    /// <summary>
    /// Represents an observer for the <see cref="GameDataLoadedEvent"/>.
    /// </summary>
    public interface IGameDataObserver : IEventObserver
    {
        /// <summary>
        /// Notifies the <see cref="IGameDataObserver"/> about changes to the game data.
        /// </summary>
        /// <param name="sender">The sender of the <see cref="GameDataLoadedEvent"/>.</param>
        /// <param name="event">The <see cref="GameDataLoadedEvent"/>.</param>
        void Notify(object sender, GameDataLoadedEvent @event);
    }
}
