using System.Windows;
using TQVaultAE.Models.Game;

namespace TQVaultAE.UI.Models
{
    public class ItemControlModel : DependencyObject
    {
        internal static DependencyProperty ItemProperty = DependencyProperty.Register(nameof(Item), typeof(Item), typeof(ItemControlModel));

        public Item Item
        {
            get => (Item)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        public ItemControlModel()
        {

        }

        public ItemControlModel(Item item)
        {
            Item = item;
        }
    }
}
