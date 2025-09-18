using System.Windows;
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
			_windowSizeUpdater.AddObserver(this);
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

		private static ContentScaleUpdatedEventArgs CalculateControlDimensions(WindowSizeUpdatedEventArgs args)
		{
			double dropDownHeight = 70d;
			double contentWidth = args.ContentWidth;
			double contentHeight = args.ContentHeight - dropDownHeight - 30d;
			double autoSortWidth = 30d;

			double maxPlayerPanelWidth = (contentWidth / 2) * 1.1;
			double maxVaultPanelWidth = contentWidth - maxPlayerPanelWidth - autoSortWidth;

			double cellWidthHeight = CalculateCellWithHeight(maxPlayerPanelWidth, maxVaultPanelWidth, contentHeight);

			return new ContentScaleUpdatedEventArgs()
			{
				General = new()
				{
					FontSize = 14, // Calculate
					ItemHightlightLabelDimensions = new(12, 12), // Calculate
					ItemCellDimensions = new(cellWidthHeight, cellWidthHeight)
				},
				VaultTab = new()
				{
					SpacerWidth = 2f, // Calculate
					PlayerPanel = new()
					{
						SpacerHeight = 5d, // Calculate
						Inventory = new()
						{
							ButtonHeight = CalculateButtonWidthHeight(cellWidthHeight) // Calculate
						},
						Stash = new()
						{
							HeaderFontSize = 14, // Calculate
							StashDimensions = new Size(12, 12), // Calculate
							Equipment = new()
							{
								EquipmentDimensions = new Size(12, 12), // Calculate
								StatisticsDimension = new Size(12, 12) // Caluclate
							},
						}
					},
					VaultPanel = new()
					{
						ButtonWidthHeight = CalculateButtonWidthHeight(cellWidthHeight),
						AutoSortButtonWidth = 12 // Calculate
					}
				},
				ConfigurationTab = new()
				{
					// TODO extend once properties are added
				},
				SearchTab = new()
				{
					// TODO extend once properties are added
				}
			};
		}

		private static double CalculateCellWithHeight(double maxPlayerPanelWidth, double maxVaultPanelWidth, double height)
		{
			double borderWidthHeight = 2f;
			double spacerWidth = 5f;
			double spacerHeight = 5f;
			int rowsPlayer = 20;
			int rowsVault = 20;
			int columnsPlayer = 20;
			int columnsVault = 18;
			double controlPanelBorderWidthHeight = 5f;

			double availableWidthPlayer = maxPlayerPanelWidth - (borderWidthHeight * 4) - spacerWidth;
			double availableWidthVault = maxVaultPanelWidth - (borderWidthHeight * 2) - 30d; // autosortbutton
			double targetedCellWidth = availableWidthPlayer / columnsPlayer;

			double targetedInventoryButtonHeight = targetedCellWidth * 1.2;
			double targetedTabItemHeight = 50f;

			double availableHeightPlayer = (height - targetedInventoryButtonHeight - spacerHeight - targetedTabItemHeight - (controlPanelBorderWidthHeight * 2) - (borderWidthHeight * 2) - 5);
			double tempAvailableHeightVault = height - CalculateButtonWidthHeight(targetedCellWidth) - 25; // Item highlight label
			if (targetedCellWidth * rowsPlayer <= availableHeightPlayer && targetedCellWidth * rowsVault <= tempAvailableHeightVault)
				return targetedCellWidth;

			double targetedCellHeight = availableHeightPlayer / rowsPlayer;

			while (targetedCellHeight * columnsPlayer > availableWidthPlayer || targetedCellHeight * columnsVault > availableWidthVault)
				targetedCellHeight -= 1d;

			return targetedCellHeight;
		}

		private static double CalculateButtonWidthHeight(double cellWidthHeight)
		{
			int vaultButtonCount = 12;
			int vaultItemColumns = 18;
			double vaultBorderThickness = 2f;

			return ((cellWidthHeight * vaultItemColumns) + (vaultBorderThickness * 2)) / vaultButtonCount;
		}
	}
}
