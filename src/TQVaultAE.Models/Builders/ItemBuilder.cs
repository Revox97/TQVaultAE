using System.Drawing;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.Game;
using TQVaultAE.Models.Game.Components;
using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Builders
{
    // TODO Rework this builder to work with various item types
	public class ItemBuilder
    {
        private readonly ItemCategory _targetCategory;
        private IEquipmentComponent? _equipmentComponent = null!;
        private ISizeComponent? _sizeComponent = null!;
        private IItemProducer? _itemProducerComponent = null!;

        private int _seed = -1;
        private string _description = string.Empty;
        private string _name = string.Empty;
        private BitmapImage? _icon = null!;
        private ItemVersion _itemVersion = ItemVersion.Unknown;
        private ItemAttribute[] _attributes = [];

        public ItemBuilder(ItemCategory targetCategory)
        {
            _targetCategory = targetCategory;

            if (_targetCategory is ItemCategory.Potion or ItemCategory.PotionEE or ItemCategory.Dye or ItemCategory.Charm)
                _sizeComponent = new FixedSizeComponent(1, 1);

            if (_targetCategory is ItemCategory.Artifact or ItemCategory.Scroll)
                _sizeComponent = new FixedSizeComponent(2, 2);

            if (_targetCategory is ItemCategory.Formula)
                _sizeComponent = new FixedSizeComponent(2, 1);
        }

        public ItemBuilder AddName(string name)
        {
            _name = name;
            return this;
        }

        // TODO Should not be handled within item
        public ItemBuilder AddLocation(int x, int y) => AddLocation(new Point(x, y));

        // TODO Should not be handled within item
        public ItemBuilder AddLocation(Point point)
        {
            throw new NotImplementedException();
        }

        public ItemBuilder AddRarity(ItemRarity rarity)
        {
            if (_targetCategory is not ItemCategory.Weapon and not ItemCategory.Gear)
                throw new InvalidOperationException($"Type '{_targetCategory}' cannot have a rarity value.");

            if (_equipmentComponent is null)
                throw new InvalidOperationException("Item must have an equipment component.");

            _equipmentComponent.Rarity = rarity;
            return this;
        }

        public ItemBuilder AddSeed(int seed)
        {
            _seed = seed;
            return this;
        }

        public ItemBuilder AddItemVersion(ItemVersion version)
        {
            _itemVersion = version;
            return this;
        }

        public ItemBuilder AddDescription(string description)
        {
            _description = description;
            return this;
        }

        public ItemBuilder AddIcon(Uri path) => AddIcon(new BitmapImage(path));

        public ItemBuilder AddIcon(BitmapImage icon)
        {
            _icon = icon;
            return this;
        }

        public ItemBuilder AddEquipmentComponent(ItemRarity itemRarity)
        {
            if (_targetCategory is not ItemCategory.Gear and not ItemCategory.Weapon)
                throw new InvalidOperationException($"Type {_targetCategory} can not have a rarity.");

            _equipmentComponent = itemRarity switch
            {
                ItemRarity.Epic => new EpicEquipmentComponent(),
                ItemRarity.Legendary => new LegendaryEquipmentComponent(),
                ItemRarity.Common => new CommonEquipmentComponent(),
                _ => new BrokenEquipmentComponent()
            };

            return this;
        }

        public void AddAttributes(params ItemAttribute[] attributes)
        {
            if (_targetCategory is ItemCategory.Potion or ItemCategory.Dye)
                throw new InvalidOperationException($"Type {_targetCategory} cannot have attributes.");

            // TODO Increment instead of overwrite
            _attributes = attributes;
        }

        public void AddCharm(Item charm)
        {
            ArgumentNullException.ThrowIfNull(charm, nameof(charm));

            if (charm.ItemType != ItemCategory.Charm)
                throw new ArgumentException("Item must be of type charm.");

            // TODO Verify charm can be applied to the specific item type (maybe within equipment strategy itself)

            if (_equipmentComponent is null)
                throw new InvalidOperationException("Item must be equipment.");

            if (_equipmentComponent.NumberCharmSlots == 0)
                throw new InvalidOperationException("Item cannot have charms.");

            if (_equipmentComponent.Charms.Count + 1 >= _equipmentComponent.NumberCharmSlots)
                throw new InvalidOperationException("Item already has maximum number of charms equipped.");

            _equipmentComponent.Charms.Add(charm);
        }

        public ItemBuilder AddProducedItem(Item item)
        {
            if (_targetCategory is not ItemCategory.Formula)
                throw new InvalidOperationException($"{_targetCategory} cannot produce another item.");

            _itemProducerComponent = new ItemProducerComponent(item);
            return this;
        }

        public ItemBuilder AddSize(int width, int height) => AddSize(new Size(width, height));

        public ItemBuilder AddSize(Size size)
        {
            if (_targetCategory is ItemCategory.Potion or ItemCategory.Formula or ItemCategory.PotionEE or ItemCategory.Artifact or ItemCategory.Charm or ItemCategory.Dye or ItemCategory.Scroll)
                throw new InvalidOperationException($"Type '{_targetCategory}' has a fixed size. It cannot be set manually.");

            _sizeComponent = new FixedSizeComponent(size);
            return this;
        }

        public Item Build()
        {
            // TODO
            // Validate all components are valid
            // build

            throw new NotImplementedException();
        }
	}
}
