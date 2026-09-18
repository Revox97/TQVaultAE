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
using TQVaultAE.Views.Pages;
using TQVaultAE.Views.Windows;

namespace TQVaultAE.Views.Controls;

public partial class ItemsPanel : UserControl, IItemDragEventObserver, IMainWindowChangedObserver
{
    private bool _isitemDragActive = false;
    private int _cellSize;

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
        InitializeGrid();

        if (!Design.IsDesignMode)
            Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);
    }

    private void DrawItems(List<Item> items)
    {
        IEnumerable<Control> oldItems = ItemsContainer.Children.ToList().Where(x => x.GetType() == typeof(ItemControl));
        ItemsContainer.Children.RemoveAll(oldItems);

        foreach (Item item in items)
        {
            ItemControl itemControl = new(item);

            ItemsContainer.Children.Add(itemControl);
            Grid.SetRow(itemControl, item.Position.Y);
            Grid.SetColumn(itemControl, item.Position.X);

            Grid.SetColumnSpan(itemControl, item.Size.Width);
            Grid.SetRowSpan(itemControl, item.Size.Height);
        }
    }

    internal void InitializeGrid()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int k = 0; k < Columns; k++)
            {
                Border item = new()
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0x8e, 0x8c, 0x81)), // #8e8c81
                    BorderThickness = new Thickness(0, 0, 1, 1),
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                    Background = new SolidColorBrush(Colors.Transparent)
                };

                item.PointerPressed += Item_PointerPressed;
                ItemsContainer.Children.Add(item);

                Grid.SetRow(item, i);
                Grid.SetColumn(item, k);
            }
        }
    }

    internal void UpdateGrid(int highlightX = -1, int highlightY = -1, int highligthWidth = 0, int highlightHeight = 0)
    {
        try
        {
            foreach (Border cellItem in ItemsContainer.Children.Where(x => x.GetType() == typeof(Border)).Cast<Border>())
            {
                int row = Grid.GetRow(cellItem);
                int column = Grid.GetColumn(cellItem);

                bool isColumnInHighlight = column >= highlightX && column < highlightX + highligthWidth;
                bool isRowInHighlight = row >= highlightY && row < highlightY + highlightHeight;

                if (isColumnInHighlight && isRowInHighlight)
                    cellItem.Background = new SolidColorBrush(Colors.Green);
                else
                    cellItem.Background = new SolidColorBrush(Colors.Transparent);
            }
        }
        catch(Exception ex)
        {

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

    public void Notify(object sender, MainWindowChangedEvent @event) => _cellSize = @event.CellSize;

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
            UpdateGrid();
            // Handle item add, replace, or skip, if not in control
            return;
        }

        // TODO refactor - Quite a mess currently, but hey it works^^
        if (@event.Type is ItemDragEventType.CursorUpdate)
        {
            try
            {
                if (Avalonia.Application.Current!.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
                    || desktop.MainWindow is not TQWindow window
                    || window.ContentContainer.Children[0] is not MainPage mainPage
                   ) return;

                if (mainPage.ItemDragVisualLayer.TranslatePoint(@event.Position, this) is not Point popupPosition)
                    return;

                Point mousePosition = popupPosition + @event.MouseOffset;

                if (mousePosition.X >= 0 && mousePosition.X <= Bounds.Width && mousePosition.Y >= 0 && mousePosition.Y <= Bounds.Height)
                {
                    int cellColumn = (int)Math.Round(popupPosition.X / _cellSize, MidpointRounding.AwayFromZero);
                    int cellRow = (int)Math.Round(popupPosition.Y / _cellSize, MidpointRounding.AwayFromZero);
                    int cellWidth = (int)@event.Size.Width / _cellSize;
                    int cellHeight = (int)@event.Size.Height / _cellSize;

                    if (cellRow < 0)
                        cellRow = 0;

                    if (cellColumn < 0)
                        cellColumn = 0;
                    
                    if (cellRow + cellHeight > Rows)
                        cellRow = Rows - cellHeight;

                    if (cellColumn + cellWidth > Columns)
                        cellColumn = Columns - cellWidth;

                    UpdateGrid(cellColumn, cellRow, cellWidth, cellHeight);
                    return;
                }

                UpdateGrid();
            }
            catch(Exception ex)
            {

            }
        }
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }
}