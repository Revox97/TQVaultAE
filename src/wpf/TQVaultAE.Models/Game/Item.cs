using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game
{
	public partial class Item : DependencyObject
	{
        public ItemCategory Category;

		/// <summary>
		/// The seed of the item.
		/// </summary>
		public int Seed { get; set; } = 0; // Todo generate new seed

		/// <summary>
		/// The name of the item.
		/// </summary>
		public string Name { get; set; } = string.Empty;

        public ItemVersion ItemVersion { get; set; }

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The second line of the description (if there is one) of the item.
        /// </summary>
        public string? AdditionalDescription { get; set; }

        public bool HasAdditionalDescription => AdditionalDescription is not null;

        public bool IsDlc => ItemVersion != ItemVersion.Original;

		/// <summary>
		/// The location of the item in the grid.
		/// </summary>
        // TODO Handle this somewhere else, not in item
		public System.Drawing.Point Location { get; set; }

		/// <summary>
		/// The icon of the <see cref="Item"/>.
		/// </summary> 
		// TODO Add placeholder icon in case the vault cannot find any icon
		public BitmapImage Icon { get; set; } = new(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/MockItem2x4.png"));

        internal static DependencyProperty ColorProptery = DependencyProperty.Register(nameof(Color), typeof(Brush), typeof(Item));
        public Brush Color
        {
            get => (Brush)GetValue(ColorProptery);
            set => SetValue(ColorProptery, value);
        }

        internal static DependencyProperty HoverColorProptery = DependencyProperty.Register(nameof(HoverColor), typeof(Brush), typeof(Item));
        public Brush HoverColor
        {
            get => (Brush)GetValue(HoverColorProptery);
            set => SetValue(HoverColorProptery, value);
        }

        internal static DependencyProperty HasAccentProptery = DependencyProperty.Register(nameof(HasAccent), typeof(bool), typeof(Item));
        public bool HasAccent
        {
            get => (bool)GetValue(HasAccentProptery);
            set => SetValue(HasAccentProptery, value);
        }

        internal Item(
            string name,
            string description,
            ItemCategory category,
            ItemVersion itemVersion,
            BitmapImage icon,
            ISizeComponent sizeComponent,
            IEquipmentComponent? equipmentComponent = null,
            IStackingComponent? stackingComponent = null,
            IItemProducer? itemProducer = null,
            int? seed = null,
            System.Drawing.Point? location = null!
        )
        {
            Name = name;
            Description = description;
            Category = category;
            ItemVersion = itemVersion;
            Icon = icon;
            _sizeComponent = sizeComponent;
            _equipmentComponent = equipmentComponent;
            _stackingComponent = stackingComponent;
            _itemProducerComponent = itemProducer;
            Seed = seed ?? -1; // TODO Generate new seed
            Location = location ?? new System.Drawing.Point(0, 0);
        }

        public override string ToString()
        {
            return $"{Name}{GetStackingAmount()}{GetItemVersionValue()}";
        }

        protected string GetStackingAmount()
        {
            return _stackingComponent is not null ? $" ({_stackingComponent.StackSize})" : string.Empty;
        }

        protected string GetItemVersionValue()
        {
            // TODO remove circular dependencies
            //: $"({EnumValueProvider.GetValue(ItemVersion)})";

            return ItemVersion switch
            {
                ItemVersion.ImmortalThrone => (" (IT)"),
                ItemVersion.Ragnarok => (" (RAG)"),
                ItemVersion.Atlantis => (" (ATL)"),
                ItemVersion.EternalEmbers => (" (EE)"),
                _ => string.Empty,
            };
        }
    }
}
