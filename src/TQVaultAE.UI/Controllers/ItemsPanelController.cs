using TQVaultAE.Models.Game;
using TQVaultAE.Models.UI;
using TQVaultAE.UI.Components;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Controllers
{
    internal class ItemsPanelController(ItemsPanel instance, ItemsPanelModel model)
    {
        private readonly ItemsPanel _instance = instance;
        private readonly ItemsPanelModel _model = model;

        // TODO Make sorting configurable to have user preferences
        internal void Sort()
        {
            List<Item> orderedItems = OrderItemSource();

            if (_model.SortDirection == SortDirection.Horizontal)
            {
                HorizontalSort(orderedItems, GenerateSortMap());
                _instance.LoadItems();
                return;
            }

            _model.SortDirection = SortDirection.Horizontal;
        }

        private bool[] GenerateSortMap()
        {
            return new bool[_instance.Rows * _instance.Columns];
        }

        private void HorizontalSort(List<Item> orderedItems, bool[] sortMap)
        {
            // TODO Gosh this is ugly and does not work, refactor, maybe consider a sorting service
            orderedItems.ForEach(item =>
            {
                int width = item.Size.Width;
                int height = item.Size.Height;
                bool isValid = true;

                for (int m = 0; m < sortMap.Length; ++m)
                {
                    for (int h = 0; h < height; ++h)
                    {
                        for (int w = 0; w < width; ++w)
                        {
                            if (m + h * _instance.Columns + w >= sortMap.Length)
                            {
                                isValid = false;
                                break;
                            }

                            if (sortMap[m + h * _instance.Columns + w])
                            {
                                isValid = false;
                                break;
                            }
                        }
                    }

                    if (isValid)
                    {
                        for (int h = 0; h < height; ++h)
                        {
                            for (int w = 0; w < width; ++w)
                                sortMap[m + h * _instance.Columns + w] = true;
                        }

                        item.Location = new System.Drawing.Point(m % _instance.Columns, m / _instance.Columns);
                        break;
                    }
                }
            });

            _model.Items = orderedItems;
            _instance.LoadItems();
            _model.SortDirection = SortDirection.Vertical;
            return;
        }

        private void VerticalSort() { }

        private List<Item> OrderItemSource()
        {
            List<Item> sortedItems = [];

            foreach (Item item in _model.Items)
            {
                int insertionIndex = 0;

                for (int i = 0; i < sortedItems.Count; i++)
                {
                    if (sortedItems[i].Size.Height > item.Size.Height)
                    {
                        insertionIndex++;
                        continue;
                    }

                    if (item.Name.CompareTo(sortedItems[i].Name) <= 0)
                    {
                        insertionIndex = i;
                        break;
                    }
                    else
                    {
                        insertionIndex++;
                    }
                }

                sortedItems.Insert(insertionIndex, item);
            }

            return sortedItems;
        }
    }
}
