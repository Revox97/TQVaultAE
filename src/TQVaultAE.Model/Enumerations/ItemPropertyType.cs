using System.ComponentModel;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Model.Enumerations
{
    /// <summary>
    /// Represents the type of an <see cref="ItemProperty"/>.
    /// </summary>
    public enum ItemPropertyType
    {
        [Description("characterBaseAttackSpeedTag")]
        CharacterBaseAttackSpeedTag,
        [Description("defensiveBlockModifier")]
        DefensiveBlockModifier,
        [Description("defensiveDisruption")]
        DefensiveDisruption,
        [Description("defensiveLife")]
        DefensiveLife,
        [Description("defensivePierce")]
        DefensivePierce,
        [Description("defensiveProtection")]
        DefensiveProtection,
        [Description("offensiveLifeLeechMin")]
        OffensiveLifeLeechMin,
        [Description("offensiveLifeMin")]
        OffensiveLifeMin,
        [Description("offensiveLifeModifier")]
        OffensiveLifeModifier,
        [Description("offensivePhysicalModifier")]
        OffensivePhysicalModifier,
        [Description("offensiveSlowLifeLeachDurationMin")] // yes, there seems to be a typo in the TQ db.
        OffensiveSlowLifeLeechDurationMin,
        [Description("offensiveSlowLifeLeachMin")] // yes, there seems to be a typo in the TQ db.
        OffensiveSlowLifeLeechMin,
    }
}
