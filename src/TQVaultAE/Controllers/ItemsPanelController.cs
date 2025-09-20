using System.Windows.Controls;
using TQVaultAE.Components;
using TQVaultAE.Models;
using TQVaultAE.Models.Game;

namespace TQVaultAE.Controllers
{
    internal class ItemsPanelController(ItemsPanel instance, ItemsPanelModel model)
    {
        private readonly ItemsPanel _instance = instance;
        private readonly ItemsPanelModel _model = model;

        // TODO Make sorting configurable to have user preferences
        internal void Sort()
        {
            List<Item> orderedItems = OrderItemSource();
            _instance.ItemsPanelContent.Children.Clear();
            bool[][] slots = new bool[_instance.Rows][];

            for (int i = 0; i < _instance.Columns; ++i)
                slots[i] = new bool[_instance.Columns];

            // TODO Gosh this is ugly, refactor
            if (_model.SortDirection == Models.UI.SortDirection.Horizontal)
            {
                orderedItems.ForEach(item =>
                {
                    int width = item.Size.Width;
                    int height = item.Size.Height;

                    for (int r = 0; r < slots.Length; ++r)
                    {
                        bool isValid = true;
                        for (int c = 0; c < slots[r].Length; ++c)
                        {
                            for (int h = 0; h < height; ++h)
                            {
                                if (r + h > slots.Length - 1)
                                {
                                    isValid = false;
                                    break;
                                }

                                for (int w = 0; w < width; ++w)
                                {
                                    if (c + w > slots[r].Length - 1)
                                    {
                                        isValid = false;
                                        break;
                                    }

                                    if (slots[r + h][c + w])
                                    {
                                        isValid = false;
                                        break;
                                    }
                                }

                                if (!isValid)
                                    break;
                            }

                            if (isValid)
                            {
                                item.Location = new System.Drawing.Point(r, c);
                                
                                for (int h = 0; h < height; ++h)
                                {
                                    for (int w = 0; w < width; ++w)
                                        slots[r + h][c + w] = true;
                                }

                                break;
                            }
                        }

                        if (isValid)
                            break;
                    }
                });

                _model.Items = orderedItems;
                _instance.LoadItems();
                _model.SortDirection = Models.UI.SortDirection.Vertical;
                return;
            }

            _model.SortDirection = Models.UI.SortDirection.Horizontal;
        }

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
