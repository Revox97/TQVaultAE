using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents a property of an <see cref="Item"/>.
    /// </summary>
    public class ItemProperty
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        public ItemPropertyType Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the <see cref="ItemProperty"/>.
        /// </summary>
        public float Value { get; set; }

        public float GetValueBySeed(int seed)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"{Type} - {Value}";
    }
}
