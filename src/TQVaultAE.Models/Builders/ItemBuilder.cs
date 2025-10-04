using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Windows.Media;
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
        private IStackingComponent? _stackingComponent = null!;

        private int _seed = -1;
        private string _description = string.Empty;
        private string _name = string.Empty;
        private BitmapImage? _icon = null!;
        private ItemVersion _itemVersion = ItemVersion.Unknown;
        private ItemAttribute[] _attributes = [];
        private Point? _location = null;

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

        public ItemBuilder AddStacking()
        {
            // TODO Handle finding of correct strategy
            _stackingComponent = new CharmStackingComponent(CharmType.Charm, 1);
            return this;
        }

        public ItemBuilder AddName(string name)
        {
            _name = name;
            return this;
        }

        // TODO Should not be handled within item
        public ItemBuilder AddLocation(int x, int y) => AddLocation(new Point(x, y));

        // TODO Should not be handled within item
        public ItemBuilder AddLocation(System.Drawing.Point point)
        {
            _location = point;
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
            if (_targetCategory is not ItemCategory.Gear and not ItemCategory.Weapon and not ItemCategory.Artifact)
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

        public ItemBuilder AddRequirements(int level, int strength, int dexterity, int intelligence)
        {
            if (_targetCategory is not ItemCategory.Gear and not ItemCategory.Weapon and not ItemCategory.Artifact)
                throw new InvalidOperationException($"Requirements are not valid for type '{_targetCategory}'.");

            if (_equipmentComponent is null)
                throw new InvalidOperationException("Equipable item must be initialized first.");

            _equipmentComponent.Requirements = new ItemRequirements(level, strength, dexterity, intelligence);
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

            if (charm.Category != ItemCategory.Charm)
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
            try
            {
                ValidateItemComponents();

                return new(
                    _name,
                    _description,
                    _targetCategory,
                    _itemVersion,
                    _icon!,
                    _sizeComponent!,
                    _equipmentComponent,
                    _stackingComponent,
                    _itemProducerComponent,
                    _seed,
                    _location
                )
                {
                    Color = GetColor(),
                    HoverColor = GetHoverColor(),
                    HasAccent = HasIconAccent(),
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Building item failed.", ex);
            }
        }

        // TODO Refactor to make this more readable
        public void ValidateItemComponents()
        {
            if (string.IsNullOrEmpty(_name))
                throw new ValidationException("Item must have a name.");

            if (string.IsNullOrEmpty(_description))
                throw new ValidationException("Item must have a description.");

            if (_itemVersion is ItemVersion.Unknown)
                throw new ValidationException("Item must have an item version.");

            if (_icon is null)
                throw new ValidationException("Item must have an icon.");

            if (_sizeComponent is null)
                throw new ValidationException("Item must have a size.");

            if (_equipmentComponent is null && (_targetCategory is ItemCategory.Gear or ItemCategory.Weapon or ItemCategory.Artifact))
            {
                throw new ValidationException($"Item type '{_targetCategory}' must be equipable.");
            }
            else if (_equipmentComponent is not null)
            {
                if (_targetCategory is not ItemCategory.Gear and not ItemCategory.Weapon and not ItemCategory.Artifact)
                    throw new ValidationException($"Item type '{_targetCategory}' cannot be equipable.");

                if (_equipmentComponent.Requirements == default)
                    throw new ValidationException($"Item type '{_targetCategory}' must have requirements.");
            }

            if (_stackingComponent is null && (_targetCategory is ItemCategory.Charm or ItemCategory.Potion or ItemCategory.PotionEE or ItemCategory.Scroll))
            {
                throw new ValidationException($"Item type '{_targetCategory}' must be stackable.");
            }
            else if (_stackingComponent is not null)
            {
                if (_targetCategory is not ItemCategory.Charm and not ItemCategory.Potion and not ItemCategory.PotionEE and not ItemCategory.Scroll)
                    throw new ValidationException($"Item type '{_targetCategory}' cannot be stackable.");

                if (_stackingComponent!.MaxStackSize < 1)
                    throw new ValidationException($"Item type '{_targetCategory}' must have a maximum stack size.");
            }

            // TODO remove once location is moved out of item
            if (_location is null)
                throw new ValidationException("Item must have a location.");

            if (_itemProducerComponent is null && _targetCategory is ItemCategory.Formula)
            {
                throw new ValidationException($"Item type '{_targetCategory}' must be producing.");
            }
            else if (_itemProducerComponent is not null)
            {
                if (_targetCategory is not ItemCategory.Formula)
                    throw new ValidationException($"Item type '{_targetCategory}' cannot be producing.");

                if (_itemProducerComponent.GetProducedItem() is null)
                    throw new ValidationException($"Item type '{_targetCategory}' must contain produced item.");

                // TODO Add requiredcomponents once implemented
            }
        }

        public Brush GetColor()
        {
            return _targetCategory switch
            {
                ItemCategory.Formula => new SolidColorBrush(TitanQuestColor.Aqua.Color()),
                ItemCategory.Artifact => new SolidColorBrush(TitanQuestColor.Aqua.Color()),
                ItemCategory.Charm => new SolidColorBrush(TitanQuestColor.Khaki.Color()), // TODO Verify this is really the correct color
                ItemCategory.Scroll => new SolidColorBrush(TitanQuestColor.GreenYellow.Color()),
                ItemCategory.Quest => new SolidColorBrush(TitanQuestColor.Indigo.Color()),
                ItemCategory.Gear or ItemCategory.Weapon => GetEquipmentColor(),
                _ => new SolidColorBrush(TitanQuestColor.DarkGray.Color())
            };
        }

        private SolidColorBrush GetEquipmentColor()
        {
            // TODO Requirements not met:
            //return new SolidColorBrush(TitanQuestColor.Maroon.Color());

            return _equipmentComponent!.Rarity switch
            {
                ItemRarity.Broken => new SolidColorBrush(TitanQuestColor.Silver.Color()), // TODO verify this color is correct
                ItemRarity.Common => GetCommonEquipmentColor(),
                ItemRarity.Rare => new SolidColorBrush(TitanQuestColor.GreenYellow.Color()),
                ItemRarity.Epic => new SolidColorBrush(TitanQuestColor.Blue.Color()),
                _ => new SolidColorBrush(TitanQuestColor.Indigo.Color()),
            };
        }

        private SolidColorBrush GetCommonEquipmentColor()
        {
            if (_equipmentComponent!.Prefix is null && _equipmentComponent!.Suffix is null)
                return new SolidColorBrush(TitanQuestColor.Silver.Color());

            if (_equipmentComponent!.Prefix is not null && _equipmentComponent!.Suffix is not null)
                return new SolidColorBrush(TitanQuestColor.Green.Color()); // TODO Get correct color - Maybe its only gradients on top

            return new SolidColorBrush(TitanQuestColor.Yellow.Color());
        }

        public Brush GetHoverColor()
        {
            return _targetCategory switch
            {
                ItemCategory.Formula => new SolidColorBrush(TitanQuestColor.Turquoise.Color()), // TODO this or turquoise
                ItemCategory.Artifact => new SolidColorBrush(TitanQuestColor.Turquoise.Color()),
                ItemCategory.Charm => new SolidColorBrush(TitanQuestColor.Orange.Color()),
                ItemCategory.Scroll => new SolidColorBrush(TitanQuestColor.GreenYellow.Color()),
                ItemCategory.Quest => new SolidColorBrush(TitanQuestColor.Fuschia.Color()), // TODO verify wether fushia or purple
                ItemCategory.Gear or ItemCategory.Weapon => GetEquipmentHoverColor(),
                _ => new SolidColorBrush(TitanQuestColor.DarkGray.Color())
            };
        }

        private SolidColorBrush GetEquipmentHoverColor()
        {
            // TODO Requirements not met:
            //return new SolidColorBrush(TitanQuestColor.Red.Color());

            return _equipmentComponent!.Rarity switch
            {
                ItemRarity.Broken => new SolidColorBrush(TitanQuestColor.Silver.Color()), // TODO Verify this color
                ItemRarity.Common => GetCommonEquipmentHoverColor(),
                ItemRarity.Rare => new SolidColorBrush(TitanQuestColor.GreenYellow.Color()),
                ItemRarity.Epic => new SolidColorBrush(TitanQuestColor.Blue.Color()),
                _ => new SolidColorBrush(TitanQuestColor.Purple.Color()), // TODO verify wether fushia or purple
            };
        }

        private SolidColorBrush GetCommonEquipmentHoverColor()
        {
            if (_equipmentComponent!.Prefix is null && _equipmentComponent!.Suffix is null)
                return new SolidColorBrush(TitanQuestColor.DarkGray.Color());

            if (_equipmentComponent!.Prefix is not null && _equipmentComponent!.Suffix is not null)
                return new SolidColorBrush(TitanQuestColor.Green.Color());

            return new SolidColorBrush(TitanQuestColor.Yellow.Color());
        }

        private bool HasIconAccent()
        {
            return _targetCategory switch
            {
                ItemCategory.Formula or ItemCategory.Artifact or ItemCategory.Scroll or ItemCategory.Quest => true,
                ItemCategory.Gear or ItemCategory.Weapon => new Func<bool>(() =>
                {
                    if (_equipmentComponent!.Rarity is ItemRarity.Broken)
                        return false;

                    if (_equipmentComponent.Rarity is ItemRarity.Common && (_equipmentComponent.Prefix is null && _equipmentComponent.Suffix is null))
                        return false;

                    return true;
                })(),
                _ => false
            };

        }
	}
}
