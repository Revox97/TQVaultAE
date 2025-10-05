using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using TQVaultAE.Models.Game;
using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.UI.Models
{
    internal class ItemDetailWindowModel : INotifyPropertyChanged
    {
        private Item _item;
        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public string ItemName
        {
            get
            {
                string name = Item.Name;

                if (Item.StackSize != 0)
                    name += $" ({Item.StackSize})";

                return name;
            }
        }

        public string ItemDlcName => Item.ItemVersion switch
        {
            ItemVersion.EternalEmbers => "Eternal Embers Item",
            ItemVersion.Atlantis => "Atlantis Item",
            ItemVersion.Ragnarok => "Ragnarok Item",
            ItemVersion.ImmortalThrone => "Immortal Throne Item",
            _ => string.Empty
        };

        public string ItemSeedValue
        {
            get
            {
                int seed = Item.Seed;

                return new StringBuilder("itemSeed: ")
                    .Append(seed)
                    .Append(" (0x").Append(seed.ToString("X")).Append(')')
                    .Append(" (").Append("10%").Append(')').ToString(); // TODO Get correct percentage value
            }
        }

        // Constructor for design time
        public ItemDetailWindowModel()
        {
            DesignAttributes =
            [
                new ItemAttribute() { Value = "15 Vitality Damage" },
                new ItemAttribute() { Value = "3.0% Chance of 50 % Reduction to Enemy's Health" },
                new ItemAttribute() { Value = "6 Fire Damage" },
                new ItemAttribute() { Value = "+8% Fire Damage" },
                new ItemAttribute() { Value = "+8% Burn Damage" },
            ];
        }

        private ObservableCollection<ItemAttribute> _designAttributes;
        public ObservableCollection<ItemAttribute> DesignAttributes
        {
            get => _designAttributes;
            set
            {
                _designAttributes = value;
                OnPropertyChanged(nameof(DesignAttributes));
            }
        }

        public ItemDetailWindowModel(Item item)
        {
            _item = item;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
