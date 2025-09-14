using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
		private const int PanelBorderThickness = 2;
		private ItemsPanel? _panel;
		private readonly CellWidthController _cellWidthController = CellWidthController.GetInstance();

		private readonly SemaphoreSlim _uiUpdateSemaphore = new(1, 1);

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

				_panel = new(cellWidthHeight, Columns, Rows, PanelBorderThickness);

				Container.Children.Add(_panel);
				Grid.SetRow(_panel, 1);
				Grid.SetColumn(_panel, 1);

				Container.ColumnDefinitions[1].Width = new GridLength((cellWidthHeight * Columns) + (PanelBorderThickness * 2));

				// Prevent tabs from growing / shrinking in a weird way
				double headerCellWidthHeight = Container.ColumnDefinitions[1].ActualWidth / 12f;
				Container.RowDefinitions[0].Height = new GridLength(headerCellWidthHeight);
				_cellWidthController.Notify(this, new CellWidthChangedEventArgs(cellWidthHeight, headerCellWidthHeight));
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
			double height = Container.ActualHeight - Container.RowDefinitions[0].ActualHeight;
			double width = Container.ActualWidth - Container.ColumnDefinitions[0].ActualWidth;

			double targetedCellHeight = (height - PanelBorderThickness) / Rows;

			if (targetedCellHeight * Columns <= width - (PanelBorderThickness * 2))
				return targetedCellHeight;

			return (width - (PanelBorderThickness * 2)) / Columns;
		}

		public void Notify(object sender, WindowSizeUpdatedEventArgs e) => CreateItemsPanel();

		private void Bag_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton sackButton)
			{
				if (sackButton.IsChecked == false)
				{
					sackButton.IsChecked = true;
					e.Handled = true;
					return;
				}

				foreach (FrameworkElement control in TabContainer.Children)
				{
					if (control is ToggleButton button && button != sackButton)
						button.IsChecked = false;
				}

				// TODO Invoke data load
			}
		}

		public void Dispose()
		{
			WindowSizeUpdater.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
