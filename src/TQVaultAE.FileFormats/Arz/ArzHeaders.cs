namespace TQVaultAE.FileFormats.Arz
{
    public class ArzHeaders
    {
        public int RecordTableStart { get; set; }
        public int RecordTableSize { get; set; }
        public int RecordTableCount { get; set; }
        public int InfoTableStart { get; set; }
        public int InfoTableSize { get; set; }
    }
}
