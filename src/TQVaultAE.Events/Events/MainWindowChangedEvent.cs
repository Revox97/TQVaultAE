using System.Drawing;

namespace TQVaultAE.Events.Events
{
    /// <summary>
    /// Represents an <see cref="IEvent"/> for changes in the main window.
    /// </summary>
    public class MainWindowChangedEvent : IEvent
    {
        /// <summary>
        /// Gets the new location of the main window.
        /// </summary>
        public Point Location { get; init; }

        /// <summary>
        /// Gets the new width of the main window.
        /// </summary>
        public double Width { get; init; }

        /// <summary>
        /// Gets the new height of the main window.
        /// </summary>
        public double Height { get; init; }

        /// <summary>
        /// Gets the new cell size for tq item controls.
        /// </summary>
        public int CellSize { get; init; }
    }
}
