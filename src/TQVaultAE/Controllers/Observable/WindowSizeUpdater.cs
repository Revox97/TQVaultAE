using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
    internal class WindowSizeUpdater
    {
		internal static WindowSizeUpdater? s_instance;
		internal static object s_instanceLock = new();

		private readonly List<IWindowSizeObserver> _observers = [];

		internal static WindowSizeUpdater GetInstance()
		{
			if (s_instance is null)
			{
				lock (s_instanceLock)
					s_instance ??= new WindowSizeUpdater();
			}

			return s_instance;
		}

		internal void AddObserver(IWindowSizeObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));
			_observers.Add(observer);
		}

		internal void RemoveObserver(IWindowSizeObserver observer)
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
