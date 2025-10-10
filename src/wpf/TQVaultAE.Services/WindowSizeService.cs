using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Services;
using TQVaultAE.Models.Services.Observers;

namespace TQVaultAE.Services
{
    public class WindowSizeService : IWindowSizeService
    {
		private static WindowSizeService? s_instance;
		private static readonly object s_instanceLock = new();

		private readonly List<IWindowSizeObserver> _observers = [];

		public static WindowSizeService GetInstance()
		{
			if (s_instance is null)
			{
				lock (s_instanceLock)
					s_instance ??= new WindowSizeService();
			}

			return s_instance;
		}

		public void AddObserver(IWindowSizeObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));
			_observers.Add(observer);
		}

		public void RemoveObserver(IWindowSizeObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));
			_observers.Remove(observer);
		}

		public void Notify(object sender, WindowSizeUpdatedEventArgs args)
		{
			_observers.ForEach(o => {
				try
				{
					o.Notify(sender, args);
				}
				catch(Exception ex)
				{
					// TODO log exception
				}
			});
		}
    }
}
