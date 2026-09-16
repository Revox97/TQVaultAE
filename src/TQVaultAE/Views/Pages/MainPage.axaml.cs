using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.ViewModels;
using TQVaultAE.Views.Controls;

namespace TQVaultAE.Views.Pages;

public partial class MainPage : UserControl, IItemDragEventObserver
{
    // TEMP
    private bool _isDraggingActive = false;

    private readonly MainPageViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainPageViewModel();
        DataContext = _viewModel;

        if (!Design.IsDesignMode)
            Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);

        ToggleButtonVault.IsCheckedChanged += VaultCheckedChanged;
        ToggleButtonSearch.IsCheckedChanged += SearchCheckedChanged;
        ToggleButtonSettings.IsCheckedChanged += SettingsCheckedChanged;
        AboutIcon.PointerReleased += OpenAboutWindow;

        ToggleButtonVault.IsChecked = true;
    }

    private void OpenAboutWindow(object? sender, PointerReleasedEventArgs e)
    {
        if (e.InitialPressMouseButton == MouseButton.Left)
            MainPageViewModel.OpenAboutWindow();
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

    public void Notify(object sender, ItemDragEvent @event)
    {
        if (@event.Type is ItemDragEventType.End)
        {
            ItemDragVisualLayer.IsVisible = false;
            ItemDragVisualLayer.Children.Clear();
            _isDraggingActive = false;
            return;
        }

        if (@event.Type is ItemDragEventType.Start)
        {
            if (@event.Item is null)
                return;

            ItemDragPopup itemDragPopup = new(@event.Item)
            {
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                Name = "ItemDragPopup",
                Width = @event.Size.Width,
                Height = @event.Size.Height,
            };

            ItemDragVisualLayer.Children.Add(itemDragPopup);
            ItemDragVisualLayer.IsVisible = true;
            _isDraggingActive = true;
        }

        ItemDragPopup popup = (ItemDragPopup)ItemDragVisualLayer.Children[0];
        Point position = @event.Position;

        Canvas.SetLeft(popup, position.X);
        Canvas.SetTop(popup, position.Y);
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }

    private void UserControl_PointerMoved(object? sender, PointerEventArgs e)
    {
        Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new ItemDragEvent(ItemDragEventType.CursorUpdate)
        {
            Position = e.GetPosition(ItemDragVisualLayer),
        });
    }

    private void UserControl_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        // Temporary so dradding can end, without restarting the app
        if (_isDraggingActive && e.InitialPressMouseButton is MouseButton.Right)
            Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new ItemDragEvent(ItemDragEventType.End));
    }
}