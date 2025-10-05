using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.Builders;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Game;
using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Services;
using TQVaultAE.Models.Services.Observers;
using TQVaultAE.UI.Resources;

namespace TQVaultAE.UI.Components
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
				new ItemBuilder(ItemCategory.Gear)
                    .AddName("Peter Lustig")
                    .AddDescription("I am a funny item")
                    .AddEquipmentComponent(ItemRarity.Legendary)
                    .AddRequirements(-1, 230, -1, -1)
                    .AddItemVersion(ItemVersion.Original)
                    .AddAttributes([ new ItemAttribute() { Value = "15 Vitality Damage" }, new ItemAttribute() { Value = "3.0% Chance of 50 % Reduction to Enemy's Health" }])
                    .AddLocation(0, 0)
                    .AddSize(2, 2)
                    .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                    .Build(),
				new ItemBuilder(ItemCategory.Gear)
                    .AddName("Hans Müller")
                    .AddDescription("I am a Hans Müller")
                    .AddEquipmentComponent(ItemRarity.Rare)
                    .AddRequirements(29, -1, 40, 250)
                    .AddItemVersion(ItemVersion.ImmortalThrone)
                    .AddLocation(2, 2)
                    .AddSize(2, 4)
                    .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByFour)
                    .Build(),
				new ItemBuilder(ItemCategory.Gear)
                    .AddName("RealItem")
                    .AddDescription("Ich bin sogar ein Item")
                    .AddEquipmentComponent(ItemRarity.Common)
                    .AddRequirements(1, 10, 10, 10)
                    .AddItemVersion(ItemVersion.EternalEmbers)
                    .AddLocation(1, 7)
                    .AddSize(2, 2)
                    .AddIcon((BitmapImage)ImagePaths.ItemMocks.TwoByTwo)
                    .Build(),
                // TODO Charms can also have requirements! -> Maybe a seperate component is required
				new ItemBuilder(ItemCategory.Charm)
                    .AddName("Legendary Charmboy")
                    .AddDescription("Ich bin ein Charm")
                    .AddItemVersion(ItemVersion.EternalEmbers)
                    .AddLocation(5, 9)
                    .AddStacking()
                    .AddIcon((BitmapImage)ImagePaths.ItemMocks.OneByOne)
                    .Build(),
			];

			_tabItems[2] = items;

			ContentScaleService.GetInstance().AddObserver(this);
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
			ContentScaleService.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}

        private void Autosort_Click(object sender, RoutedEventArgs e) => _panel?.Sort();
    }
}
