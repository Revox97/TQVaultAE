using System.ComponentModel;
using System.Text.Json.Serialization;
using TQVaultAE.Model.Attributes;

namespace TQVaultAE.Model.Enumerations
{
    /// <summary>
    /// Represents the type for item requirements.
    /// </summary>
    public enum ItemRequirementType
    {
        [Description("dexterityRequirement")]
        Dexterity,
        [Description("intelligenceRequirement")]
        Intelligence,
        [Description("levelRequirement")]
        [LocalizationTag("LevelRequirement")]
        Level,
        [Description("strengthRequirement")]
        Strength
    }
}
