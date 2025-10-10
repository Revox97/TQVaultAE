using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Game;
using TQVaultAE.Services;
using TQVaultAE.UI.Controllers;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Components
{
    /// <summary>
    /// Interaction logic for ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        private readonly ItemControlController _controller;
        private readonly ItemControlModel _model;

        private readonly DragController _dragController;

        // TODO Remove dependency property use NotifyPropertyChanged
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(nameof(DataSource), typeof(ItemControlModel), typeof(ItemControl));
        private LinearGradientBrush _normalBrush = null!;
        private LinearGradientBrush _hoverBrush = null!;

        public ItemControlModel DataSource
        {
            get => (ItemControlModel)GetValue(DataSourceProperty);
            set => SetValue(DataSourceProperty, value);
        }

        public ItemControl(Item item)
        {
            InitializeComponent();

            _model = new(item);
            DataContext = _model;
            _controller = new ItemControlController(this, _model);

            // TODO Remove this
            DataSource = _model;

            _dragController = DragController.GetInstance();
            _dragController.ItemDraggedChanged += (s, e) => UserControl_MouseLeave(this, null!);

            CalculateBrushes((SolidColorBrush)item.Color);
            DrawRarityHighlight();
            DrawItem();
        }

        private void DrawRarityHighlight()
        {
            HighlightColorPanel.Fill = _normalBrush;
        }

        private void CalculateBrushes(SolidColorBrush itemBrush)
        {
            // TODO Might not be required anymore due to hover color property
            Color normal = Color.FromArgb(50, itemBrush.Color.R, itemBrush.Color.G, itemBrush.Color.B);
            Color highlight = Color.FromArgb(240, itemBrush.Color.R, itemBrush.Color.G, itemBrush.Color.B);

            GradientStopCollection normalGradientStops = new([
                new GradientStop(highlight, 0.0),
                new GradientStop(normal, 0.06),
                new GradientStop(normal, 0.94),
                new GradientStop(highlight, 1.0),
            ]);

            _normalBrush = new LinearGradientBrush(normalGradientStops, new Point(0, 1), new Point(1, 0));

            Color hoverNormal = Color.FromArgb(100, itemBrush.Color.R, itemBrush.Color.G, itemBrush.Color.B);
            Color hoverHighlight = Color.FromArgb(250, itemBrush.Color.R, itemBrush.Color.G, itemBrush.Color.B);

            GradientStopCollection hoverGradientStops = new([
                new GradientStop(hoverHighlight, 0.0),
                new GradientStop(hoverNormal, 0.06),
                new GradientStop(hoverNormal, 0.94),
                new GradientStop(hoverHighlight, 1.0),
            ]);

            _hoverBrush = new LinearGradientBrush(hoverGradientStops, new Point(0, 1), new Point(1, 0));
        }

        private void DrawItem()
        {
            ItemSlotContent.Source = DataSource.Item.Icon;
        }

        public void AddHighlight() => UserControl_MouseEnter(null!, null!);
        public void RemoveHighlight() => UserControl_MouseLeave(null!, null!);

        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_dragController.IsItemDragged)
            {
                HighlightColorPanel.Fill = _hoverBrush;
                _controller.ShowItemDetailsWindow();
                ItemHoverService.GetInstance().Notify(this, new ItemOverEventArgs() { ItemName = DataSource.Item.Name }); // , Rarity = DataSource.Item.Rarity
            }
        }

        private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HighlightColorPanel.Fill = _normalBrush;
            _controller.CloseItemDetailsWindow();
            ItemHoverService.GetInstance().Notify(this, new ItemOverEventArgs() { IsMouseOver = false });
        }

        private void UserControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _controller.ShowItemDragPopup();
        }
    }
}
