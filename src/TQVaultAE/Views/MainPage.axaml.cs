using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views;

public partial class MainPage : UserControl
{
    private readonly MainPageViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainPageViewModel();

        DataContext = _viewModel;

        ToggleButtonVault.IsCheckedChanged += VaultCheckedChanged;
        ToggleButtonSearch.IsCheckedChanged += SearchCheckedChanged;
        ToggleButtonSettings.IsCheckedChanged += SettingsCheckedChanged;

        ToggleButtonVault.IsChecked = true;
    }

    private void VaultCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton button && (button.IsChecked ?? false))
        {
            _viewModel.SwitchPage(_viewModel.VaultPage);
            ToggleButtonSearch.IsChecked = false;
            ToggleButtonSettings.IsChecked = false;
        }
    }

    private void SearchCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton button && (button.IsChecked ?? false))
        {
            _viewModel.SwitchPage(_viewModel.SearchPage);
            ToggleButtonVault.IsChecked = false;
            ToggleButtonSettings.IsChecked = false;
        }
    }

    private void SettingsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton button && (button.IsChecked ?? false))
        {
            _viewModel.SwitchPage(_viewModel.SettingsPage);
            ToggleButtonVault.IsChecked = false;
            ToggleButtonSearch.IsChecked = false;
        }
    }
}