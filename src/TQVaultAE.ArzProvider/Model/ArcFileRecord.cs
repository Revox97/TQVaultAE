namespace TQVaultAE.TitanQuestDataProviders.Model
{
    /// <summary>
    /// Represents information about an ARC directory entry.
    /// </summary>
    public class ArcFileRecord
    {
        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        public string FileName { get; set; } = string.Empty;
        //public RecordId FileName { get; set; }

        // TODO: Make it an enumeration
        /// <summary>
        /// Gets or sets the storage type.
        /// Data is either compressed (3) or stored (1)
        /// </summary>
        public ArcStorageType StorageType { get; set; }

        /// <summary>
        /// Gets or sets the offset within the file.
        /// </summary>
        public int FileOffset { get; set; }

        /// <summary>
        /// Gets or sets the compressed size of this entry.
        /// </summary>
        public int CompressedSize { get; set; }

        /// <summary>
        /// Gets or sets the real size of this entry.
        /// </summary>
        public int RealSize { get; set; }

        /// <summary>
        /// Gets or sets the part data.
        /// </summary>
        public ArcPartEntry[] Parts { get; set; } = [];

        /// <summary>
        /// Gets whether this part is active.
        /// </summary>
        public bool IsActive => StorageType is ArcStorageType.Real || Parts is not null;

        /// <summary>
        /// Gets or sets the content type of the <see cref="ArcFileRecord"/>.
        /// </summary>
        public ArcRecordType ContentType { get; set; }

        /// <summary>
        /// The content of the <see cref="ArcFileRecord"/>.
        /// </summary>
        public object Content { get; set; } = null!;
    }
}
