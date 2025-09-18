using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;

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
