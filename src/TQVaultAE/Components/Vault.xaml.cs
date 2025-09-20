using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Game;

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

		private readonly List<List<Item>> _tabItems = [ [], [], [], [], [], [], [], [], [], [], [], [] ];

		private readonly SemaphoreSlim _uiUpdateSemaphore = new(1, 1);

        public Vault()
        {
            InitializeComponent();

			// TODO REMOVE
			List<Item> items = [
				new ItemBuilder().SetName("Peter Lusting").SetRarity(ItemRarity.Legendary).SetLocation(0, 0).SetSize(2, 2).SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x2.png")).Build(),
				new ItemBuilder().SetName("Hans Müller").SetRarity(ItemRarity.MonsterRare).SetLocation(2, 2).SetSize(2, 4).SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x4.png")).Build(),
				new ItemBuilder().SetName("Ich bin sogar ein Item").SetRarity(ItemRarity.Rare).SetLocation(1, 7).SetSize(2, 2).SetIcon(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/MockItem2x2.png")).Build(),
			];
			_tabItems[2] = items;

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
			
			_panel = new(LoadContent(), args.General.ItemCellDimensions.Width, Columns, Rows, new Thickness(2, 0, 2, 2));

			Container.Children.Add(_panel);
			Grid.SetRow(_panel, 1);
			Grid.SetColumn(_panel, 1);

			Container.ColumnDefinitions[1].Width = new GridLength(ItemsPanel.CalculateDimensions(args.General.ItemCellDimensions.Width, Columns, Rows, thickness).Width);
			Container.RowDefinitions[0].Height = new GridLength(args.VaultTab.VaultPanel.ButtonWidthHeight);
        }

		private List<Item> LoadContent(int id = -1)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(id, -1, nameof(id));
			ArgumentOutOfRangeException.ThrowIfGreaterThan(id, _tabItems.Count, nameof(id));

			if (id != -1)
				return _tabItems[id];

			int sackId = 0;

			foreach (FrameworkElement control in TabContainer.Children)
			{
				if (control is ToggleButton button && button.IsChecked == true)
				{
					sackId = int.Parse(button.Uid);
					break;
				}
			}

			return _tabItems[sackId];
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

				if (_panel is not null)
				{
					_panel.SwitchContent(LoadContent(int.Parse(sackButton.Uid)));
					_panel.LoadItems();
				}
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

        private void Autosort_Click(object sender, RoutedEventArgs e) => _panel?.Sort();
    }
}
