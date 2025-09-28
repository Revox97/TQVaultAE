namespace TQVaultAE.IO
{
    [Flags]
    internal enum TitanQuestVersion
    {
        Original = 1 << 0,
        ImmortalThrone = 1 << 1,
        AnniversaryEdition = 1 << 2,
        All = Original | ImmortalThrone | AnniversaryEdition,
    }
}
