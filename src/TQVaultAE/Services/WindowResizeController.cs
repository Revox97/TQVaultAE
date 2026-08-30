using System.Collections.Generic;
using System.Threading;
using Avalonia.Controls;
using TQVaultAE.Observers;
using TQVaultAE.Observers.EventArgs;

namespace TQVaultAE.Services
{
    internal class WindowResizeController
    {
        private static WindowResizeController? s_instance;
        private static readonly SemaphoreSlim s_instanceSemaphore = new(1, 1);

        private readonly List<IWindowResizeObserver> _observers = [];

        internal static WindowResizeController GetInstance()
        {
            if (s_instance is null)
            {
                try
                {
                    s_instanceSemaphore.Wait(2000);
                    s_instance ??= new WindowResizeController();
                }
                finally
                {
                    s_instanceSemaphore.Release();
                }
            }

            return s_instance;
        }

        private WindowResizeController() { }

        internal void AddObserver(IWindowResizeObserver observer)
        {
            if (_observers.Contains(observer))
                return;

            _observers.Add(observer);
        }

        internal void RemoveObserver(IWindowResizeObserver observer)
        {
            if (!_observers.Contains(observer))
                return;

            _observers.Remove(observer);
        }

        internal void Update(WindowResizedEventArgs args)
        {
            WindowSizeChangedEventArgs e = new()
            {
                Size = args.ClientSize,
                CellSize = (int)(args.ClientSize.Width / 50) // TODO dummy value calculate correct result
            };

            _observers.ForEach(x =>
            {
                try
                {
                    x.Update(e);
                }
                catch
                {
                    // TODO: Handle exception
                }
            });
        }
    }
}
