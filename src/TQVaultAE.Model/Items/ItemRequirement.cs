using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents an requirement for an <see cref="Item"/> to be equiped.
    /// </summary>
    public class ItemRequirement(ItemRequirementType type, int value)
    {
        /// <summary>
        /// Gets or sets the <see cref="ItemRequirement"/> type.
        /// </summary>
        public ItemRequirementType Type { get; set; } = type;

        /// <summary>
        /// Gets or sets the value attached to the <see cref="ItemRequirement"/>.
        /// </summary>
        public int Value { get; set; } = value;
    }
}
