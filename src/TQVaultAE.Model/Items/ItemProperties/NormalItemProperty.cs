namespace TQVaultAE.Model.Items.ItemProperties
{
    public class NormalItemProperty : ItemProperty
    {
        public float Min { get; set; }
        public float Max { get; set; }
        public float Duration { get; set; }
        public float DurationModifier { get; set; }
        public bool IsGlobal { get; set; }

        // TODO What is this parameter?
        public bool XOR { get; set; }

        public override string ToString()
        {
            string result = "";

            if (IsGlobal)
                result += "\t";

            if (Chance > 0.0f)
            {
                string chanceFormat = GetLocalizedValue($"Chance");
                result += ReplaceSingleValueInString(chanceFormat, Chance);
            }

            string propertyValue = GetLocalizedValue(Name);

            if (Max > 0.0f)
            {
                string rangeFormat = GetLocalizedValue("RangeFormat");
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
                string durationFormat = GetLocalizedValue($"{Name}DurationMin");
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
