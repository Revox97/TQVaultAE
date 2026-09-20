using Avalonia.Controls.Documents;
using Avalonia.Media;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items.ItemProperties
{
    /// <summary>
    /// Represents a property of an <see cref="Item"/>.
    /// </summary>
    // TODO Have sub elements, for e.g. Global chance
    // TOdo Also merge item properties, that are belonging together
    public abstract class ItemProperty
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="ItemProperty"/>.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public abstract float GetValueBySeed(int seed);

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
    }
}
