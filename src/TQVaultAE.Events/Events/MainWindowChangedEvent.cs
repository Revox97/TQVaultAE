using System.Drawing;

namespace TQVaultAE.Events.Events
{
    public class MainWindowChangedEvent : IEvent
    {
        /// <summary>
        /// Gets the new (Avalonia) location of the window.
        /// </summary>
        public Point Location { get; init; }

        /// <summary>
        /// Gets the new (Avalonia) size of the window.
        /// </summary>
        public double Width { get; init; }
        public double Height { get; init; }

        /// <summary>
        /// Gets the new cell size for tq item controls.
        /// </summary>
        public int CellSize { get; init; }
    }
}
