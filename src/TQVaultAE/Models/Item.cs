// TODO Move into seperate project

using System.Drawing;
using System.Windows.Media.Imaging;

namespace TQVaultAE.Models
{
	internal class Item
	{
		/// <summary>
		/// The seed of the item.
		/// </summary>
		internal int Seed { get; init; } // Todo generate new seed

		/// <summary>
		/// The name of the item.
		/// </summary>
		internal string Name { get; set; }

		/// <summary>
		/// The size in cells in an <see langword="ItemsPanel"/>.
		/// </summary>
		internal Size Size { get; set; }

		/// <summary>
		/// The location of the item in the grid.
		/// </summary>
		internal Point Location { get; set; }

		internal BitmapImage Icon { get; set; } = new(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/inventorybagup01.png")); // TODO Placeholder

		public Item(string name, Point location, Size size, Uri iconAddress, int? seed = null)
		{
			Seed = seed ?? 0;
			Name = name;
			Size = size;
			Location = location;
			Icon = new BitmapImage(iconAddress);
		}

		public Item(string name, Point location, Size size, BitmapImage icon, int? seed = null)
		{
			Seed = seed ?? 0;
			Name = name;
			Size = size;
			Location = location;
			Icon = icon;
		}

		internal bool IsLocationOverlap(Item other)
		{
			if (other == this)
				return false;

			for (int i = other.Location.X; i < other.Location.X + other.Size.Width - 1; ++i)
			{
				for (int k = Location.X; k < Location.X + Size.Width - 1; ++k)
				{
					if (i == k)
						return true;
				}
			}

			for (int i = other.Location.Y; i < other.Location.Y + other.Size.Height - 1; ++i)
			{
				for (int k = Location.Y; k < Location.Y + Size.Height - 1; ++k)
				{
					if (i == k)
						return true;
				}
			}

			return false;
		}
	}
}
