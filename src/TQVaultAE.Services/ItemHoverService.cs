using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Services;
using TQVaultAE.Models.Services.Observers;

namespace TQVaultAE.Services
{
	public class ItemHoverService : IItemHoverService
	{
		private static ItemHoverService? s_instance;
		private static readonly object s_instanceLock = new();

		private readonly List<IItemOverObserver> _observers = [];

		public static ItemHoverService GetInstance()
		{
			if (s_instance is null)
			{
				lock (s_instanceLock)
					s_instance ??= new ItemHoverService();
			}

			return s_instance;
		}

		private ItemHoverService() { }

		public void AddObserver(IItemOverObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));

			if (_observers.Any(o => o == observer))
				return;

			_observers.Add(observer);
		}

		public void RemoveObserver(IItemOverObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));
			_observers.Remove(observer);
		}

		public void Notify(object sender, ItemOverEventArgs args)
		{
			try
			{
				_observers.ForEach(o =>
				{
					try
					{
						o.Notify(sender, args);
					}
					catch (Exception ex)
					{
						// TODO Log exception
					}
				});
			}
			catch(Exception ex)
			{
				// TODO Log exception
			}
		}
	}
}
