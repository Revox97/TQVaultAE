using Avalonia.Controls;
using Avalonia.Media;

namespace TQVaultAE.Views.Controls;

public partial class ItemsPanel : UserControl
{
    internal int Rows { get; set; } = 20;
    internal int Columns { get; set; } = 18;

    internal int CellSize { get; set; }

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

        // TODO DUMMY, REMOVE
        ItemControl dummyItem = new(new()
        {
            Name = "Sample item name",
            ItemLevel = 4,
        });

        ItemsContainer.Children.Add(dummyItem);
        Grid.SetRow(dummyItem, 2);
        Grid.SetColumn(dummyItem, 4);
        Grid.SetColumnSpan(dummyItem, 2);
        Grid.SetRowSpan(dummyItem, 2);
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