using Avalonia.Controls.Documents;
using Avalonia.Media;
using TQVaultAE.Localisation;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items.ItemProperties
{
    /// <summary>
    /// Represents a global <see cref="ItemProperty"/>, that can contain multiple <see cref="NormalItemProperty"/>s.
    /// </summary>
    public class GlobalItemProperty : ItemProperty
    {
        /// <summary>
        /// Gets or sets a list of sub <see cref="ItemProperty"/>s.
        /// </summary>
        public List<ItemProperty> SubProperties { get; set; } = [];

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalItemProperty"/> class.
        /// </summary>
        /// <param name="gameLocalizationService">A service used for localizing Titan Quest elements.</param>
        public GlobalItemProperty(IGameLocalizationService? gameLocalizationService = null) : base(gameLocalizationService)
        {
            Type = ItemPropertyType.Global;
        }

        /// <summary>
        /// Gets a string, that represents the value of the property, the same way, as whithin the game.
        /// </summary>
        /// <returns>A <see cref="string"/>, that represents the <see cref="ItemProperty"/>.</returns>
        public override string ToString()
        {
            string propertyValue = GetLocalizedValue("GlobalPercentChanceOfAllTag");

            return !propertyValue.StartsWith("<<UNKNOWN>>")
                ? ReplaceSingleValueInString(propertyValue, Chance)
                : propertyValue;
        }

        public override List<Inline> GetDescription()
        {
            List<Inline> result = [];

            result.Add(new Run()
            {
                Text = ToString(),
                Foreground = new SolidColorBrush(TitanQuestColors.Blue), // TODO Replace with Colors!
                Classes = { "Run__ItemDefault" },
            });

            result.Add(new LineBreak());

            foreach (ItemProperty itemProperty in SubProperties)
                result.AddRange(itemProperty.GetDescription());

            return result;
        }
    }
}
