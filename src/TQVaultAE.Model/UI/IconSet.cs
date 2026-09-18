namespace TQVaultAE.Model.UI
{
    /// <summary>
    /// Represents a set of <see cref="Icon"/>s, that are used together (e.g. in buttons).
    /// <remarks>Initializes a new instance of the <see cref="IconSet"/> class.</remarks>
    /// </summary>
    public class IconSet
    {
        /// <summary>
        /// Gets or sets the id of the <see cref="IconSet"/>.
        /// </summary>
        public string Id { get; set; } = "defaultIconSet";

        /// <summary>
        /// Gets or sets the id of the down <see cref="Icon"/>. Used for EF.
        /// </summary>
        public string IconDownId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the down <see cref="Icon"/>.
        /// </summary>
        public Icon IconDown { get; set; }

        /// <summary>
        /// Gets or sets the id of the up <see cref="Icon"/>. Used for EF.
        /// </summary>
        public string IconUpId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the up <see cref="Icon"/>.
        /// </summary>
        public Icon IconUp { get; set; }

        /// <summary>
        /// Gets or sets the id of the hover <see cref="Icon"/>. Used for EF.
        /// </summary>
        public string IconHoverId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hover <see cref="Icon"/>.
        /// </summary>
        public Icon IconHover { get; set; }

        // EF constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public IconSet() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <param name="id">The id of the <see cref="IconSet"/>.</param>
        /// <param name="iconDown">The <see cref="Icon"/>, that is used for down state.</param>
        /// <param name="iconUp">The <see cref="Icon"/>, that is used for up state.</param>
        /// <param name="iconHover">The <see cref="Icon"/>, that is used for hover state.</param>
        public IconSet(string id, Icon iconDown, Icon iconUp, Icon iconHover)
        {
            Id = id;
            IconDown = iconDown;
            IconDownId = iconDown.ResourcePath.ToString();
            IconUp = iconUp;
            IconUpId = iconUp.ResourcePath.ToString();
            IconHover = iconHover;
            IconHoverId = iconHover.ResourcePath.ToString();
        }
    }
}
