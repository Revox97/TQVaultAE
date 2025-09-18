using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.Models;

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
		private readonly List<Item> _items = [];

        public ItemsPanel(double columnWidthHeight, int columns, int rows, Thickness borderThickness)
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

			// Set Items
			try
			{
				_items.Add(new Item("Name1", new System.Drawing.Point(0, 0), new System.Drawing.Size(2,2), new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/inventorybagup01.png"))); // TEMP
				_items.Add(new Item("Name1", new System.Drawing.Point(2, 2), new System.Drawing.Size(2,2), new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/inventorybagup01.png"))); // TEMP
				_items.Add(new Item("Name1", new System.Drawing.Point(1, 7), new System.Drawing.Size(2,2), new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/inventorybagup01.png"))); // TEMP
			} catch { }

			AddItems();
        }

		private void AddItems()
		{
			foreach (Item item in _items)
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
						DataContext = item,
					};

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
			if (_items.Any(i => i.IsLocationOverlap(item)))
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
