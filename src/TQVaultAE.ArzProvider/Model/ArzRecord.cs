namespace TQVaultAE.Arz.Model
{
    public class ArzRecord
    {
        internal string Type { get; set; } = string.Empty;
        internal int DataLength { get; set; }

        // What exactly does this represent?
        internal string Info { get; set; } = string.Empty;
    }
}
