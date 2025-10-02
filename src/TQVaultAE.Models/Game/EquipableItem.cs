using System.Windows.Media;
using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    public abstract class EquipableItem : Item
    {
        public int CharmSlots { get; set; } = 0;

        public List<Charm> Charms { get; set; } = [];

        public List<ItemAttribute> Attributes { get; set; } = [];

        public Affix? Prefix { get; set; } = null;
        public Affix? Suffix { get; set; } = null;

        /// <summary>
        /// The rarity of the item.
        /// </summary>
        public ItemRarity Rarity { get; set; } = ItemRarity.Legendary;

		/// <summary>
		/// The requirements of a <see cref="Character"/> in order to wear this <see cref="Item"/>.
		/// </summary>
		public ItemRequirements Requirements { get; set; }

        public EquipableItem() : base() { }

        public virtual bool CanEquipCharm()
        {
            return Rarity < ItemRarity.Epic;
        }


        public override Brush Color
        {
            get
            {
                if (Rarity == ItemRarity.Common)
                {
                    if (Suffix is not null && Prefix is not null)
                        return new SolidColorBrush(Colors.Green);

                    if (Suffix is not null || Prefix is not null)
                        return new SolidColorBrush(Colors.Yellow);

                    return new SolidColorBrush(Colors.White);
                }

                if (Rarity == ItemRarity.Rare)
                    return new SolidColorBrush(Colors.GreenYellow);

                if (Rarity == ItemRarity.Epic)
                    return new SolidColorBrush(Colors.Blue);

                if (Rarity == ItemRarity.Legendary)
                    return new SolidColorBrush(Colors.Purple);

                return new SolidColorBrush(Colors.Gray);
            }
        }

        public override Brush HoverColor => throw new NotImplementedException();
    }
}
