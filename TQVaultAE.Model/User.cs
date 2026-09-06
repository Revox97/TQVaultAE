using System.Text.Json.Serialization;
using TQVaultAE.Model.Settings;

namespace TQVaultAE.Model
{
    public class User
    {
        // windows uuid | find solution for linux
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public Guid SettingsId { get; set; } 

        [JsonPropertyName("settings")]
        public Settings.Settings Settings { get; set; } = new();
    }
}
