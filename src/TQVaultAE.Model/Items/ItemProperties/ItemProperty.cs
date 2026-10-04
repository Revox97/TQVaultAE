using System.Text.RegularExpressions;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using TQVaultAE.Localisation;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items.ItemProperties
{
    /// <summary>
    /// Represents a property of an <see cref="Item"/>.
    /// </summary>
    // TODO Have sub elements, for e.g. Global chance
    // TOdo Also merge item properties, that are belonging together
    public abstract partial class ItemProperty
    {
        private readonly IGameLocalizationService _gameLocalizationService;

        /// <summary>
        /// Gets or sets the name of the <see cref="ItemProperty"/>.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the <see cref="ItemProperty"/>. Used for sorting.
        /// </summary>
        public ItemPropertyType Type { get; set; }

        /// <summary>
        /// Gets or sets the chance, with which the effect of the <see cref="ItemProperty"/> will be triggered.
        /// </summary>
        public float Chance { get; set; }

        internal ItemProperty(IGameLocalizationService? gameLocalizationService = null)
        {
            _gameLocalizationService = gameLocalizationService ?? new GameLocalizationService();
        }

        /// <summary>
        /// Gets UI elements, that can be used to display the <see cref="ItemProperty"/> as part of a <see cref="Avalonia.Controls.TextBlock"/>.
        /// </summary>
        /// <returns>A list of <see cref="Inline"/>s.</returns>
        public virtual List<Inline> GetDescription()
        {
            List<Inline> result = [];

            result.Add(new Run()
            {
                Text = ToString(),
                Foreground = new SolidColorBrush(TitanQuestColors.Blue),
                Classes = { "Run__ItemDefault" },
            });

            result.Add(new LineBreak());
            return result;
        }

        protected string GetLocalizedValue(string input)
        {
            string tag = new ItemPropertyTagCollection()[input];
            string? localizedName = _gameLocalizationService.GetLocalizedValueByTagAsync(tag).Result;

            return !string.IsNullOrEmpty(localizedName) ? localizedName : $"<<UNKNOWN>> - {input}";
        }

        protected static string ReplaceSingleValueInString(string input, float property)
        {
            Regex rgx = PropertyValuePlaceHolderRegex();

            return rgx.Replace(input, match =>
            {
                int decimals = int.Parse(match.Groups["decimals"].Value);
                string plusIndicator = match.Groups["plusIndicator"].Value;
                return $"{plusIndicator}{property.ToString($"F{decimals}")}";
            });
        }

        protected static string ReplaceRangeOfValue(string input, float[] values)
        {
            Regex rgx = PropertyValuePlaceHolderRegex();

            MatchCollection matches = rgx.Matches(input);

            if (matches.Count != values.Length)
                return "<<INVALIDRANGE>>";

            string value = input;

            for (int i = 0; i < values.Length; i++)
            {
                int decimalsMin = int.Parse(matches[i].Groups["decimals"].Value);
                value = value.Replace(matches[i].Value, values[i].ToString($"F{decimalsMin}"));
            }

            return value;
        }

        [GeneratedRegex(@"{%(?<plusIndicator>\+?)\.(?<decimals>[0-9])f(?<index>[0-9])}")]
        internal static partial Regex PropertyValuePlaceHolderRegex();
    }
}
