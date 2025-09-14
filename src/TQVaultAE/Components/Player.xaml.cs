using System.Windows;
using System.Windows.Controls;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Components
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl, ICellWidthObserver
    {
		private const int MainSackColumns = 12;
		private const int SackRows = 5;
		private const int AdditionalSackColumns = 8;
		private const int PanelBorderThickness = 2;

		private ItemsPanel? _mainSackPanel;
		private ItemsPanel? _additionalSackPanel;

		private readonly SemaphoreSlim _uiUpdateSemaphore = new(1, 1);

        public Player()
        {
            InitializeComponent();
        }

		private void PlayerInventory_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			CellWidthController.GetInstance().AddObserver(this);
		}

		private void CreatePanels(double cellWidthHeight, double newHeaderWidthHeight)
		{
			
			CreateMainSackPanel(cellWidthHeight);
			CreateAdditionalSackPanel(cellWidthHeight);
			SetHeaders(newHeaderWidthHeight);
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
			MainAutoSort.Width = itemHeight * 4;
			MainAutoSort.Height = itemHeight;
			AdditionalAutoSort.Width = itemHeight * 4;
			AdditionalAutoSort.Height = itemHeight;

		}

		private void CreateMainSackPanel(double cellWidthHeight)
		{
			_uiUpdateSemaphore.Wait();

			try
			{
				PlayerInventory.Children.Remove(_mainSackPanel);
				_mainSackPanel = new(cellWidthHeight, MainSackColumns, SackRows, PanelBorderThickness);

				PlayerInventory.Children.Add(_mainSackPanel);
				Grid.SetRow(_mainSackPanel, 1);
				Grid.SetColumn(_mainSackPanel, 0);

				PlayerInventory.ColumnDefinitions[0].Width = new GridLength((cellWidthHeight * MainSackColumns) + (PanelBorderThickness * 2));
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

		private void CreateAdditionalSackPanel(double cellWidthHeight)
		{
			_uiUpdateSemaphore.Wait();

			try
			{
				PlayerInventory.Children.Remove(_additionalSackPanel);

				_additionalSackPanel = new(cellWidthHeight, AdditionalSackColumns, SackRows, PanelBorderThickness);

				PlayerInventory.Children.Add(_additionalSackPanel);
				Grid.SetRow(_additionalSackPanel, 1);
				Grid.SetColumn(_additionalSackPanel, 2);

				PlayerInventory.ColumnDefinitions[2].Width = new GridLength((cellWidthHeight * AdditionalSackColumns) + (PanelBorderThickness * 2));
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

		void ICellWidthObserver.Notify(object sender, CellWidthChangedEventArgs args) => CreatePanels(args.NewWidthHeight, args.NewHeaderWidthHeight);

		public void Dispose()
		{
			CellWidthController.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
