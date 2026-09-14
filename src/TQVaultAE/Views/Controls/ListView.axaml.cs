using System.Collections;
using Avalonia;
using Avalonia.Controls;

namespace TQVaultAE.Views.Controls
{
    public partial class ListView : UserControl
    {
        public static readonly StyledProperty<IEnumerable?> ItemsSourceProperty =
            AvaloniaProperty.Register<ListView, IEnumerable?>(nameof(ItemsSource));

        public IEnumerable? ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
                DrawListViewItems();
            }
        }

        public ListView()
        {
            InitializeComponent();
            //DrawListViewItems();
        }

        private void DrawListViewItems()
        {
            Grid container = ListView__DataContainer;
            container.Children.Clear();

            if (ItemsSource is null)
                return;

            foreach (object item in ItemsSource)
            {
                Label itemLabel = new()
                {
                    Content = item.ToString(),
                };

                itemLabel.Classes.Add("ListView__Item");
                container.Children.Add(itemLabel);
            }

        }
    }
}