using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Events
{
    /// <summary>
    /// Represents a dispatcher, that registers / unregisters and notifies clients about TQVault <see cref="IEvent"/>s.
    /// </summary>
    public interface IEventDispatcher
    {
        /// <summary>
        /// Adds a new <see cref="IEventObserver"/> for all <see cref="IEvent"/> types, that the client implements.
        /// </summary>
        /// <param name="observer">The <see cref="IEventObserver"/>.</param>
        void AddObserver(IEventObserver observer);

        /// <summary>
        /// Removes an <see cref="IEventObserver"/> from all events, that it is registered for.
        /// </summary>
        /// <param name="observer">The <see cref="IEventObserver"/>.</param>
        void RemoveObserver(IEventObserver observer);

        /// <summary>
        /// Sends an <see cref="IEvent"/> to all <see cref="IEventObserver"/>s, that are registered for the event type.
        /// </summary>
        /// <param name="sender">The sender of the <paramref name="event"/>.</param>
        /// <param name="event">The <see cref="IEvent"/>, that is sent to the <see cref="IEventObserver"/>s.</param>
        /// <exception cref="KeyNotFoundException">Thrown, if the <see cref="IEvent"/> is not a registered event type.</exception>
        /// <exception cref="Exception"></exception>
        void Dispatch(object sender, IEvent args);
    }
}
