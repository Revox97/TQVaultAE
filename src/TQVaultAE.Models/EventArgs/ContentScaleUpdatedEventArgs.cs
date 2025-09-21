using System.Windows;

namespace TQVaultAE.Models.EventArgs
{
	public class ContentScaleUpdatedEventArgs
	{
		public GeneralDimensions General { get; init; }
		public VaultTabDimensions VaultTab { get; init; }
		public SearchTabDimensions SearchTab { get; init; }
		public ConfigurationTabDimensions ConfigurationTab { get; init; }
	}

	public record struct GeneralDimensions(int FontSize, Size ItemHightlightLabelDimensions, Size ItemCellDimensions);

	public record struct VaultTabDimensions(VaultPanelDimensions VaultPanel, PlayerPanelDimensions PlayerPanel, double SpacerWidth);
	public record struct VaultPanelDimensions(double AutoSortButtonWidth, double ButtonWidthHeight);
	public record struct PlayerPanelDimensions(PlayerInventoryDimensions Inventory, PlayerStashDimensions Stash, double SpacerHeight);
	public record struct PlayerInventoryDimensions(double ButtonHeight);
	public record struct PlayerStashDimensions(Size StashDimensions, int HeaderFontSize, PlayerEquipmentDimensions Equipment);
	public record struct PlayerEquipmentDimensions(Size EquipmentDimensions, Size StatisticsDimension, int StatisticsFontSize);

	public record struct SearchTabDimensions();
	public record struct ConfigurationTabDimensions();
}
