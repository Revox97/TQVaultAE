using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class OneShotItem : Item
    {
        public List<OneShotBonus> Bonuses { get; set; } = [];

        public OneShotItem(Item item)
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
        }

        public override bool ShowIconAccent => false;

        public override Color Color
        {
            get
            {
                // TODO Verify colors
                return Class switch
                {
                    ItemClass.OneShot_PotionHealth => TitanQuestColors.Red,
                    ItemClass.OneShot_PotionMana => TitanQuestColors.Blue,
                    ItemClass.OneShot_Scroll => TitanQuestColors.Khaki,
                    ItemClass.OneShot_Scroll_Eternal => TitanQuestColors.Red,
                    ItemClass.OneShot_Dye => TitanQuestColors.DarkGray,
                    _ => TitanQuestColors.Red,
                };
            }
        }

        public override Color AccentColor
        {
            get
            {
                // TODO Verify colors
                return Class switch
                {
                    ItemClass.OneShot_PotionHealth
                        or ItemClass.OneShot_PotionMana
                        or ItemClass.OneShot_Scroll_Eternal
                        or ItemClass.OneShot_Dye
                        => new Color(0x10, TitanQuestColors.Silver.R, TitanQuestColors.Silver.G, TitanQuestColors.Silver.B),
                    ItemClass.OneShot_Scroll => new Color(0x10, TitanQuestColors.Khaki.R, TitanQuestColors.Khaki.G, TitanQuestColors.Khaki.B),
                    _ => TitanQuestColors.Red,
                };
            }
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

            result.Inlines.Add(new Run()
            {
                Text = Description,
                Classes = { ClassSelectorRunItemDefault }
            });

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
