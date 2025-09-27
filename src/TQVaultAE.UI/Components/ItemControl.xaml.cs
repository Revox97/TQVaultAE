using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.PlayerData;
using TQVaultAE.Models.Services;
using TQVaultAE.Services;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Components
{
    /// <summary>
    /// Interaction logic for ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(nameof(DataSource), typeof(ItemControlModel), typeof(ItemControl));
        private LinearGradientBrush _normalBrush = null!;
        private LinearGradientBrush _hoverBrush = null!;

        public ItemControlModel DataSource
        {
            get => (ItemControlModel)GetValue(DataSourceProperty);
            set => SetValue(DataSourceProperty, value);
        }

        public ItemControl()
        {
            InitializeComponent();

            Item item = new ItemBuilder().Build();
            DataSource = new ItemControlModel(item);
            CalculateBrushes(Item.GetBrushByRarity(item.Rarity));
            DrawRarityHighlight();
            DrawItem();

        }
        public ItemControl(Item item)
        {
            InitializeComponent();

            DataSource = new ItemControlModel(item);
            CalculateBrushes(Item.GetBrushByRarity(item.Rarity));
            DrawRarityHighlight();
            DrawItem();
        }

        private void DrawRarityHighlight()
        {
            HighlightColorPanel.Fill = _normalBrush;
        }

        private void CalculateBrushes(SolidColorBrush itemBrush)
        {
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
            HighlightColorPanel.Fill = _hoverBrush;
            ItemHoverService.GetInstance().Notify(this, new ItemOverEventArgs() { ItemName = DataSource.Item.Name, Rarity = DataSource.Item.Rarity });
        }

        private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HighlightColorPanel.Fill = _normalBrush;
            ItemHoverService.GetInstance().Notify(this, new ItemOverEventArgs() { IsMouseOver = false });
        } 
    }
}
