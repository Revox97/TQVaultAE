using System.ComponentModel;
using TQVaultAE.Model.Attributes;

namespace TQVaultAE.Model.Enumerations
{
    public enum AttackSpeed
    {
        [Description("CharacterAttackSpeedNotSet")]
        [LocalizationTag("CharacterAttackSpeedNotSet")]
        NotSet,
        [Description("CharacterAttackSpeedVerySlow")]
        [LocalizationTag("CharacterAttackSpeedVerySlow")]
        VerySlow,
        [Description("CharacterAttackSpeedSlow")]
        [LocalizationTag("CharacterAttackSpeedSlow")]
        Slow,
        [Description("CharacterAttackSpeedAverage")]
        [LocalizationTag("CharacterAttackSpeedAverage")]
        Average,
        [Description("CharacterAttackSpeedFast")]
        [LocalizationTag("CharacterAttackSpeedFast")]
        Fast,
        [Description("CharacterAttackSpeedVeryFast")]
        [LocalizationTag("CharacterAttackSpeedVeryFast")]
        VeryFast,
    }
}
