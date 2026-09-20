using System.Text.RegularExpressions;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using TQVaultAE.Localisation;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items.ItemProperties
{
    public partial class GlobalItemProperty : ItemProperty
    {
        public List<ItemProperty> Children { get; set; } = [];

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

        public override List<Inline> GetDescription()
        {
            List<Inline> result = [];

            result.Add(new Run()
            {
                Text = ToString(),
                Foreground = new SolidColorBrush(TitanQuestColors.Blue),
                Classes = { "Run__ItemDefault" },
            });

            result.Add(new LineBreak());

            foreach (ItemProperty itemProperty in Children)
                result.AddRange(itemProperty.GetDescription());

            return result;
        }
    }
}
