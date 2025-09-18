using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
	internal class ItemOverController
	{
		private static ItemOverController? s_instance;
		private static readonly object s_instanceLock = new();

		private readonly List<IItemOverObserver> _observers = [];

		public static ItemOverController GetInstance()
		{
			if (s_instance is null)
			{
				lock (s_instanceLock)
					s_instance ??= new ItemOverController();
			}

			return s_instance;
		}

		private ItemOverController() { }

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
