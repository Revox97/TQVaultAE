using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    // TODO Add dlc version
	public abstract class Item
	{
		/// <summary>
		/// The seed of the item.
		/// </summary>
		public int Seed { get; set; } = 0; // Todo generate new seed

		/// <summary>
		/// The name of the item.
		/// </summary>
		public string Name { get; set; } = string.Empty;

        public ItemVersion ItemVersion { get; set; }

        public abstract Brush Color { get; }

        public abstract Brush HoverColor { get; }

        public abstract bool HasVisualAccent { get; } 

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }

        protected Size _size;

		/// <summary>
		/// The size in cells in an <see langword="ItemsPanel"/>.
		/// </summary>
		public virtual Size Size
        {
            get => _size;
            set
            {
                if (value.Width < 1 || value.Height < 1 || value.Width > 2 || value.Height > 4)
                    throw new ArgumentException($"Invalid item size ({value.Width} - {value.Height}).");

                _size = value;
            }
        }

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

        public override string ToString()
        {
            return $"{Name} {GetItemVersionValue})";
        }

        protected string GetItemVersionValue()
        {
            return ItemVersion == ItemVersion.Original
                ? string.Empty
                : $"({EnumValueProvider.GetValue(ItemVersion)})";
        }
    }
}
