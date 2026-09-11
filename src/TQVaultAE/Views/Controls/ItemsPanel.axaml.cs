using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Views.Controls;

public partial class ItemsPanel : UserControl
{
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
    }

    private void DrawItems(List<Item> items)
    {
        IEnumerable<Control> oldItems = ItemsContainer.Children.ToList().Where(x => x.GetType() == typeof(ItemControl));
        ItemsContainer.Children.RemoveAll(oldItems);

        foreach (Item item in items)
        {
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
                    BorderThickness = new Avalonia.Thickness(0, 0, 1, 1),
                    Background = new SolidColorBrush(Colors.Transparent),
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
                };

                ItemsContainer.Children.Add(item);

                Grid.SetRow(item, i);
                Grid.SetColumn(item, k);
            }
        }
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
}