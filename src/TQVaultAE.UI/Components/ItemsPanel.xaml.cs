using System.Windows;
using System.Windows.Controls;
using TQVaultAE.Models.PlayerData;
using TQVaultAE.UI.Controllers;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Components
{
    /// <summary>
    /// Interaction logic for ItemsPanel.xaml
    /// </summary>
    public partial class ItemsPanel : UserControl
    {
		private readonly double _cellWidthHeight;
        public readonly int Columns;
        public readonly int Rows;

        private readonly ItemsPanelModel _model;
        private readonly ItemsPanelController _controller;

        public ItemsPanel(List<Item> items, double columnWidthHeight, int columns, int rows, Thickness borderThickness)
        {
            InitializeComponent();

			_cellWidthHeight = columnWidthHeight;
			Columns = columns;
			Rows = rows;
			InitializePanel();

			Size dimensions = CalculateDimensions(_cellWidthHeight, Columns, Rows, borderThickness);
			ItemsPanelBorder.Height = dimensions.Height;
			ItemsPanelBorder.Width = dimensions.Width;
			ItemsPanelBorder.BorderThickness = borderThickness;

            _model = new ItemsPanelModel(items);
            _controller = new ItemsPanelController(this, _model);
			LoadItems();
        }

        public void SwitchContent(List<Item> items) => _model.Items = items;

		public void LoadItems()
		{
			ItemsPanelContent.Children.Clear();
			InitializeCells();

            foreach (Item item in _model.Items)
            {
                try
                {
                    if (!IsValidPlacement(item))
                        throw new InvalidOperationException("Invalid item placement. Another item is already located in this position.");

                    CreateItem(item);
                }
                catch(Exception ex)
                {
                    // TODO add logging
                }
            }
		}

		private void CreateItem(Item item)
		{
			ItemControl itemControl = new(item);
            ItemsPanelContent.Children.Add(itemControl);
            Grid.SetColumn(itemControl, item.Location.X);
            Grid.SetColumnSpan(itemControl, item.Size.Width);
            Grid.SetRow(itemControl, item.Location.Y);
            Grid.SetRowSpan(itemControl, item.Size.Height);
		}

        private bool IsValidPlacement(Item item)
		{
            return !_model.Items.Any(i => i.IsLocationOverlap(item)) 
				&& !(item.Location.X < 0 || item.Location.X + item.Size.Width > Columns || item.Location.Y < 0 || item.Location.Y + item.Size.Height > Rows);
        }

        public static Size CalculateDimensions(double cellWidthHeight, int columns, int rows, Thickness borderThickness)
		{
			double height = cellWidthHeight * rows + borderThickness.Top + borderThickness.Bottom;
			double width = cellWidthHeight * columns + borderThickness.Left + borderThickness.Right;

			return new Size(width, height);
		}

		private void InitializePanel()
		{
			try
			{
				GridLength size = new(_cellWidthHeight);

				for (int i = 0; i< Columns; ++i)
				{
					BackgroundContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = size });
					ItemsPanelContent.ColumnDefinitions.Add(new ColumnDefinition() { Width = size });
				}

				for (int i = 0; i < Rows; ++i)
				{
					BackgroundContainer.RowDefinitions.Add(new RowDefinition() { Height = size });
					ItemsPanelContent.RowDefinitions.Add(new RowDefinition() { Height = size });
				}

				InitializeCells();
			}
			catch (Exception ex)
			{
				// TODO handle exception
			}
		}

		private void InitializeCells()
		{
			if (FindResource("ItemSlotBackground") is Style style)
			{
				// TODO optimize this chunk of code
				for (int r = 0; r < BackgroundContainer.RowDefinitions.Count; ++r)
				{
					for (int c = 0; c < BackgroundContainer.ColumnDefinitions.Count; ++c)
					{
						Border border = new() { Style = style };

						BackgroundContainer.Children.Add(border);
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

        internal void Sort() => _controller.Sort();
    }
}
