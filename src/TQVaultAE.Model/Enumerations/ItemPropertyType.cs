using System.ComponentModel;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Model.Enumerations
{
    /// <summary>
    /// Represents the type of an <see cref="ItemProperty"/>.
    /// </summary>
    public enum ItemPropertyType
    {
        [Description("characterArmorStrengthReqReduction")]
        CharacterArmorStrengthRequirementReduction,
        [Description("characterBaseAttackSpeedTag")]
        CharacterBaseAttackSpeedTag,
        [Description("characterDefensiveAbility")]
        CharacterDefensiveAbility,
        [Description("characterDexterity")]
        CharacterDexterity,
        [Description("characterIntelligence")]
        CharacterIntelligence,
        [Description("characterLife")]
        CharacterLife,
        [Description("characterStrength")]
        CharacterStrength,
        [Description("characterManaRegenModifier")]
        CharacterManaRegenModifier,
        [Description("defensiveBlockModifier")]
        DefensiveBlockModifier,
        [Description("defensiveDisruption")]
        DefensiveDisruption,
        [Description("defensiveLife")]
        DefensiveLife,
        [Description("defensivePierce")]
        DefensivePierce,
        [Description("defensivePhysical")]
        DefensivePhysical,
        [Description("defensiveProtection")]
        DefensiveProtection,
        [Description("defensiveSlowManaLeach")]
        DefensiveSlowManaLeach,
        [Description("defensiveStun")]
        DefensiveStun,
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
        [Description("offensiveSlowManaLeachDurationMin")]
        OffensiveSLowManaLeachDurationMin,
        [Description("offensiveSlowManaLeachMin")]
        OffensiveSLowManaLeachMin,
        [Description("offensiveTotalDamageModifier")]
        OffensiveTotalDamageModifier,
        [Description("skillCooldownReduction")]
        SkillCooldownReduction,
    }
}
