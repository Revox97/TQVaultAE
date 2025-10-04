using System.Windows;
using TQVaultAE.Models.Game;

namespace TQVaultAE.UI.Models
{
    public class ItemControlModel : DependencyObject
    {
        // TODO Get rid of dependency property
        internal static DependencyProperty ItemProperty = DependencyProperty.Register(nameof(Item), typeof(Item), typeof(ItemControlModel));

        public Item Item
        {
            get => (Item)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        // Design time constructor
        public ItemControlModel()
        {

        }

        public ItemControlModel(Item item)
        {
            Item = item;
        }
    }
}
