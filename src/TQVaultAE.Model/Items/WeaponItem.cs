using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class WeaponItem : EquipableItem
    {
        public WeaponItemType WeaponType { get; set; }

        public WeaponItem(Item item)
        {
            Class = item.Class;
            Position = item.Position;
            Prefix = item.Prefix;
            Suffix = item.Suffix;
            ResourcePath = item.ResourcePath;
            Seed = item.Seed;
            Var1 = item.Var1;
            Var2 = item.Var2;
            WeaponType = GetWeaponTypeFromClass(Class);
        }

        private static WeaponItemType GetWeaponTypeFromClass(ItemClass itemClass)
        {
            return itemClass switch
            {
                ItemClass.WeaponArmor_Shield => WeaponItemType.Shield,
                ItemClass.WeaponHunting_Bow => WeaponItemType.Bow,
                ItemClass.WeaponHunting_RangedOneHand => WeaponItemType.RangedOneHand,
                ItemClass.WeaponHunting_Spear => WeaponItemType.Spear,
                ItemClass.WeaponMagical_Staff => WeaponItemType.Staff,
                ItemClass.WeaponMelee_Axe => WeaponItemType.Axe,
                ItemClass.WeaponMelee_Mace => WeaponItemType.Mace,
                ItemClass.WeaponMelee_Sword => WeaponItemType.Sword,
                _ => default
            };
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
            // TODO Add damage

            result.Inlines.Add(new LineBreak());
            // TODO Add speed

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
