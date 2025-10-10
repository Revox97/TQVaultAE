using System.Collections.Specialized;
using System.Windows;
using TQVaultAE.Models.Game;
using TQVaultAE.Models.UI;

namespace TQVaultAE.UI.Models
{
    internal class ItemsPanelModel(List<Item> items) : DependencyObject, INotifyCollectionChanged
    {
        private double _cellWidthHeight; 

        public SortDirection SortDirection { get; set; }

        // TODO make it bindable and update by using INotifyCollectionChanged
        public List<Item> Items { get; set; } = items;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}
