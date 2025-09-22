using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TQVaultAE.Models.Game
{
	public class Item
	{
		/// <summary>
		/// The seed of the item.
		/// </summary>
		public int Seed { get; set; } = 0; // Todo generate new seed

		/// <summary>
		/// The name of the item.
		/// </summary>
		public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The rarity of the item.
        /// </summary>
        public ItemRarity Rarity { get; set; } = ItemRarity.Legendary;

        /// <summary>
        /// The <see cref="ItemType"/> of the item.
        /// </summary>
        public ItemType ItemType { get; set; }

		/// <summary>
		/// The requirements of a <see cref="Player"/> in order to wear this <see cref="Item"/>.
		/// </summary>
		public ItemRequirements Requirements { get; set; }

		/// <summary>
		/// The size in cells in an <see langword="ItemsPanel"/>.
		/// </summary>
		public Size Size { get; set; }

		/// <summary>
		/// The location of the item in the grid.
		/// </summary>
		public Point Location { get; set; }

		/// <summary>
		/// The icon of the <see cref="Item"/>.
		/// </summary> 
		// TODO Add placeholder icon in case the vault cannot find any icon
		public BitmapImage Icon { get; set; } = new(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/MockItem2x4.png"));

		public Item() { }

		/// <summary>
		/// Calculates the <see cref="SolidColorBrush"/> matching the provided <see cref="ItemRarity"/>.
		/// </summary>
		/// <param name="rarity">The <see cref="ItemRarity"/> for which the <see cref="SolidColorBrush"/> should be calculated.</param>
		/// <returns>The <see cref="SolidColorBrush"/> matching the <see cref="ItemRarity"/>.</returns>
		public static SolidColorBrush GetBrushByRarity(ItemRarity rarity)
		{
			return rarity switch
			{
				ItemRarity.Broken => new SolidColorBrush(Colors.Gray),
				ItemRarity.Common => new SolidColorBrush(Colors.White),
				ItemRarity.Rare => new SolidColorBrush(Colors.Yellow),
				ItemRarity.MonsterRare => new SolidColorBrush(Colors.GreenYellow),
				ItemRarity.Epic => new SolidColorBrush(Colors.Blue),
				_ => new SolidColorBrush(Colors.Purple),
			};
		}
	}
}
