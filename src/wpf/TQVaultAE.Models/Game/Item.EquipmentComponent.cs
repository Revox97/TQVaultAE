using System.Collections.ObjectModel;
using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game
{
    public partial class Item
    {
        private readonly IEquipmentComponent? _equipmentComponent = null!;

        public bool IsEquippable => _equipmentComponent is not null;

        public ObservableCollection<ItemAttribute> Attributes
        {
            get
            {
                return IsEquippable
                    ? new ObservableCollection<ItemAttribute>([.. _equipmentComponent!.Attributes])
                    : [];
            }
        }

        public ItemRarity Rarity
        {
            get
            {
                return IsEquippable
                    ? _equipmentComponent!.Rarity
                    : throw new NotSupportedException("Item is not equipable and therefore has no rarity.");
            }
        }

        public Affix? Prefix
        {
            get
            {
                return IsEquippable
                    ? _equipmentComponent!.Prefix
                    : throw new NotSupportedException("Item is not equipable and therefore has no prefix.");
            }
        }

        public Affix? Suffix
        {
            get
            {
                return IsEquippable
                    ? _equipmentComponent!.Suffix
                    : throw new NotSupportedException("Item is not equipable and therefore has no suffix.");
            }
        }

        public int NumberOfCharmSlots
        {
            get
            {
                return IsEquippable
                    ? _equipmentComponent!.NumberCharmSlots
                    : throw new NotSupportedException("Item is not equipable and therefore has no charm slots.");
            }
        }

        public Item[] Charms
        {
            get
            {
                return IsEquippable
                    ? [.. _equipmentComponent!.Charms]
                    : throw new NotSupportedException("Item is not equipable and therefore has no charm slots.");
            }
        }

        public bool CanEquipCharm()
        {
            return IsEquippable && _equipmentComponent!.CanEquipCharm();
        }

        public ItemRequirements Requirements
        {
            get
            {
                return IsEquippable
                    ? _equipmentComponent!.Requirements
                    : default;
            }
        }
    }
}
