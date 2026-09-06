using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Settings
{
    public class Settings
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("general")]
        public GeneralSettings General { get; set; } = new();

        [JsonPropertyName("userInterface")]
        public UserInterfaceSettings UserInterface { get; set; } = new();

        [JsonPropertyName("game")]
        public GameSettings Game { get; set; } = new();

        [JsonPropertyName("cheats")]
        public CheatSettings Cheats = new();
    }
}
