using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;

namespace TQVaultAE.Services
{
    internal sealed class WindowResizeController : IWindowResizeController
    {
        private readonly IEventDispatcher _eventDispatcher = Program.Services.GetRequiredService<IEventDispatcher>();

        void IWindowResizeController.Invoke(WindowResizedEventArgs args)
        {
            MainWindowChangedEvent e = new()
            {
                Width = args.ClientSize.Width,
                Height = args.ClientSize.Height,
                CellSize = (int)(args.ClientSize.Width / 50) // TODO dummy value calculate correct result
            };

            _eventDispatcher.Dispatch(this, e);
        }
    }
}
