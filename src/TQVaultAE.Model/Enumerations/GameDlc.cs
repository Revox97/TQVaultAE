using TQVaultAE.Model.Attributes;

namespace TQVaultAE.Model.Enumerations
{
    public enum GameDlc
    {
        [GameDlcDescription("TQ", "tagBackground01")]
        TitanQuest,
        [GameDlcDescription("IT", "tagBackground02")]
        ImmortalThrone,
        [GameDlcDescription("RAG", "tagBackground03")]
        Ragnarok,
        [GameDlcDescription("ATL", "tagBackground04")]
        Atlantis,
        [GameDlcDescription("EEM", "x4tagBackground05")]
        EternalEmbers
    }
}
