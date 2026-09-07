using TQVaultAE.Arz.Model;

namespace TQVaultAE.TitanQuestDataProviders.Model
{
    /// <summary>
    /// Represents an Arz file.
    /// </summary>
    public class ArzFile
    {
        /// <summary>
        /// Gets the name of the <see cref="ArzFile"/>.
        /// </summary>
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Gets a list of infos related to the records.
        /// </summary>
        public string[] Infos { get; init; }  = [];

        /// <summary>
        /// Gets a list of <see cref="ArzRecord"/>s.
        /// </summary>
        public ArzRecord[] Records { get; init; } = [];

        public int RecordCount => Records.Length;
    }
}
