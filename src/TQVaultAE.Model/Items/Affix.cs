using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public string Value { get; set; } = string.Empty;

        [NotMapped]
        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public IEnumerable<ItemProperty> Properties { get; set; } = [];

        [NotMapped]
        public IEnumerable<ItemRequirement> Requirements { get; set; } = [];

        [NotMapped]
        public float MarketAdjustmentPercent { get; set; }

        [NotMapped]
        public string Format { get; set; } = string.Empty;
    }
}
