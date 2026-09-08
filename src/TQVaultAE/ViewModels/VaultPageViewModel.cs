using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Views.Controls;

namespace TQVaultAE.ViewModels
{
    internal class VaultPageViewModel : INotifyPropertyChanged
    {
        public ContentSelectorComboBox? ItemContainerSelectorLeft
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(ItemContainerSelectorLeft));
            }
        }

        public ContentSelectorComboBox? ItemContainerSelectorRight
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(ItemContainerSelectorRight));
            }
        } 

        // Design time constructor
        public VaultPageViewModel()
        {
            ItemContainerSelectorLeft = new ContentSelectorComboBox(
                new Uri("avares://TQVaultAE/Assets/Img/icon_majestic_chest.png"),
                ["Vault1", "Vault2"]);
            ItemContainerSelectorRight = new ContentSelectorComboBox(
                new Uri("avares://TQVaultAE/Assets/Img/icon_character.png"),
                Program.Services.GetRequiredService<IPlayerService>().GetPlayerNamesAsync().Result); // TODO Get rid of result call
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
