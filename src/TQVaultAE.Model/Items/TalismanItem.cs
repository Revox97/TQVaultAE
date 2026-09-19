using System.ComponentModel.DataAnnotations.Schema;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents a relic or charm item.
    /// </summary>
    // Represents relics and charms. Find a better name for it.
    public class TalismanItem : Item
    {
        public TalismanType TalismanType { get; set; }

        public int ShardCompletionCount => TalismanType switch
        {
            TalismanType.Charm => 5,
            TalismanType.Relic or _ => 3
        };

        /// <summary>
        /// Gets or sets the bonus of the <see cref="TalismanItem"/>.
        /// </summary>
        public string Bonus { get; set; } = string.Empty;

        // EF constructor
        public TalismanItem()
        {

        }

        public TalismanItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
            CanStack = true;
            StackCount = Var1 == 0 ? 1 : Var1;
            TalismanType = item.Class == ItemClass.ItemCharm ? TalismanType.Charm : TalismanType.Relic;
        }

        [NotMapped]
        public Bitmap IconIncomplete { get; set; } = null!;

        [NotMapped]
        public Bitmap IconComplete { get; set; } = null!;

        [NotMapped]
        public override Bitmap Icon
        {
            get
            {
                return StackCount < ShardCompletionCount
                    ? IconIncomplete
                    : IconComplete;
            }
            // TODO implement a useful setter.
        }

        public override bool ShowStackCount => StackCount != ShardCompletionCount;

        public override Color Color => TitanQuestColors.Orange;

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
                Text = StackCount == ShardCompletionCount
                    ? $"Completed {TalismanType}" // TODO Localize correctly
                    : $"{TalismanType} - {StackCount} / {ShardCompletionCount}", // TODO localize correctly
                Foreground = new SolidColorBrush(Color),
                Classes = { ClassSelectorRunItemDefault }
            });

            result.Inlines.Add(new LineBreak());

            string[] description = Description.Split(" {^n}{^y}"); // TODO translate ^y to the actual TQ Color

            result.Inlines.Add(new Run()
            {
                Text = description[0],
                Classes = { ClassSelectorRunItemDefault }
            });

            result.Inlines.Add(new LineBreak());

            if (description.Length == 2)
            {
                result.Inlines.Add(new Run()
                {
                    Text = description[1],
                    Foreground = new SolidColorBrush(TitanQuestColors.Yellow),
                    Classes = { ClassSelectorRunItemDefault }
                });
            }

            result.Inlines.Add(new LineBreak());
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

            result.Inlines.Add(new LineBreak());

            // Separator stretch workaround
            result.LayoutUpdated += (_, _) =>
            {
                foreach (InlineUIContainer separator in result.Inlines.Where(x => x is InlineUIContainer).Cast<InlineUIContainer>())
                    separator.Child.Width = result.Bounds.Width;
            };

            // TODO Get DLC

            return result;
        }
    }
}
