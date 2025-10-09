using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Game;
using TQVaultAE.Models.Services;
using TQVaultAE.Models.Services.Observers;
using TQVaultAE.UI.Controllers;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Components
{
    /// <summary>
    /// Interaction logic for ItemsPanel.xaml
    /// </summary>
    public partial class ItemsPanel : UserControl, IContentScaleObserver
    {
		private double _cellWidthHeight;
        //public readonly int Columns;
        //public readonly int Rows;

        private readonly ItemsPanelModel _model;
        private readonly ItemsPanelController _controller;

        internal static DependencyProperty ColumnsProperty = DependencyProperty.Register(nameof(Columns), typeof(int), typeof(ItemsPanelModel));
        public int Columns
        {
            get => (int)GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        internal static DependencyProperty RowsProperty = DependencyProperty.Register(nameof(Rows), typeof(int), typeof(ItemsPanelModel));
        public int Rows
        {
            get => (int)GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        public ItemsPanel()
        {
            InitializeComponent();

            _model = new ItemsPanelModel([]);
            _controller = new ItemsPanelController(this, _model);
            ContentScaleService.GetInstance().AddObserver(this);
            DragController.GetInstance().ItemMoved += HandleItemDrag;
        }

        public void Notify(object sender, ContentScaleUpdatedEventArgs args)
        {
            _cellWidthHeight = args.General.ItemCellDimensions.Width;
			InitializePanel();

			Size dimensions = CalculateDimensions(_cellWidthHeight, Columns, Rows, new Thickness(2, 0, 2, 2));
			ItemsPanelBorder.Height = dimensions.Height;
			ItemsPanelBorder.Width = dimensions.Width;
            ItemsPanelBorder.BorderThickness = new Thickness(2, 0, 2, 2); // TODO Set this via dependencyProperty
            Height = dimensions.Height;
			LoadItems();
        }

        // TODO Remove once replaced in all locations
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
            //DragController.GetInstance().ItemMoved += HandleItemDrag;
			LoadItems();
        }

        // TODO use all edges to calculate not just topleft (popup)
        private void HandleItemDrag(object source, ItemMovedEventArgs args)
        {
            double itemPositionX = args.CurrentLocation.X;
            double itemPositionY = args.CurrentLocation.Y;

            UpdateLayout();
            Point topLeft = PointToScreen(new Point(0, 0));

            if (itemPositionX >= topLeft.X && itemPositionX <= topLeft.X + ActualWidth
             && itemPositionY >= topLeft.Y && itemPositionY <= topLeft.Y + ActualHeight)
            {
                foreach (object? item in BackgroundContainer.Children)
                {
                    if (item is Border border)
                        border.Background = new SolidColorBrush(Colors.Green);
                }
            }
            else
            {
                foreach (object? item in BackgroundContainer.Children)
                {
                    if (item is Border border)
                        border.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#2E291F");
                }
            }
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

        public void AddObserver(IContentScaleObserver observer)
        {
            throw new NotImplementedException();
        }

        public void RemoveObserver(IContentScaleObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            ContentScaleService.GetInstance().RemoveObserver(this);
            GC.SuppressFinalize(this);
        }

        public void Notify(object sender, WindowSizeUpdatedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
