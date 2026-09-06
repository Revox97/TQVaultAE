using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Settings
{
    public class BackupSettings
    {
        [JsonPropertyName("backup")]
        public bool IsBackupEnabled { get; set; }

        [JsonPropertyName("backupPlayerSaves")]
        public bool ArePlayerSavesEnabled { get; set; }

        [JsonPropertyName("gitRepositoryUrl")]
        public string? GitRepositoryUrl { get; set; } = null;
    }
}
