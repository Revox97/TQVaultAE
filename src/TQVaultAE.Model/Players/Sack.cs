using TQVaultAE.Model.Items;

namespace TQVaultAE.Model.Players
{
    /// <summary>
    /// Represents a <see cref="Sack"/> of a <see cref="Player"/>.
    /// </summary>
    public sealed class Sack
    {
        /// <summary>
        /// Gets the number of the <see cref="Sack"/>.
        /// </summary>
        public int Number {  get; init; }

        /// <summary>
        /// Gets or sets a list of <see cref="Item"/>s within the <see cref="Sack"/>.
        /// </summary>
        public List<Item> Items { get; set; } = [];

        /// <summary>
        /// Gets the amount of items in the <see cref="Sack"/>.
        /// </summary>
        public int ItemCount => Items.Count;
    }
}
