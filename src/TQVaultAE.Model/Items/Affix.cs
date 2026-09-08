namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents an <see cref="Affix"/> which can be attached to an <see cref="Item"/>.
    /// </summary>
    public class Affix
    {
        /// <summary>
        /// Gets the path of the <see cref="Affix"/> within the Titan Quest database.
        /// </summary>
        public string Path { get; init; } = string.Empty;
    }
}
