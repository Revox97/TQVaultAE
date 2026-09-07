namespace TQVaultAE.Model.UI
{
    /// <summary>
    /// Represents an Icon, that is used in the TQVaultAE UI.
    /// </summary>
    /// <param name="id">The id of the <see cref="Icon"/>.</param>
    /// <param name="uri">The resource uri of the <see cref="Icon"/>.</param>
    public class Icon(string id, Uri uri)
    {
        /// <summary>
        /// Gets or sets the id of the <see cref="Icon"/>.
        /// </summary>
        public string Id { get; set; } = id;

        /// <summary>
        /// Gets or sets the resource uri of the <see cref="Icon"/>.
        /// </summary>
        public Uri Uri { get; set; } = uri;
    }
}
