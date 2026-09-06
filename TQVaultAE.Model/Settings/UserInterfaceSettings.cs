using System.Text.Json.Serialization;
using TQVaultAE.Model.Enumerations;

// TODO: Add localization for all currently available languages:
// - English
// - Chinese
// - Czech
// - French (grrrrr)
// - German
// - Italian
// - Polish
// - Portugese
// - Russian
// - Spanish

namespace TQVaultAE.Model.Settings
{
    public class UserInterfaceSettings
    {
        [JsonPropertyName("language")]
        public Language Language { get; set; } = Language.English;

        // TODO: Add font support
        [JsonPropertyName("font")]
        public string Font { get; set; } = string.Empty;

        [JsonPropertyName("sounds")]
        public bool AreSoundsEnabled { get; set; }

        // TODO: Might be the wrong settings type. Recategorize, if necessary
        [JsonPropertyName("itemRequirementRestriction")]
        public bool IsItemRequirementRestrictionEnabled { get; set; } = true;

        [JsonPropertyName("itemBackgroundTransparencyLevel")]
        public double ItemBackgroundTransparencyLevel { get; set; } = 0.4d;
    }
}
