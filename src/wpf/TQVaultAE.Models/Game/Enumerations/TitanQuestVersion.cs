namespace TQVaultAE.Models.Game.Enumerations
{
    [Flags]
    public enum TitanQuestVersion
    {
        Original = 1 << 0,
        ImmortalThrone = 1 << 1,
        AnniversaryEdition = 1 << 2,
        All = Original | ImmortalThrone | AnniversaryEdition,
    }
}
