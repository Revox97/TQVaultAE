using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class ArmorItem : EquipableItem
    {
        public ArmorItemType ArmorType { get; set; }

        public ArmorItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
            ArmorType = GetArmorTypeFromClass(Class);
        }

        private static ArmorItemType GetArmorTypeFromClass(ItemClass itemClass)
        {
            return itemClass switch
            {
                ItemClass.ArmorProtective_Head => ArmorItemType.Head,
                ItemClass.ArmorProtective_Forearm => ArmorItemType.Arms,
                ItemClass.ArmorProtective_LowerBody => ArmorItemType.Legs,
                ItemClass.ArmorProtective_UpperBody => ArmorItemType.Body,
                _ => default
            };
        }

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

            // TODO Add armor

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
            // TODO Get Prefix properties

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new LineBreak());
            // TODO Get Suffix properties

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new LineBreak());
            // TODO Get RelicOne properties

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new LineBreak());
            // TODO Get RelicTwo properties

            result.Inlines.Add(new LineBreak());
            result.Inlines.Add(new LineBreak());
            // TODO Add set information

            result.Inlines.Add(new LineBreak());
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
