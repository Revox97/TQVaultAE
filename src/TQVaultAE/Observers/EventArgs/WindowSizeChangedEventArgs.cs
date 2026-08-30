using Avalonia;

namespace TQVaultAE.Observers.EventArgs
{
    public class WindowSizeChangedEventArgs
    {
        /// <summary>
        /// Gets the new (Avalonia) location of the window.
        /// </summary>
        public Point Location { get; init; }

        /// <summary>
        /// Gets the new (Avalonia) size of the window.
        /// </summary>
        public Size Size { get; init; }

        /// <summary>
        /// Gets the new cell size for tq item controls.
        /// </summary>
        public int CellSize { get; init; }
    }
}
