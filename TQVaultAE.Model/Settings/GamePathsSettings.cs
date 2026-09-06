using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Settings
{
    public class GamePathsSettings
    {
        [JsonPropertyName("autoDetect")]
        public bool IsAutoDetectionEnabled { get; set; }

        [JsonPropertyName("TitanQuestGamePath")]
        public string TitanQuestGamePath { get; set; } = string.Empty;

        [JsonPropertyName("ImmortalThroneGamePath")]
        public string ImmortalThroneGamePath { get; set; } = string.Empty;
    }
}
