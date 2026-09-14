using System.Collections.ObjectModel;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents an <see cref="Affix"/> which can be attached to an <see cref="Item"/>.
    /// </summary>
    public class Affix
    {
        /// <summary>
        /// Gets the path of the <see cref="Affix"/> within the Titan Quest database.
        /// </summary>
        public string Path { get; init; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public IEnumerable<ItemProperty> Properties { get; set; } = [];

        public IEnumerable<ItemRequirement> Requirements { get; set; } = [];

        public float MarketAdjustmentPercent { get; set; }

        public string Format { get; set; } = string.Empty;
    }
}
