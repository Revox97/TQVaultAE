using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
	// Delete once replaced
	// TODO calculate with in here and forward events. Pull out calculation from UI
	internal class CellWidthController
	{
		private static CellWidthController? s_instance;
		private static readonly object s_instanceLock = new();
		private readonly List<ICellWidthObserver> _observers = [];

		public static CellWidthController GetInstance()
		{
			if (s_instance is null)
			{
				lock(s_instanceLock)
					s_instance ??= new CellWidthController();
			}

			return s_instance;
		}

		private CellWidthController() { }

		public void AddObserver(ICellWidthObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));
			_observers.Add(observer);
		}

		public void RemoveObserver(ICellWidthObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));
			_observers.Remove(observer);
		}

		public void Notify(object sender, CellWidthChangedEventArgs args)
		{
			_observers.ForEach(o =>
			{
				try
				{
					o.Notify(sender, args);
				}
				catch (Exception ex)
				{
					// TODO Add logging
				}
			});
		}
	}
}
