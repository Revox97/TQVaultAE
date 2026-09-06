using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Media;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Views.Controls;

public partial class ItemsPanel : UserControl
{
    internal int Rows { get; set; } = 20;
    internal int Columns { get; set; } = 18;

    internal int CellSize { get; set; }

    public List<ItemBase> Items { get; set; } = [
        new ArtifactItem()
        {
            Name = "Sample item 1",
            ItemLevel = 4,
            Position = new System.Drawing.Point(2, 3),
            Size = new System.Drawing.Size(2,2)
        },
        new ArtifactItem()
        {
            Name = "Sample item 2",
            ItemLevel = 4,
            Position = new System.Drawing.Point(7, 8),
            Size = new System.Drawing.Size(2,2)
        },
    ];

    public ItemsPanel()
    {
        InitializeComponent();
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

        foreach(ItemBase item in Items)
        {
            ItemControl itemControl = new(item);

            ItemsContainer.Children.Add(itemControl);
            Grid.SetRow(itemControl, item.Position.Y);
            Grid.SetColumn(itemControl, item.Position.X);
            Grid.SetColumnSpan(itemControl, item.Size.Width);
            Grid.SetRowSpan(itemControl, item.Size.Height);
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