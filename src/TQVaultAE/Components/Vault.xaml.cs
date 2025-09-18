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
	public partial class Vault : UserControl, IContentScaleObserver
    {
		private const int Columns = 18;
		private const int Rows = 20;
		private ItemsPanel? _panel;

		private readonly SemaphoreSlim _uiUpdateSemaphore = new(1, 1);

        public Vault()
        {
            InitializeComponent();
			ContentScaleController.GetInstance().AddObserver(this);
        }

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			try
			{
				_uiUpdateSemaphore.Wait();
				CreateItemsPanel(args);
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

		private void CreateItemsPanel(ContentScaleUpdatedEventArgs args)
		{
			Container.Children.Remove(_panel);
			Thickness thickness = new(2, 0, 2, 2);
			_panel = new(args.General.ItemCellDimensions.Width, Columns, Rows, new Thickness(2, 0, 2, 2));

			Container.Children.Add(_panel);
			Grid.SetRow(_panel, 1);
			Grid.SetColumn(_panel, 1);

			Container.ColumnDefinitions[1].Width = new GridLength(ItemsPanel.CalculateDimensions(args.General.ItemCellDimensions.Width, Columns, Rows, thickness).Width);
			Container.RowDefinitions[0].Height = new GridLength(args.VaultTab.VaultPanel.ButtonWidthHeight);
        }

		private void Bag_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton sackButton)
			{
				foreach (FrameworkElement control in TabContainer.Children)
				{
					if (control is ToggleButton button && button != sackButton)
						button.IsChecked = false;
				}

				// TODO Invoke data load
			}
		}

		private void Bag_Unchecked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton sackButton)
			{
				foreach (FrameworkElement control in TabContainer.Children)
				{
					if (control is ToggleButton button && button.IsChecked == true)
						return;
				}

				sackButton.IsChecked = true;
				e.Handled = true;
			}
		}

		public void Dispose()
		{
			ContentScaleController.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
