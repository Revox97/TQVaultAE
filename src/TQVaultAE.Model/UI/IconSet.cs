namespace TQVaultAE.Model.UI
{
    /// <summary>
    /// Represents a set of <see cref="Icon"/>s, that are used together (e.g. in buttons).
    /// <remarks>Initializes a new instance of the <see cref="IconSet"/> class.</remarks>
    /// </summary>
    /// <param name="id">The id of the <see cref="IconSet"/>.</param>
    /// <param name="iconDown">The <see cref="Icon"/>, that is used for down state.</param>
    /// <param name="iconUp">The <see cref="Icon"/>, that is used for up state.</param>
    /// <param name="iconHover">The <see cref="Icon"/>, that is used for hover state.</param>
    public class IconSet(string id, Icon iconDown, Icon iconUp, Icon iconHover)
    {
        /// <summary>
        /// Gets or sets the id of the <see cref="IconSet"/>.
        /// </summary>
        public string Id { get; set; } = id;

        /// <summary>
        /// Gets or sets the id of the down <see cref="Icon"/>. Used for EF.
        /// </summary>
        public string IconDownId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the down <see cref="Icon"/>.
        /// </summary>
        public Icon IconDown { get; set; } = iconDown;

        /// <summary>
        /// Gets or sets the id of the up <see cref="Icon"/>. Used for EF.
        /// </summary>
        public string IconUpId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the up <see cref="Icon"/>.
        /// </summary>
        public Icon IconUp { get; set; } = iconUp;

        /// <summary>
        /// Gets or sets the id of the hover <see cref="Icon"/>. Used for EF.
        /// </summary>
        public string IconHoverId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hover <see cref="Icon"/>.
        /// </summary>
        public Icon IconHover { get; set; } = iconHover;
    }
}
