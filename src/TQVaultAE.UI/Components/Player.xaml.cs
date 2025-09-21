using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Game;
using TQVaultAE.Models.Services;
using TQVaultAE.Models.Services.Observers;
using TQVaultAE.UI.Resources;

namespace TQVaultAE.UI.Components
{
	/// <summary>
	/// Interaction logic for Player.xaml
	/// </summary>
	public partial class Player : UserControl, IContentScaleObserver
    {
		private const int MainSackColumns = 12;
		private const int SackRows = 5;
		private const int AdditionalSackColumns = 8;

		private ItemsPanel? _mainSackPanel;
		private ItemsPanel? _additionalSackPanel;
		private readonly List<Item> _mainSackItems = [];
		private readonly List<List<Item>> _additionalSackItems = [ [], [], [] ];

		private readonly SemaphoreSlim _uiUpdateSemaphore = new(1, 1);

        public Player()
        {
            InitializeComponent();

			// TODO REMOVE
			List<Item> items = [
				new ItemBuilder().SetLocation(0, 0)
                                 .SetSize(2, 2)
                                 .SetIcon((BitmapImage)ImagePaths.Buttons.InventoryBag.Up)
                                 .Build(),
				new ItemBuilder().SetLocation(2, 2)
                                 .SetSize(2, 2)
                                 .SetIcon((BitmapImage)ImagePaths.Buttons.InventoryBag.Up)
                                 .Build(),
				new ItemBuilder().SetLocation(4, 0)
                                 .SetSize(2, 2)
                                 .SetIcon((BitmapImage)ImagePaths.Buttons.InventoryBag.Up)
                                 .Build(),
			];
			_mainSackItems = items;
			_additionalSackItems[1] = items;

			ContentScaleService.GetInstance().AddObserver(this);
        }

