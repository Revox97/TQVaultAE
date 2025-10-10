using TQVaultAE.IO.Records;

namespace TQVaultAE.IO.Parsers
{
    public class RecordInfo
    {
        /// <summary>
		/// Offset in the file for this record.
		/// </summary>
		public int Offset;

		/// <summary>
		/// String index of ID
		/// </summary>
		public int IdStringIndex;

		/// <summary>
		/// Initializes a new instance of the RecordInfo class.
		/// </summary>
		public RecordInfo()
		{
			IdStringIndex = -1;
			RecordType = string.Empty;
		}

		/// <summary>
		/// Gets the string ID
		/// </summary>
		public RecordId Id { get; set; }

		/// <summary>
		/// Gets the Record type.
		/// </summary>
		public string RecordType { get; set; }
    }
}
