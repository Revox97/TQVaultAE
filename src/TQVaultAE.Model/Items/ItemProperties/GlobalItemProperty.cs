using Avalonia.Controls.Documents;
using Avalonia.Media;
using TQVaultAE.Localisation;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items.ItemProperties
{
    public class GlobalItemProperty : ItemProperty
    {
        public List<ItemProperty> SubProperties { get; set; } = [];

        public GlobalItemProperty(IGameLocalizationService? gameLocalizationService = null) : base(gameLocalizationService)
        {
            Type = ItemPropertyType.Global;
        }

        public override string ToString()
        {
            string propertyValue = GetLocalizedValue(Name);

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
