using TQVaultAE.Models.Game;

namespace TQVaultAE.Models.EventArgs
{
	public class ItemOverEventArgs 
	{
		public bool IsMouseOver { get; init; } = true;
		public string ItemName { get; init; } = string.Empty;
		public ItemRarity Rarity { get; init; } = ItemRarity.Broken;
	}
}
