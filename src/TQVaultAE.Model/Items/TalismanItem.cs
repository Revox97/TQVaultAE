using System.Drawing;
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

        public Bitmap IconIncomplete { get; set; } = null!;
        
        public Bitmap IconComplete { get; set; } = null!;

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
    }
}
