namespace TQVaultAE.FileFormats.Chr
{
    /// <summary>
    /// Represents a .chr file.
    /// </summary>
    /// <param name="name">The name of the file.</param>
    /// <param name="root">The root element of the file.</param>
    public class ChrFile(string name, ChrBlock root)
    {
        /// <summary>
        /// Gets or sets the file name of the <see cref="ChrFile"/>.
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// Gets or sets the root element of the <see cref="ChrFile"/>.
        /// </summary>
        public ChrBlock Root { get; } = root;
    }
}
