namespace TQVaultAE.TitanQuestDataProviders.Model
{
    internal class ArcHeaders
    {
        public string FileType { get; set; } = string.Empty;
        public int NumberOfFileRecords { get; set; }
        public int PartEntryCount { get; set; }
        public int TocOffset { get; set; }
    }
}
