using System.ComponentModel;

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
        Level,
        [Description("strengthRequirement")]
        Strenth
    }
}
