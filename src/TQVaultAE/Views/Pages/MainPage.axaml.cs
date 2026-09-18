using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
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
    private ItemDragPopup? _itemDragPopup;
    private Point? _mouseOffset;
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
        if (@event.Type is ItemDragEventType.Cancel or ItemDragEventType.Complete)
        {
            // TODO Have a bindable property to handle the visibility
            ItemDragVisualLayer.IsVisible = false;
            ItemDragVisualLayer.Children.Clear();
            _itemDragPopup = null;
            _isDraggingActive = false;
            return;
        }

        if (@event.Type is ItemDragEventType.Start)
        {
            if (@event.Item is null)
                return;

            _mouseOffset = @event.MouseOffset;
            Point position = @event.Position - @event.MouseOffset;

            _itemDragPopup = new(@event.Item)
            {
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                Name = "ItemDragPopup",
                Width = @event.Size.Width,
                Height = @event.Size.Height,
            };

            ItemDragVisualLayer.Children.Add(_itemDragPopup);
            Canvas.SetLeft(_itemDragPopup, position.X);
            Canvas.SetTop(_itemDragPopup, position.Y);

            ItemDragVisualLayer.IsVisible = true;
            _isDraggingActive = true;
        }
    }

    private void UserControl_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDraggingActive || _itemDragPopup is null || _mouseOffset is null)
            return;

        Point pointerPosition = (Point)(e.GetPosition(ItemDragVisualLayer) - _mouseOffset);
        double x = Math.Clamp(pointerPosition.X, 0, ItemDragVisualLayer.Bounds.Width - _itemDragPopup.Bounds.Width);
        double y = Math.Clamp(pointerPosition.Y, 0, ItemDragVisualLayer.Bounds.Height - _itemDragPopup.Bounds.Height);

        Canvas.SetLeft(_itemDragPopup, x);
        Canvas.SetTop(_itemDragPopup, y);

        Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new ItemDragEvent(ItemDragEventType.CursorUpdate)
        {
            Position = new Point(x, y)
        });
    }

    private void ContentControl_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property.Name != "Content")
            return;

        ItemDragVisualLayer.IsVisible = _isDraggingActive && e.NewValue is VaultPage;
    }

    private void Root_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_isDraggingActive && e.InitialPressMouseButton == MouseButton.Right)
            Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new ItemDragEvent(ItemDragEventType.Cancel));
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }
}