		private void PlayerSack_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton sackButton)
			{
				foreach (FrameworkElement control in AdditionalSackTabs.Children)
				{
					if (control is ToggleButton button && button != sackButton)
						button.IsChecked = false;
				}

				if (_additionalSackPanel is not null)
				{
					_additionalSackPanel.SwitchContent(LoadAdditionalSackContent(int.Parse(sackButton.Uid)));
					_additionalSackPanel.LoadItems();
				}
			}
		}

		private List<Item> LoadAdditionalSackContent(int id = -1)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(id, -1, nameof(id));
			ArgumentOutOfRangeException.ThrowIfGreaterThan(id, _additionalSackItems.Count, nameof(id));

			if (id != -1)
				return _additionalSackItems[id];

			int sackId = 0;

			foreach (FrameworkElement control in AdditionalSackTabs.Children)
			{
				if (control is ToggleButton button && button.IsChecked == true)
				{
					sackId = int.Parse(button.Uid);
					break;
				}
			}

			return _additionalSackItems[sackId];
		}

		private void PlayerSack_Unchecked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton sackButton)
			{
				foreach (FrameworkElement control in AdditionalSackTabs.Children)
				{
					if (control is ToggleButton button && button.IsChecked == true)
						return;
				}

				sackButton.IsChecked = true;
				e.Handled = true;
			}
		}

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			ArgumentNullException.ThrowIfNull(args, nameof(args));

			try
			{
				_uiUpdateSemaphore.Wait();
				UpdateGeneralDimensions(args);
				UpdateInventory(args);
				UpdateStorageAreaTab(args);
				UpdateTransferAreaTab(args);
				UpdateRelicVaultTab(args);
				UpdateEquipmentTab(args);
			}
			catch(Exception ex)
			{
				// TODO log exception
			}
			finally
			{
				_uiUpdateSemaphore.Release();
			}
		}

		private void UpdateGeneralDimensions(ContentScaleUpdatedEventArgs args)
		{
			PlayerContainer.RowDefinitions[1].Height = new GridLength(args.VaultTab.PlayerPanel.SpacerHeight);
		}

		private void UpdateInventory(ContentScaleUpdatedEventArgs args)
		{
			CreateMainSackPanel(args.General.ItemCellDimensions.Height);
			CreateAdditionalSackPanel(args.General.ItemCellDimensions.Width);
			SetHeaders(args.VaultTab.PlayerPanel.Inventory.ButtonHeight);
		}

		private void UpdateEquipmentTab(ContentScaleUpdatedEventArgs args)
		{
			Size dimensions = ItemsPanel.CalculateDimensions(args.General.ItemCellDimensions.Width, 16, 15, new Thickness(2));
			EquipmentContainer.Width = dimensions.Width;
			EquipmentContainer.Height = dimensions.Height;
		}

		private void UpdateStorageAreaTab(ContentScaleUpdatedEventArgs args)
		{
			TabStorageArea.Content = new ItemsPanel([], args.General.ItemCellDimensions.Width, 16, 15, new Thickness(2));
		}

		private void UpdateTransferAreaTab(ContentScaleUpdatedEventArgs args)
		{
			TabTransferArea.Content = new ItemsPanel([], args.General.ItemCellDimensions.Width, 16, 15, new Thickness(2));
		}

		private void UpdateRelicVaultTab(ContentScaleUpdatedEventArgs args)
		{
			TabRelicVault.Content = new ItemsPanel([], args.General.ItemCellDimensions.Width, 16, 15, new Thickness(2));
		}

		private void SetHeaders(double newHeaderWidthHeight)
		{
			PlayerInventory.RowDefinitions[0].Height = new GridLength(newHeaderWidthHeight);
			double itemHeight = newHeaderWidthHeight / 1.5;
			AdditionalSackTabs.ColumnDefinitions[0].Width = new GridLength(itemHeight);
			AdditionalSackTabs.ColumnDefinitions[1].Width = new GridLength(itemHeight);
			AdditionalSackTabs.ColumnDefinitions[2].Width = new GridLength(itemHeight);

			PlayerSackOne.Width = itemHeight;
			PlayerSackTwo.Width = itemHeight;
			PlayerSackThree.Width = itemHeight;
			PlayerSackOne.Height = itemHeight;
			PlayerSackTwo.Height = itemHeight;
			PlayerSackThree.Height = itemHeight;

			double autoSortScaleX = 2.8;
			double autoSortScaleY = 1.3;

			MainAutoSort.Width = itemHeight * autoSortScaleX;
			MainAutoSort.Height = itemHeight / autoSortScaleY;
			AdditionalAutoSort.Width = itemHeight * autoSortScaleX;
			AdditionalAutoSort.Height = itemHeight / autoSortScaleY;
		}

		private void CreateMainSackPanel(double cellWidthHeight)
		{
			try
			{
				PlayerInventory.Children.Remove(_mainSackPanel);

				Thickness thickness = new(2, 0, 2, 2);
				_mainSackPanel = new(_mainSackItems, cellWidthHeight, MainSackColumns, SackRows, thickness);

				PlayerInventory.Children.Add(_mainSackPanel);
				Grid.SetColumn(_mainSackPanel, 0);
				Grid.SetRow(_mainSackPanel, 1);

				PlayerInventory.ColumnDefinitions[0].Width = new GridLength(cellWidthHeight * MainSackColumns + thickness.Left + thickness.Right);
			}
			catch (Exception ex)
			{
				// TODO log exception
			}
		}

		private void CreateAdditionalSackPanel(double cellWidthHeight)
		{
			try
			{
				PlayerInventory.Children.Remove(_additionalSackPanel);

				Thickness thickness = new(2, 0, 2, 2);
				_additionalSackPanel = new(LoadAdditionalSackContent(), cellWidthHeight, AdditionalSackColumns, SackRows, thickness);

				PlayerInventory.Children.Add(_additionalSackPanel);
				Grid.SetColumn(_additionalSackPanel, 2);
				Grid.SetRow(_additionalSackPanel, 1);

				PlayerInventory.ColumnDefinitions[2].Width = new GridLength(cellWidthHeight * AdditionalSackColumns + thickness.Left + thickness.Right);
			}
			catch (Exception ex)
			{
				// TODO log exception
			}
		}

		public void Dispose()
		{
			ContentScaleService.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
