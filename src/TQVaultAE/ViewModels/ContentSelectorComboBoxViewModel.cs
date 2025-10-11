using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using TQVaultAE.Models;
using TQVaultAE.Styles;

namespace TQVaultAE.ViewModels
{
    internal class ContentSelectorComboBoxViewModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private IBrush? _icon;
        public IBrush? Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        // TODO This needs to be more generic
        public ObservableCollection<ItemContainer> Items { get; set; } = [];

        private ItemContainer? _selectedItem;
        public ItemContainer? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        // Needed for Design Time
        // AVALONIA WARNINGS ARE DUMB AS HELL, GET RID OF THEM
        public ContentSelectorComboBoxViewModel()
        {
            SetIcon(Images.Icons.MajesticChest);
            Items =
            [
                new() { Name = "Vault1" },
                new() { Name = "Vault2" },
                new() { Name = "Vault3" },
                new() { Name = "Vault4" },
            ];

            SelectedItem = Items[0];
        }

        public ContentSelectorComboBoxViewModel(Uri icon, List<ItemContainer> items)
        {
            SetIcon(icon);
            Items = new ObservableCollection<ItemContainer>(items);
        }

        public void SetIcon(Uri uri)
        {
            Icon = new ImageBrush
            {
                Source = new Bitmap(AssetLoader.Open(uri)),
                AlignmentX = AlignmentX.Center,
                AlignmentY = AlignmentY.Center,
                Stretch = Stretch.Fill,
                TileMode = TileMode.None
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
