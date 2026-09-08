namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents a relic item.
    /// </summary>
    public class RelicItem : Item
    {
        /// <summary>
        /// Gets or sets the bonus of the <see cref="RelicItem"/>.
        /// </summary>
        public string Bonus { get; set; } = string.Empty;
    }
}
