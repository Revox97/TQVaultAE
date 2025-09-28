namespace TQVaultAE.IO.Parsers
{
    internal class ChrFileRecord
    {
        public string Key { get; set; } = string.Empty;
        public ChrRecordType Type { get; set; }
        public object? Value { get; set; } = null;
        public int KeyStart { get; set; }
        public int KeyTo { get; set; }
        public int ValueStart { get; set; }
        public int ValueTo { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }

    internal enum ChrRecordType
    {
        Unknown = 0,
        Int,
        Bool,
        String,
        Id,
        Raw,
    }
}
