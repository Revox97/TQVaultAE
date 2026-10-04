namespace TQVaultAE.Model.Items.ItemProperties
{
    /// <summary>
    /// Represents a normal (one line) <see cref="ItemProperty"/>, of a <see cref="Item"/>.
    /// </summary>
    public class NormalItemProperty : ItemProperty
    {
        /// <summary>
        /// Gets or sets the minimum value of the <see cref="ItemProperty"/>.
        /// </summary>
        public float Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum value of the <see cref="ItemProperty"/>.
        /// </summary>
        public float Max { get; set; }

        /// <summary>
        /// Gets or sets the duration of the <see cref="ItemProperty"/>.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// Gets or sets an additional modifier of the <see cref="ItemProperty"/>.
        /// </summary>
        public float DurationModifier { get; set; }

        /// <summary>
        /// Flags, whether the <see cref="ItemProperty"/> is a child of a <see cref="GlobalItemProperty"/>.
        /// </summary>
        public bool IsGlobal { get; set; }

        // TODO What is this parameter?
        /// <summary>
        /// ??
        /// </summary>
        public bool XOR { get; set; }

        /// <summary>
        /// Gets a string, that represents the value of the property, the same way, as whithin the game.
        /// </summary>
        /// <returns>A <see cref="string"/>, that represents the <see cref="ItemProperty"/>.</returns>
        public override string ToString()
        {
            string result = string.Empty;

            if (IsGlobal)
                result += "\t";

            if (Chance > 0.0f)
            {
                string chanceFormat = GetLocalizedValue($"ChanceOfTag");
                result += ReplaceSingleValueInString(chanceFormat, Chance);
            }

            string propertyValue = GetLocalizedValue(Name);

            if (Max > 0.0f)
            {
                string rangeFormat = GetLocalizedValue("DamageRangeFormat");
                string range = ReplaceRangeOfValue(rangeFormat, [Min, Max]);
                result += $"{range}{propertyValue}";
            }
            else
            {
                // TODO: Create a cleaner solution for the condition
                result += propertyValue.StartsWith("{%") || propertyValue.StartsWith("-{%")
                    ? ReplaceSingleValueInString(propertyValue, Min)
                    : $"{Min}{propertyValue}";
            }

            if (Duration > 0)
            {
                string durationFormat = GetLocalizedValue($"DamageSingleFormatTime");
                result += ReplaceSingleValueInString(durationFormat, Duration);
            }

            if (DurationModifier > 0.0f)
            {
                string durationModifierFormat = GetLocalizedValue($"{Name}DurationModifier");
                result += ReplaceSingleValueInString(durationModifierFormat, DurationModifier);
            }

            return result;
        }
    }
}
