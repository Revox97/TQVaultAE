using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class JewelryItem : EquipableItem
    {
        public JewelryItemType JewelryType { get; set; }

        public JewelryItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
            JewelryType = Class is ItemClass.ArmorJewelry_Amulet ? JewelryItemType.Amulet : JewelryItemType.Ring;
        }

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
            result.Inlines.Add(new LineBreak());
            result.Inlines.AddRange(GetItemDescriptionProperties());
            result.Inlines.Add(new LineBreak());

            if (Prefix is not null)
            {
                result.Inlines.Add(new Run()
                {
                    Text = $"Prefix: {Prefix.Name}", // TODO Localize
                    Foreground = new SolidColorBrush(TitanQuestColors.Orange),
                    Classes = { ClassSelectorRunItemDefault }
                });
                result.Inlines.Add(new LineBreak());

                List<string> prefixProperties = Prefix.GetAffixDescriptionProperties();

                foreach (string prefixProperty in prefixProperties)
                {
                    result.Inlines.Add(new Run()
                    {
                        Text = prefixProperty,
                        Foreground = new SolidColorBrush(TitanQuestColors.Blue),
                        Classes = { ClassSelectorRunItemDefault }
                    });
                    result.Inlines.Add(new LineBreak());
                }

                // TODO add skill augments
                result.Inlines.Add(new LineBreak());
            }

            if (Suffix is not null)
            {
                result.Inlines.Add(new Run()
                {
                    Text = $"Suffix: {Suffix.Name}", // TODO Localize
                    Foreground = new SolidColorBrush(TitanQuestColors.Orange),
                    Classes = { ClassSelectorRunItemDefault }
                });
                result.Inlines.Add(new LineBreak());

                List<string> suffixProperties = Suffix.GetAffixDescriptionProperties();

                foreach (string suffixProperty in suffixProperties)
                {
                    result.Inlines.Add(new Run()
                    {
                        Text = suffixProperty,
                        Foreground = new SolidColorBrush(TitanQuestColors.Blue),
                        Classes = { ClassSelectorRunItemDefault }
                    });
                    result.Inlines.Add(new LineBreak());
                }

                // TODO add skill augments
                result.Inlines.Add(new LineBreak());
            }

            if (TalismanOne is not null)
            {
                TextBlock talismanTb = TalismanOne.GetItemDescription();
                result.Inlines.AddRange(talismanTb.Inlines!);
                result.Inlines.Add(new LineBreak());
                result.Inlines.Add(new LineBreak());
            }

            if (TalismanTwo is not null)
            {
                TextBlock talismanTb = TalismanTwo.GetItemDescription();
                result.Inlines.AddRange(talismanTb.Inlines!);
                result.Inlines.Add(new LineBreak());
                result.Inlines.Add(new LineBreak());
            }

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
