using System.Drawing;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents a Titan Quest item.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the position of the <see cref="Item"/> in its container.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Gets the path within the Titan Quest database.
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the seed of the <see cref="Item"/>.
        /// </summary>
        public int Seed { get; set; }

        /// <summary>
        /// Gets or sets the prefix of the <see cref="Item"/>.
        /// </summary>
        public Affix? Prefix { get; set; } = null;

        /// <summary>
        /// Gets or sets the suffix of the <see cref="Item"/>.
        /// </summary>
        public Affix? Suffix { get; set; } = null;

        /// <summary>
        /// Gets or sets the first relic of the <see cref="Item"/>.
        /// </summary>
        public RelicItem? RelicOne { get; set; }

        /// <summary>
        /// Gets or sets the second relic of the <see cref="Item"/>. 
        /// </summary>
        public RelicItem? RelicTwo { get; set; }

        // TODO Currently no clue what these are used for.
        public int Var1 { get; set; }
        public int Var2 { get; set; }
    }
}
