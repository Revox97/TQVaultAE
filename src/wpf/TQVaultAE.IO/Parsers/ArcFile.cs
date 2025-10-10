namespace TQVaultAE.IO.Parsers
{
    /// <summary>
    /// Initializes a new instance of the ArcFile class.
    /// </summary>
    /// <param name="fileName">File Name of the ARC file to be read.</param>
    public class ArcFile(string fileName)
    {
        		/// <summary>
		/// Signifies that the file has been read into memory.
		/// </summary>
		public bool FileHasBeenRead;

        /// <summary>
        /// Dictionary of the directory entries.
        /// </summary>
        // TODO Record Id most likely has to be reworked
        public Dictionary<string, ArcDirEntry> DirectoryEntries = [];
        //public Dictionary<RecordId, ArcDirEntry> DirectoryEntries = [];

		/// <summary>
		/// Ordered keys for the directoryEntries dictionary.
		/// </summary>
        // TODO Record Id most likely has to be reworked
		public IEnumerable<string> Keys => DirectoryEntries.Keys.OrderBy(v => v);
		//public IEnumerable<RecordId> Keys => DirectoryEntries.Keys.OrderBy(v => v);

        /// <summary>
        /// Gets the ARC file name.
        /// </summary>
        public string FileName { get; private set; } = fileName;

        /// <summary>
        /// Gets the number of Directory entries
        /// </summary>
        public int Count => DirectoryEntries.Count;
    }
}
