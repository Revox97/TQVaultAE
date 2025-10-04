using TQVaultAE.IO.Records;

namespace TQVaultAE.IO.Parsers
{
    /// <summary>
    /// Initializes a new instance of the ArzFile class.
    /// </summary>
    /// <param name="fileName">name of the ARZ file.</param>
    public class ArzFile(string fileName)
    {
        /// <summary>
		/// Name of the ARZ file.
		/// </summary>
		public readonly string FileName = fileName;

		/// <summary>
		/// String table
		/// </summary>
		public string[] Strings = [];

		/// <summary>
		/// RecordInfo keyed by their ID
		/// </summary>
		public Dictionary<RecordId, RecordInfo> RecordInfo = [];

		/// <summary>
		/// Ordered keys for the recordInfo Dictionary
		/// </summary>
		public IEnumerable<RecordId> Keys => RecordInfo.Keys.OrderBy(v => v);

        /// <summary>
        /// Gets the number of DBRecords
        /// </summary>
        public int Count => RecordInfo.Count;

		/// <summary>
		/// Retrieves a string from the string table.
		/// </summary>
		/// <param name="index">Offset in the string table.</param>
		/// <returns>string from the string table</returns>
		public string Getstring(int index) => Strings[index];
    }
}
