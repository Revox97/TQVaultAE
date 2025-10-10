using System.ComponentModel;
using System.Runtime.CompilerServices;
using TQVaultAE.Models.Game;

namespace TQVaultAE.UI.Models
{
    internal class ItemDragWindowModel : INotifyPropertyChanged
    {
        private Item _item;

        // Design time constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ItemDragWindowModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public ItemDragWindowModel(Item item)
        {
            _item = item;
        }

        public Item Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
