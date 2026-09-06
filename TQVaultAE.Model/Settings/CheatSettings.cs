using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Settings
{
    public class CheatSettings
    {
        [JsonPropertyName("areCheatsEnabled")]
        public bool AreCheatsEnabled { get; set; }

        [JsonPropertyName("characterEditingFeatures")]
        public bool AreCharacterEditingFeaturesEnabled { get; set; }

        [JsonPropertyName("itemCopying")]
        public bool IsItemCopyingEnabled { get; set; }

        [JsonPropertyName("itemEditingFeatures")]
        public bool AreItemEditingFeaturesEnabled { get; set; }

        [JsonPropertyName("epicAndLegendaryAffixes")]
        public bool AreEpicAndLegendaryAffixesEnabled { get; set; }
    }
}
