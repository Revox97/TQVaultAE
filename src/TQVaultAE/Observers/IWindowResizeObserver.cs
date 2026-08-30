using System;
using TQVaultAE.Observers.EventArgs;

namespace TQVaultAE.Observers
{
    internal interface IWindowResizeObserver : IDisposable
    {
        void Update(WindowSizeChangedEventArgs args);
    }
}
