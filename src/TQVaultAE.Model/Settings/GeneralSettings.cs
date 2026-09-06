using System.Text.Json.Serialization;

// TODO build service to migrate "old school" tq vault settings to this version of the tool | Same applies for vaults
namespace TQVaultAE.Model.Settings
{
    public class GeneralSettings
    {
        // TODO remove | just for info - Vault path becomes obsolete with the data database
        // TODO is CSV delimiter required? (I dont think so)

        [JsonPropertyName("bypassTitleScreenAutomatically")]
        public bool IsBypassTitleScreenEnabled { get; set; }

        [JsonPropertyName("loadLastOpenedCharacterAutomatically")]
        public bool IsLoadLastOpenedCharacterAutomaticallyEnabled { get; set; }

        [JsonPropertyName("loadLastOpenedVaultAutomatically")]
        public bool IsLoadLastOpenedVaultAutomaticallyEnabled { get; set; }

        [JsonPropertyName("loadAllVaultAndCharacterFilesOnStartup")]
        public bool IsLoadAllVaultAndCharacterDataOnStartupEnabled { get; set; }

        [JsonPropertyName("hotReload")]
        public bool IsHotReloadEnabled { get; set; }

        [JsonPropertyName("bypassConfirmationMessages")]
        public bool IsBypassConfirmationMessagesEnabled { get; set; }

        [JsonPropertyName("detailedTooltipView")]
        public bool IsDetailedTooltipViewEnabled { get; set; }

        [JsonPropertyName("playerEquipmentReadOnly")]
        public bool IsPlayerEquipmentReadOnlyEnabled { get; set; } = true;

        [JsonPropertyName("autoStacking")]
        public bool IsAutoStackingEnabled { get; set; } = true;

        [JsonPropertyName("backup")]
        public BackupSettings BackupSettings { get; set; } = new();
    }
}
