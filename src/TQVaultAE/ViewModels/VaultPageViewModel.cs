using System;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Model.Players;
using TQVaultAE.Model.Vaults;
using TQVaultAE.Models;
using TQVaultAE.Views.Controls;

namespace TQVaultAE.ViewModels
{
    public class VaultPageViewModel : ObservableObject
    {
        public Player? Player
        {
            get;
            set => SetProperty(ref field, value);
        }

        public Vault? Vault
        {
            get;
            set => SetProperty(ref field, value);
        }

        public ContentSelectorComboBox? VaultSelector
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(VaultSelector));
            }
        }

        public ContentSelectorComboBox? PlayerSelector
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(PlayerSelector));
            }
        }

        // Design time constructor
        public VaultPageViewModel()
        {
            if (Design.IsDesignMode)
            {
                VaultSelector = new ContentSelectorComboBox(
                    new Uri("avares://TQVaultAE/Assets/Img/icon_majestic_chest.png"),
                    ["Vault1", "Vault2"]);
                PlayerSelector = new ContentSelectorComboBox(
                    new Uri("avares://TQVaultAE/Assets/Img/icon_character.png"),
                    ["Player1", "Player2"]);
            }
            else
            {
                VaultSelector = new ContentSelectorComboBox(
                    new Uri("avares://TQVaultAE/Assets/Img/icon_majestic_chest.png"),
                    Program.Services.GetRequiredService<IVaultService>().GetVaultsAsync().Result.ConvertAll(x => x.Name)); // TODO Get rid of result call

                PlayerSelector = new ContentSelectorComboBox(
                    new Uri("avares://TQVaultAE/Assets/Img/icon_character.png"),
                    Program.Services.GetRequiredService<IPlayerService>().GetPlayerNamesAsync().Result); // TODO Get rid of result call
            }

            VaultSelector.SelectionChanged += VaultSelector_SelectionChanged;
            PlayerSelector.SelectionChanged += PlayerSelector_SelectionChanged;
        }

        private void VaultSelector_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            try
            {
                IVaultService vaultService = Program.Services.GetRequiredService<IVaultService>();
                Vault? vault = vaultService.GetVaultsAsync().Result.SingleOrDefault(x => x.Name == ((ItemContainer)e.AddedItems[0]!).Name); // TODO Get rid of result call

                Vault = vault is null ? null : vaultService.GetCompleteVaultAsync(vault).Result;
            }
            catch (Exception ex)
            {
                // TODO Log updating vault failed
            }
        }

        private void PlayerSelector_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            try
            {
                Player player = Program.Services.GetRequiredService<IPlayerService>().GetPlayerByNameAsync(((ItemContainer)e.AddedItems[0]!).Name).Result;
                Player = player;
            }
            catch (Exception ex)
            {
                // TODO log updating player failed
            }
        }
    }
}
