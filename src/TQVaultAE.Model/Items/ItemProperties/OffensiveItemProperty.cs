using System.Text.RegularExpressions;
using TQVaultAE.Localisation;

namespace TQVaultAE.Model.Items.ItemProperties
{
    public partial class OffensiveItemProperty : ItemProperty
    {
        public bool IsGlobal { get; set; }

        public bool XOR { get; set; }

        public float Chance { get; set; }
        public bool HasChance => Chance > 0f;

        public float DamageRatio { get; set; }
        public bool HasDamageRation => DamageRatio > 0f;

        public float DurationMax { get; set; }
        public bool HasDurationMax => DurationMax > 0f;

        public float DurationMin { get; set; }
        public bool HasDurationMin => DurationMin > 0f;

        public float DrainMax { get; set; }
        public bool HasDrainMax => DrainMax > 0f;

        public float DrainMin { get; set; }
        public bool HasDrainMin => DrainMin > 0f;

        public float Max { get; set; }
        public bool HasMax => Max > 0f;

        public float Min { get; set; }
        public bool HasMin => Min > 0f;

        public float Modifier { get; set; }
        public bool HasModifier => Modifier > 0f;

        public override float GetValueBySeed(int seed)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string tag = new ItemPropertyTagCollection()[Name];

            string? localizedName = new GameLocalizationService().GetLocalizedValueByTagAsync(tag).Result;

            // TODO Can't find it in localization, where is this value coming from?
            if (tag == "<<TOTAL>>")
                localizedName = "{%+.0f0}% Total Damage";

            if (string.IsNullOrEmpty(localizedName) || tag == "<<UNKNOWN>>")
            {
                if (tag == "<<UNKNOWN>>")
                    return Name;

                return Name;
            }


            Regex rgx = PropertyValuePlaceHolderRegex();
            string result = "";

            if (HasChance && HasMin)
            {
                tag = new ItemPropertyTagCollection()[$"{Name}Chance"];
                string? chanceFormat = new GameLocalizationService().GetLocalizedValueByTagAsync(tag).Result;

                if (string.IsNullOrEmpty(chanceFormat))
                    result = Name;

                string chance = rgx.Replace(chanceFormat, match =>
                {
                    int decimals = int.Parse(match.Groups["decimals"].Value);
                    string plusIndicator = match.Groups["plusIndicator"].Value;
                    return $"{plusIndicator}{Chance.ToString($"F{decimals}")}";
                });

                result = $"{chance}{Min}{localizedName}";
            }

            else if (HasMin && HasMax)
            {
                string rangeFormat = new GameLocalizationService().GetLocalizedValueByTagAsync("DamageRangeFormat").Result!;

                MatchCollection matches = rgx.Matches(rangeFormat);

                int decimalsMin = int.Parse(matches[0].Groups["decimals"].Value);
                int indexMin = int.Parse(matches[0].Groups["index"].Value);
                string range = rangeFormat.Replace(matches[0].Value, Min.ToString($"F{decimalsMin}"));

                int decimalsMax = int.Parse(matches[1].Groups["decimals"].Value);
                int indexMax = int.Parse(matches[1].Groups["index"].Value);
                range = range.Replace(matches[1].Value, Max.ToString($"F{decimalsMax}"));

                result = $"{range}{localizedName}";
            }

            else if (HasMin)
            {
                result = rgx.IsMatch(localizedName)
                    ? rgx.Replace(localizedName, match =>
                        {
                            int decimals = int.Parse(match.Groups["decimals"].Value);
                            string plusIndicator = match.Groups["plusIndicator"].Value;
                            return $"{plusIndicator}{Min.ToString($"F{decimals}")}";
                        })
                    : $"{Min}{localizedName}";
            }

            else if (HasMax)
            {
                result = $"{Max}{localizedName}";
            }

            else if (HasModifier)
            {
                if (tag != "<<TOTAL>>")
                {
                    tag = new ItemPropertyTagCollection()[$"{Name}Modifier"];
                    localizedName = new GameLocalizationService().GetLocalizedValueByTagAsync(tag).Result;

                    if (string.IsNullOrEmpty(localizedName))
                        result = Name;
                }
                else
                {
                    result = rgx.IsMatch(localizedName)
                        ? rgx.Replace(localizedName, match =>
                            {
                                int decimals = int.Parse(match.Groups["decimals"].Value);
                                string plusIndicator = match.Groups["plusIndicator"].Value;
                                return $"{plusIndicator}{Modifier.ToString($"F{decimals}")}";
                            })
                        : $"{Modifier}{localizedName}";
                }
            }

            else
            {

            }

            return $"{(IsGlobal ? "\t" : "")}{result}";
        }

        [GeneratedRegex(@"{%(?<plusIndicator>\+?)\.(?<decimals>[0-9])f(?<index>[0-9])}")]
        internal static partial Regex PropertyValuePlaceHolderRegex();
    }
}
