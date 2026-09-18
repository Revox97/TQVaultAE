using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace TQVaultAE.Model.UI
{
    /// <summary>
    /// Represents an Icon, that is used in the TQVaultAE UI.
    /// </summary>
    /// <param name="id">The id of the <see cref="Icon"/>.</param>
    /// <param name="resourcePath">The resource uri of the <see cref="Icon"/>.</param>
    public class Icon(string id, string resourcePath)
    {
        /// <summary>
        /// Gets or sets the id of the <see cref="Icon"/>.
        /// </summary>
        public string Id { get; set; } = id;

        /// <summary>
        /// Gets or sets the resource uri of the <see cref="Icon"/>.
        /// </summary>
        public string ResourcePath { get; set; } = resourcePath;

        [NotMapped]
        public Bitmap? Bitmap { get; set; }
    }
}
