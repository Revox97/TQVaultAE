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
    }
}
