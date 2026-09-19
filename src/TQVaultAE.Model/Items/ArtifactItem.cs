using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents an artifact <see cref="Item"/>.
    /// </summary>
    public class ArtifactItem : Item
    {
        /// <summary>
        /// Gets or sets the <see cref="ArtifactClassification"/> of the <see cref="ArtifactItem"/>.
        /// </summary>
        public ArtifactClassification ArtifactClassification { get; set; }

        public ArtifactItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
        }

        public override Color Color => TitanQuestColors.Aqua;

        public override TextBlock GetItemDescription()
        {
            TextBlock result = new()
            {
                Inlines = [],
                TextWrapping = TextWrapping.Wrap,
            };

            result.Inlines.Add(new Run()
            {
                Text = Name,
                Foreground = new SolidColorBrush(Color),
                Classes = { ClassSelectorRunItemName }
            });

            result.Inlines.Add(new LineBreak());

            result.Inlines.Add(new Run()
            {
                Text = "Divine Artifact", // TODO Localize and get the correct value from DB
                Classes = { ClassSelectorRunItemDefault }
            });

            result.Inlines.Add(new LineBreak());

            List<string> properties = GetItemDescriptionProperties();

            foreach (string property in properties)
            {
                result.Inlines.Add(new Run()
                {
                    Text = property,
                    Foreground = new SolidColorBrush(TitanQuestColors.Blue),
                    Classes = { ClassSelectorRunItemDefault }
                });

                result.Inlines.Add(new LineBreak());
            }

            result.Inlines.Add(new LineBreak());

            result.Inlines.Add(new Run()
            {
                Text = "Completion Bonus:", // TODO Localize
                Foreground = new SolidColorBrush(TitanQuestColors.Yellow),
                Classes = { ClassSelectorRunItemDefault }
            });

            // TODO Add completion bonus

            result.Inlines.Add(new LineBreak());

            List<string> requirements = GetItemDescriptionRequirements();

            foreach (string requirement in requirements)
            {
                result.Inlines.Add(new Run()
                {
                    Text = requirement,
                    Foreground = new SolidColorBrush(TitanQuestColors.DarkGray),
                    Classes = { ClassSelectorRunItemDefault }
                });

                result.Inlines.Add(new LineBreak());
            }

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new InlineUIContainer { Child = new Rectangle { Classes = { ClassSelectorRunItemSeparator } } });
            result.Inlines.Add(new LineBreak());

            result.Inlines.Add(new Run()
            {
                Text = $"Seed: {Seed}", // TODO Localize
                Foreground = new SolidColorBrush(TitanQuestColors.DarkGray),
                Classes = { ClassSelectorRunItemDefault }
            });

            // TODO Get DLC

            // Separator stretch workaround
            result.LayoutUpdated += (_, _) =>
            {
                foreach (InlineUIContainer separator in result.Inlines.Where(x => x is InlineUIContainer).Cast<InlineUIContainer>())
                    separator.Child.Width = result.Bounds.Width;
            };

            return result;
        }
    }
}
