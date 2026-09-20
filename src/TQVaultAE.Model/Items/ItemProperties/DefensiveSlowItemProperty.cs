using System.Text.RegularExpressions;
using TQVaultAE.Localisation;

namespace TQVaultAE.Model.Items.ItemProperties
{
    public partial class DefensiveSlowItemProperty : ItemProperty
    {
        public float Value { get; set; }

        public override float GetValueBySeed(int seed)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string tag = new ItemPropertyTagCollection()[Name];
            string? localizedName = new GameLocalizationService().GetLocalizedValueByTagAsync(tag).Result;

            if (string.IsNullOrEmpty(localizedName) || tag == "<<UNKNOWN>>")
            {
                if (tag == "<<UNKNOWN>>")
                    return Name;

                return Name;
            }

            Regex rgx = PropertyValuePlaceHolderRegex();

            string result = rgx.Replace(localizedName, match =>
            {
                int decimals = int.Parse(match.Groups["decimals"].Value);
                string plusIndicator = match.Groups["plusIndicator"].Value;
                return $"{plusIndicator}{Value.ToString($"F{decimals}")}";
            });

            return result;
        }

        [GeneratedRegex(@"{%(?<plusIndicator>\+?)\.(?<decimals>[0-9])f(?<index>[0-9])}")]
        internal static partial Regex PropertyValuePlaceHolderRegex();
    }
}
