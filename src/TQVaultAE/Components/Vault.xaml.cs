using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Components
{
	/// <summary>
	/// Interaction logic for Vault.xaml
	/// </summary>
	public partial class Vault : UserControl, IWindowSizeObserver
    {
		private const int Columns = 18;
		private const int Rows = 20;
		private const int BorderWidth = 2;
		private ItemsPanel? _panel;

		private SemaphoreSlim _uiUpdateSemaphore = new(1, 1);

        public Vault()
        {
            InitializeComponent();
        }

		private void Container_Loaded(object sender, RoutedEventArgs e)
		{
			CreateItemsPanel();
			WindowSizeUpdater.GetInstance().AddObserver(this);
		}

		private void CreateItemsPanel()
		{
			_uiUpdateSemaphore.Wait();

			try
			{
				Container.Children.Remove(_panel);

				double cellWidthHeight = CalculateCellWithHeight();
				_panel = new(cellWidthHeight, Columns, Rows);

				Container.Children.Add(_panel);
				Grid.SetRow(_panel, 1);
				Grid.SetColumn(_panel, 1);

				// Update container width to match cells
				Container.ColumnDefinitions[1].Width = new GridLength(cellWidthHeight * Columns + 4, GridUnitType.Pixel);

				// Prevent tabs from growing / shrinking in a weird way
				Container.RowDefinitions[0].Height = new GridLength(Container.ColumnDefinitions[1].ActualWidth / 12f);
			}
			catch (Exception ex)
			{
				// TODO log exception
			}
			finally
			{
				_uiUpdateSemaphore.Release();
			}
        }

		private double CalculateCellWithHeight()
		{
			// TODO figure out how to solve scaling bug (items panel is larger then container
			double height = Container.ActualHeight - Container.RowDefinitions[0].ActualHeight;
			double width = Container.ActualWidth - Container.ColumnDefinitions[0].ActualWidth;

			double targetedCellHeight = (height - (BorderWidth * 2)) / Rows;

			double roundingBuffer = 40;
			if (targetedCellHeight * Columns <= width - (BorderWidth * 2) - roundingBuffer)
				return targetedCellHeight;

			return (Container.ColumnDefinitions[1].ActualWidth - (BorderWidth * 2)) / Columns;
		}

		void IWindowSizeObserver.Notify(object sender, WindowSizeUpdatedEventArgs e)
		{
			CreateItemsPanel();
		}

		public void Dispose()
		{
			WindowSizeUpdater.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
