using System.ComponentModel.DataAnnotations.Schema;
using TQVaultAE.Model.Items.ItemProperties;

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

        internal List<string> GetAffixDescriptionProperties()
        {
            List<string> propertyValues = [];

            foreach (ItemProperty property in Properties)
                propertyValues.Add(property.ToString());

            return propertyValues;
        }
    }
}
