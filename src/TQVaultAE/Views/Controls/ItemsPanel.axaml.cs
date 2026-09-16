using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Views.Controls;

public partial class ItemsPanel : UserControl, IItemDragEventObserver
{
    private bool _isitemDragActive = false;

    public static readonly StyledProperty<List<Item>> ItemsProperty =
        AvaloniaProperty.Register<ItemsPanel, List<Item>>(nameof(Items));

    public static readonly StyledProperty<int> RowsProperty =
        AvaloniaProperty.Register<ItemsPanel, int>(nameof(Rows));

    public static readonly StyledProperty<int> ColumnsProperty =
        AvaloniaProperty.Register<ItemsPanel, int>(nameof(Columns));

    public int Rows
    {
        get => GetValue(RowsProperty);
        set => SetValue(RowsProperty, value);
    }

    public int Columns
    {
        get => GetValue(ColumnsProperty);
        set => SetValue(ColumnsProperty, value);
    }

    internal int CellSize { get; set; }

    public List<Item> Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    static ItemsPanel()
    {
        ItemsProperty.Changed.AddClassHandler<ItemsPanel>((control, args) =>
        {
            if (control is ItemsPanel itemsPanel && args.NewValue is List<Item> newValue)
                control.DrawItems(newValue);
        });
    }

    public ItemsPanel()
    {
        InitializeComponent();
        InitializeUI();

        if (!Design.IsDesignMode)
            Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);
    }

    private void DrawItems(List<Item> items)
    {
        IEnumerable<Control> oldItems = ItemsContainer.Children.ToList().Where(x => x.GetType() == typeof(ItemControl));
        ItemsContainer.Children.RemoveAll(oldItems);

        foreach (Item item in items)
        {
            // TODO Should not be necessary anymore
            if (item.Position.X == -1 || item.Position.Y == -1)
                continue;

            ItemControl itemControl = new(item);

            ItemsContainer.Children.Add(itemControl);
            Grid.SetRow(itemControl, item.Position.Y);
            Grid.SetColumn(itemControl, item.Position.X);

            Grid.SetColumnSpan(itemControl, item.Size.Width);
            Grid.SetRowSpan(itemControl, item.Size.Height);
        }
    }

    internal void InitializeUI()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int k = 0; k < Columns; k++)
            {
                Border item = new()
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0x8e, 0x8c, 0x81)), // #8e8c81
                    BorderThickness = new Thickness(0, 0, 1, 1),
                    Background = new SolidColorBrush(Colors.Transparent),
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                };

                item.PointerPressed += Item_PointerPressed;
                ItemsContainer.Children.Add(item);

                Grid.SetRow(item, i);
                Grid.SetColumn(item, k);
            }
        }
    }

    private void Item_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (!_isitemDragActive)
            return;

        Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new ItemDragEvent(ItemDragEventType.End));
    }

    internal void UpdateUI()
    {
        ItemsContainer.RowDefinitions.Clear();
        ItemsContainer.ColumnDefinitions.Clear();

        for (int i = 0; i < Rows; i++)
            ItemsContainer.RowDefinitions.Add(new RowDefinition(CellSize, GridUnitType.Pixel));

        for (int i = 0; i < Columns; i++)
            ItemsContainer.ColumnDefinitions.Add(new ColumnDefinition(CellSize, GridUnitType.Pixel));
    }

    // TODO Handle cell update on cursor update
    public void Notify(object sender, ItemDragEvent @event)
    {
        if (@event.Type is ItemDragEventType.Start)
        {
            _isitemDragActive = true;
            return;
        }

        if (@event.Type is ItemDragEventType.End)
        {
            _isitemDragActive = false;
            // Handle item add, replace, or skip, if not in control
            return;
        }

        if (@event.Type is ItemDragEventType.CursorUpdate)
        {
            // TODO Check whether position is in bounds and set hover effect if so.
            // Green, if item can be placed
            // Red, if item overlaps with two or more other items
            if (Avalonia.Application.Current!.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                return;

            Point? controlPositionTopLeft = this.TranslatePoint(new Point(Bounds.Left, Bounds.Top), desktop.MainWindow!);
            Point? controlPositionBottomRight = this.TranslatePoint(new Point(Bounds.Right, Bounds.Bottom), desktop.MainWindow!);

            if (controlPositionTopLeft is null ||  controlPositionBottomRight is null)
                return;

            Point popupPosition = @event.Position;

            if (
                    popupPosition.X >= controlPositionTopLeft.Value.X
                 && popupPosition.X <= controlPositionBottomRight.Value.X
                 && popupPosition.Y >= controlPositionTopLeft.Value.Y
                 && popupPosition.Y <= controlPositionBottomRight.Value.Y
            )
            {
                Point? relativePosition = this.TranslatePoint(popupPosition, this);
                if (relativePosition is null)
                    return;

                // Highlight cells in green or red
            }

            return;
        }
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }
}