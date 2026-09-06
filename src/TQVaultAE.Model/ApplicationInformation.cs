using System.Text.Json.Serialization;

namespace TQVaultAE.Model
{
    public class ApplicationInformation
    {
        [JsonPropertyName("version")]
        public Version Version { get; set; } = new Version(5, 0, 0, 0);

        [JsonPropertyName("name")]
        public string Name { get; set; } = "TQVaultAE";

        // TODO add remaining properties
    }
}
