using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.Players;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class EquipmentControl : UserControl, IMainWindowChangedObserver
{
    private Popup? _popup;

    public EquipmentControlViewModel ViewModel { get; } = new();

    private int _cellSize;

    public static readonly StyledProperty<Equipment?> EquipmentProperty =
        AvaloniaProperty.Register<EquipmentControl, Equipment?>(nameof(Player));

    public Equipment? Equipment
    {
        get => GetValue(EquipmentProperty);
        set => SetValue(EquipmentProperty, value);
    }

    static EquipmentControl()
    {
        EquipmentProperty.Changed.AddClassHandler<EquipmentControl>((control, args) =>
        {
            if (control is EquipmentControl eControl && args.NewValue is Equipment newValue)
            {
                eControl.ViewModel.Equipment = newValue;
                eControl.DrawItems();
            }
        });
    }

    private void DrawItems()
    {
        // TODO Implement
    }

    public EquipmentControl()
    {
        InitializeComponent();

        if (!Design.IsDesignMode)
            Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);
    }

    public void Notify(object sender, MainWindowChangedEvent @event)
    {
        _cellSize = @event.CellSize;
        UpdateUI();
    }

    private void UpdateUI()
    {
        double totalWidth = (_cellSize * 6) + (_cellSize * 1.4) + (_cellSize * 0.8) + _cellSize;
        Width = totalWidth;
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }

    private void Grid_PointerEntered(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        if (sender is not Grid itemGrid || itemGrid.Tag is not Item item)
            return;

        itemGrid.Background = new SolidColorBrush(new Color(0x80, item.AccentColor.R, item.AccentColor.G, item.AccentColor.B));

        _popup = new()
        {
            Tag = item,
            Child = new ItemPopup(item),
            Placement = PlacementMode.RightEdgeAlignedTop,
            PlacementTarget = itemGrid,
        };

        _popup.Opened += Popup_Opened;
        _popup.Open();
    }

    // Required workaround, as Avalonia has no native tranparency support for popups.
    private void Popup_Opened(object? sender, System.EventArgs e)
    {
        if (sender is not Popup popup)
            return;

        TopLevel? topLevelElem = TopLevel.GetTopLevel(popup.Child);

        if (topLevelElem is null)
            return;

        topLevelElem.Background = Brushes.Transparent;
    }

    private void Grid_PointerExited(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        if (sender is not Grid itemGrid || itemGrid.Tag is not Item item)
            return;

        itemGrid.Background = new SolidColorBrush(new Color(item.AccentColor.A, item.AccentColor.R, item.AccentColor.G, item.AccentColor.B));
        _popup?.Close();
        _popup = null;
    }
}