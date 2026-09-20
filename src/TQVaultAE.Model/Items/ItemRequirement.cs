using TQVaultAE.Localisation;
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

        public override string ToString()
        {
            string localisationTag = Type.GetLocalizationTagOrEnumValue();

            // TODO There is a base attack speed tag, that is no property, needs to be removed from item properties!
            if (localisationTag.EndsWith("Tag"))
                return string.Empty;

            string requirementName = new GameLocalizationService().GetLocalizedValueByTagAsync(localisationTag).Result ?? string.Empty;

            if (requirementName == string.Empty)
                return string.Empty;

            return $"Required {requirementName}: {Value}"; // TODO Localize
        }
    }
}
