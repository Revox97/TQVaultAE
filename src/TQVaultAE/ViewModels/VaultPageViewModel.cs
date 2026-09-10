using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Model.Players;
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
                    ["Vault1", "Vault2"]);
                PlayerSelector = new ContentSelectorComboBox(
                    new Uri("avares://TQVaultAE/Assets/Img/icon_character.png"),
                    Program.Services.GetRequiredService<IPlayerService>().GetPlayerNamesAsync().Result); // TODO Get rid of result call
            }

            VaultSelector.SelectionChanged += VaultSelector_SelectionChanged;
            PlayerSelector.SelectionChanged += PlayerSelector_SelectionChanged;
        }

        // TODO implement
        private void VaultSelector_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            return;
        }

        private void PlayerSelector_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            try
            {
                Player player = Program.Services.GetRequiredService<IPlayerService>().GetPlayerByNameAsync(((ItemContainer)e.AddedItems[0]!).Name).Result;
                Player = player;
            }
            catch(Exception ex)
            {
                // TODO log updating player failed
            }

            // TODO Get stash
        }
    }
}
