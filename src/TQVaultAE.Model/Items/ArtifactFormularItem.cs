using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class ArtifactFormularItem : Item
    {
        public ArtifactFormularItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
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
                TextWrapping = TextWrapping.Wrap
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
                Text = "Recipe", // TODO Localize
                Classes = { ClassSelectorRunItemDefault }
            });

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new LineBreak());

            result.Inlines.Add(new Run()
            {
                Text = "Required Reagents (0/3)", // TODO Localize and get correct data.
                Foreground = new SolidColorBrush(TitanQuestColors.Yellow),
                Classes = { ClassSelectorRunItemDefault }
            });

            result.Inlines.Add(new LineBreak());
            // TODO get reagent1

            result.Inlines.Add(new LineBreak());
            // TODO get reagent2

            result.Inlines.Add(new LineBreak());
            // TODO get reagent3

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new LineBreak());
            // TODO get "crafting" cost

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new LineBreak());
            // TODO Get artifact description

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new InlineUIContainer { Child = new Rectangle { Classes = { ClassSelectorRunItemSeparator } } });
            result.Inlines.Add(new LineBreak());

            result.Inlines.Add(new Run()
            {
                Text = $"Seed: {Seed}", // TODO Localize
                Foreground = new SolidColorBrush(TitanQuestColors.DarkGray),
                Classes = { ClassSelectorRunItemDefault }
            });

            if (GameDlc is not GameDlc.TitanQuest)
            {
                result.Inlines.Add(new LineBreak());

                result.Inlines.Add(new Run()
                {
                    Text = $"{GameDlc.GetEnumStringValue()} Item", // TODO Localize
                    Foreground = new SolidColorBrush(TitanQuestColors.Green),
                    Classes = { ClassSelectorRunItemDefault }
                });
            }

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
