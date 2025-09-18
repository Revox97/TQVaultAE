using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
	internal class ContentScaleController : IWindowSizeObserver
	{
		private static ContentScaleController? s_instance;
		private static readonly object s_instanceLock = new();

		private readonly WindowSizeUpdater _windowSizeUpdater;
		private readonly List<IContentScaleObserver> _observers = [];

		internal static ContentScaleController GetInstance()
		{
			if (s_instance is null)
			{
				lock (s_instanceLock)
				{
					s_instance ??= new ContentScaleController();
				}
			}

			return s_instance;
		}

		private ContentScaleController() 
		{ 
			_windowSizeUpdater = WindowSizeUpdater.GetInstance();
		}

		internal void AddObserver(IContentScaleObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));

			if (!_observers.Contains(observer))
				_observers.Add(observer);
		}

		internal void RemoveObserver(IContentScaleObserver observer)
		{
			ArgumentNullException.ThrowIfNull(observer, nameof(observer));
			_observers.Remove(observer);
		}

		public void Notify(object sender, WindowSizeUpdatedEventArgs e)
		{
			try
			{
				ContentScaleUpdatedEventArgs arguments = CalculateControlDimensions(e);

				_observers.ForEach(o =>
				{
					try
					{
						o.Notify(this, arguments);
					}
					catch (Exception ex)
					{
						// TODO log exception
					}
				});
			}
			catch (Exception ex) 
			{ 
				// TODO log exception
			}
		}

		public void Dispose()
		{
			_windowSizeUpdater.RemoveObserver(this);
			GC.SuppressFinalize(this);
		}

		private ContentScaleUpdatedEventArgs CalculateControlDimensions(WindowSizeUpdatedEventArgs args)
		{
			throw new NotImplementedException();
			return new ContentScaleUpdatedEventArgs();
		}
	}
}
