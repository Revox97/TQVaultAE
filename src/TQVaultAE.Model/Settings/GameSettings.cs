using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Settings
{
    public class GameSettings
    {
        [JsonPropertyName("GamePaths")]
        public GamePathsSettings GamePaths { get; set; } = new();

        [JsonPropertyName("customMaps")]
        public bool AreCustomMapsEnabled { get; set; }

        [JsonPropertyName("originalTqSupport")]
        public bool IsOriginalTqSupportEnabled { get; set; }
    }
}
