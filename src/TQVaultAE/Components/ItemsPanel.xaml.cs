using System.Windows;
using System.Windows.Controls;

namespace TQVaultAE.Components
{
    /// <summary>
    /// Interaction logic for ItemsPanel.xaml
    /// </summary>
    public partial class ItemsPanel : UserControl
    {
		private readonly int _width;
		private readonly int _height;

        public ItemsPanel(int width, int height)
        {
            InitializeComponent();

			_width = width;
			_height = height;
			InitializePanel();

			// Set Items
        }

		private void InitializePanel()
		{
			try
			{
				for (int i = 0; i < _width; ++i)
					ItemsPanelContent.RowDefinitions.Add(new RowDefinition());

				for (int i = 0; i< _height; ++i)
					ItemsPanelContent.ColumnDefinitions.Add(new ColumnDefinition());

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
