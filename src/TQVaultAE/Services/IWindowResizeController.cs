using Avalonia.Controls;

namespace TQVaultAE.Services
{
    internal interface IWindowResizeController
    {
        void Invoke(WindowResizedEventArgs args);
    }
}
