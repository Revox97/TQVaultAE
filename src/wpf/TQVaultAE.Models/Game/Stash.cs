using System.Drawing;
using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    public class Stash
    {
        public StashType StashType { get; set; }

        public Size Size { get; set; }

        public List<Item> Items { get; set; } = [];

        public void AddItem(Item item, Point location)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            if (Items.Contains(item))
                throw new InvalidOperationException("Stash already contains the item.");

            item.Location = location;

            List<Item> overlappingItems = [.. Items.Where(i => i.IsLocationOverlap(item))];

            if (overlappingItems.Count > 0)
            {
                if (overlappingItems.Count > 1)
                    return;

                RemoveItem(overlappingItems[0]);
            }

            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            Items.Remove(item);
        }

        public void MoveItem(Stash source, Item item, Point location)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            Point oldLocation = item.Location;
            item.Location = location;

            List<Item> overlappingItems = [.. Items.Where(i => i.IsLocationOverlap(item))];

            if (overlappingItems.Count > 0)
            {
                if (overlappingItems.Count > 1)
                {
                    item.Location = oldLocation;
                    return;
                }

                RemoveItem(overlappingItems[0]);
            }
        }
    }
}
