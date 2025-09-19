using System.Drawing;
using System.Windows.Media.Imaging;

namespace TQVaultAE.Models.Game
{
	public class ItemBuilder
	{
		private readonly Item _item = new();

		public Item Create() => _item;

		public ItemBuilder SetName(string name)
		{
			_item.Name = name;
			return this;
		}

		public ItemBuilder SetSeed(int seed)
		{
			_item.Seed = seed;
			return this;
		}

		public ItemBuilder SetSize(Size size)
		{
			_item.Size = size;
			return this;
		}

		public ItemBuilder SetSize(int width, int height)
		{
			_item.Size = new Size(width, height);
			return this;
		}

		public ItemBuilder SetLocation(Point location)
		{
			_item.Location = location;
			return this;
		}

		public ItemBuilder SetLocation(int x, int y)
		{
			_item.Location = new Point(x, y);
			return this;
		}

		public ItemBuilder SetDimensions(int x, int y, int width, int height)
		{
			_item.Location = new Point(x, y);
			_item.Size = new Size(width, height);
			return this;
		}

		public ItemBuilder SetDimensions(Point location, Size size)
		{
			_item.Location = location;
			_item.Size = size;
			return this;
		}

		public ItemBuilder SetIcon(Uri iconAddress)
		{
			_item.Icon = new BitmapImage(iconAddress);
			return this;
		}

		public ItemBuilder SetIcon(BitmapImage icon)
		{
			_item.Icon = icon;
			return this;
		}

		public ItemBuilder SetRarity(ItemRarity rarity)
		{
			_item.Rarity = rarity;
			return this;
		}

		public ItemBuilder SetRequirements(ItemRequirements requirements)
		{
			_item.Requirements = requirements;
			return this;
		}

		public ItemBuilder SetRequirements(int level, int strength, int dexterity, int intelligence)
		{
			_item.Requirements = new ItemRequirements(level, strength, dexterity, intelligence);
			return this;
		}
	}
}
