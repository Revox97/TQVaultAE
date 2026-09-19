using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Model.Items;
using TQVaultAE.Views.Pages;
using TQVaultAE.Views.Windows;

namespace TQVaultAE.Views.Controls;

public partial class ItemControl : UserControl
{
    // TODO Move into view model
    private Popup? _popup;

    public Item Item { get; init; }

    // Needed for XAML Designer
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public ItemControl()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        InitializeComponent();
        DataContext = new Item();
    }

    public ItemControl(Item item)
    {
        InitializeComponent();
        Item = item;
        DataContext = item;
    }

    private void UserControl_PointerEntered(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        ItemAccent.Fill = new SolidColorBrush(new Color(0x80, Item.AccentColor.R, Item.AccentColor.G, Item.AccentColor.B));

        _popup = new()
        {
            Tag = this,
            Child = new ItemPopup(Item),
            Placement = PlacementMode.RightEdgeAlignedTop,
            PlacementTarget = this,
        };

        _popup.Opened += Popup_Opened;
        _popup.Open();
    }

    // Required workaround, as Avalonia has no native tranparency support for popups.
    private void Popup_Opened(object? sender, EventArgs e)
    {
        if (sender is not Popup popup)
            return;

        if (TopLevel.GetTopLevel(popup.Child) is TopLevel topLevelElem)
            topLevelElem.Background = Brushes.Transparent;
    }

    private void UserControl_PointerExited(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        ItemAccent.Fill = new SolidColorBrush(new Color(Item.AccentColor.A, Item.AccentColor.R, Item.AccentColor.G, Item.AccentColor.B));
        _popup?.Close();
        _popup = null;
    }

    public void DeleteCommand()
    {
        throw new NotImplementedException();
    }

    public void CopyCommand()
    {
        throw new NotImplementedException();
    }

    public void DuplicateCommand()
    {
        throw new NotImplementedException();
    }

    public void PropertiesCommand()
    {
        throw new NotImplementedException();
    }

    private void UserControl_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        if (Avalonia.Application.Current!.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop || sender is not ItemControl control)
            return;

        Window? mainWindow = desktop.MainWindow;
        if (mainWindow is null)
            return;

        // TODO clean up (and find better solution) - this is ugly as fuck
        Canvas visualLayer = ((MainPage)((TQWindow)mainWindow).ContentContainer.Children[0]).ItemDragVisualLayer;

        Point? controlPosition = this.TranslatePoint(new Point(0, 0), visualLayer);

        if (controlPosition is null)
            return;

        Point mouseOffset = e.GetPosition(control);

        Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new ItemDragEvent(ItemDragEventType.Start)
        {
            Item = Item,
            Position = controlPosition.Value,
            Size = new Size(control.Bounds.Width, control.Bounds.Height),
            MouseOffset = mouseOffset,
        });

        e.Handled = true;
    }
}