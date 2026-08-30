using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TQVaultAE.Views.Controls;

namespace TQVaultAE.ViewModels
{
    internal class VaultPageViewModel : INotifyPropertyChanged
    {
        private ContentSelectorComboBox? _itemSelectorComboBoxLeft;
        public ContentSelectorComboBox? ItemContainerSelectorLeft
        {
            get => _itemSelectorComboBoxLeft;
            set
            {
                _itemSelectorComboBoxLeft = value;
                OnPropertyChanged(nameof(ItemContainerSelectorLeft));
            }
        } 

        private ContentSelectorComboBox? _itemSelectorComboBoxRight;
        public ContentSelectorComboBox? ItemContainerSelectorRight
        {
            get => _itemSelectorComboBoxRight;
            set
            {
                _itemSelectorComboBoxRight = value;
                OnPropertyChanged(nameof(ItemContainerSelectorRight));
            }
        } 

        // Design time constructor
        public VaultPageViewModel()
        {
            ItemContainerSelectorLeft = new ContentSelectorComboBox(new Uri("avares://TQVaultAE/Assets/Img/icon_majestic_chest.png"));
            ItemContainerSelectorRight = new ContentSelectorComboBox(new Uri("avares://TQVaultAE/Assets/Img/icon_character.png"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
