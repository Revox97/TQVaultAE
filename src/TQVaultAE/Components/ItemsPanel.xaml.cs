using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models;
using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Components
{
    /// <summary>
    /// Interaction logic for ItemsPanel.xaml
    /// </summary>
    public partial class ItemsPanel : UserControl
    {
		private readonly double _cellWidthHeight;
		private readonly int _columns;
		private readonly int _rows;
		private readonly ItemOverController _itemOverController = ItemOverController.GetInstance();

		public List<Item> Items { get; set; } = [];

        public ItemsPanel(List<Item> items, double columnWidthHeight, int columns, int rows, Thickness borderThickness)
        {
            InitializeComponent();

			_cellWidthHeight = columnWidthHeight;
			_columns = columns;
			_rows = rows;
			InitializePanel();

			Size dimensions = CalculateDimensions(_cellWidthHeight, _columns, _rows, borderThickness);
			ItemsPanelBorder.Height = dimensions.Height;
			ItemsPanelBorder.Width = dimensions.Width;
			ItemsPanelBorder.BorderThickness = borderThickness;

			Items = items;
			LoadItems();
        }

		public void LoadItems()
		{
			ItemsPanelContent.Children.Clear();
			InitializeCells();

			foreach (Item item in Items)
			{
				try
				{
					if (!IsValidPlacement(item))
						throw new InvalidOperationException("Invalid item placement. Another item is already located in this position.");

					Image itemControl = new()
					{
						Source = item.Icon,
						Stretch = Stretch.Fill,
						VerticalAlignment = VerticalAlignment.Top,
						HorizontalAlignment = HorizontalAlignment.Left,
						DataContext = item
					};

					itemControl.MouseEnter += (s, e) => _itemOverController.Notify(itemControl, new ItemOverEventArgs() { ItemName = item.Name, Rarity = item.Rarity });
					itemControl.MouseLeave += (s, e) => _itemOverController.Notify(itemControl, new ItemOverEventArgs() { IsMouseOver = false });

					ItemsPanelContent.Children.Add(itemControl);
					Grid.SetColumn(itemControl, item.Location.X);
					Grid.SetColumnSpan(itemControl, item.Size.Width);
					Grid.SetRow(itemControl, item.Location.Y);
					Grid.SetRowSpan(itemControl, item.Size.Height);
				}
				catch(Exception ex)
				{
					// TODO add logging
				}
			}
		}

		private bool IsValidPlacement(Item item)
		{
			if (Items.Any(i => i.IsLocationOverlap(item)))
				return false;

			return !(item.Location.X < 0 || item.Location.X + item.Size.Width > _columns || item.Location.Y < 0 || item.Location.Y + item.Size.Height > _rows);
		}

		public static Size CalculateDimensions(double cellWidthHeight, int columns, int rows, Thickness borderThickness)
		{
			double height = (cellWidthHeight * rows) + borderThickness.Top + borderThickness.Bottom;
			double width = (cellWidthHeight * columns) + borderThickness.Left + borderThickness.Right;

			return new Size(width, height);
		}

		private void InitializePanel()
		{
			try
			{
				GridLength size = new(_cellWidthHeight);
				for (int i = 0; i< _columns; ++i)
					ItemsPanelContent.ColumnDefinitions.Add(new ColumnDefinition() { Width = size });

				for (int i = 0; i < _rows; ++i)
					ItemsPanelContent.RowDefinitions.Add(new RowDefinition() { Height = size });

				InitializeCells();
			}
			catch (Exception ex)
			{
				// TODO handle exception
			}
		}

		private void InitializeCells()
		{
			if (FindResource("ItemSlot") is Style style)
			{
				// TODO optimize this chunk of code
				for (int r = 0; r < ItemsPanelContent.RowDefinitions.Count; ++r)
				{
					for (int c = 0; c < ItemsPanelContent.ColumnDefinitions.Count; ++c)
					{
						Border border = new() { Style = style };

						ItemsPanelContent.Children.Add(border);
						Grid.SetRow(border, r);
						Grid.SetColumn(border, c);
					}
				}
			}
			else
			{
				throw new ResourceReferenceKeyNotFoundException("Failed to load style for item slot.", "ItemSlot");
			}
		}
    }
}
