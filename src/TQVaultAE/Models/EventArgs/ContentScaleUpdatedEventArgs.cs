using System.Windows;

namespace TQVaultAE.Models.EventArgs
{
	internal class ContentScaleUpdatedEventArgs
	{
		public GeneralDimensions General { get; init; }
		public VaultTabDimensions VaultTab { get; init; }
		public SearchTabDimensions SearchTab { get; init; }
		public ConfigurationTabDimensions ConfigurationTab { get; init; }
	}

	internal record struct GeneralDimensions(int FontSize, Size ItemHightlightLabelDimensions, Size ItemCellDimensions);

	internal record struct VaultTabDimensions(VaultPanelDimensions VaultPanel, PlayerPanelDimensions PlayerPanel, double SpacerWidth);
	internal record struct VaultPanelDimensions(double AutoSortButtonWidth);
	internal record struct PlayerPanelDimensions(PlayerInventoryDimensions Inventory);
	internal record struct PlayerInventoryDimensions(double ButtonHeight);
	internal record struct PlayerStashDimensions(Size StashDimensions, int HeaderFontSize, PlayerEquipmentDimensions Equipment);
	internal record struct PlayerEquipmentDimensions(Size EquipmentDimensions, Size StatisticsDimension, int StatisticsFontSize);

	internal record struct SearchTabDimensions();
	internal record struct ConfigurationTabDimensions();
}
