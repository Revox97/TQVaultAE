using System.ComponentModel;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Model.Enumerations
{
    /// <summary>
    /// Represents the type of an <see cref="ItemProperty"/>.
    /// </summary>
    public enum ItemPropertyType
    {
        [Description("characterArmorAndDexterityReqReduction")]
        CharacterArmorAndDexterityRequirementsReduction,

        [Description("characterArmorStrengthReqReduction")]
        CharacterArmorStrengthRequirementReduction,

        [Description("characterAttackSpeedModifier")]
        CharacterAttackSpeedModifier,

        [Description("characterBaseAttackSpeed")]
        CharacterBaseAttackSpeed,

        [Description("characterBaseAttackSpeedTag")]
        CharacterBaseAttackSpeedTag,

        [Description("characterDefensiveAbility")]
        CharacterDefensiveAbility,

        [Description("characterDexterity")]
        CharacterDexterity,

        [Description("characterEnergyAbsorptionPercent")]
        CharacterEnergyAbsorptionPercent,

        [Description("characterIntelligence")]
        CharacterIntelligence,

        [Description("characterLife")]
        CharacterLife,

        [Description("characterLifeRegen")]
        CharacterLifeRegen,

        [Description("characterLifeRegenModifier")]
        CharacterLifeRegenModifier,

        [Description("characterOffensiveAbility")]
        CharacterOffensiveAbility,

        [Description("characterArmorDexterityReqReduction")]
        CharacterArmorDexterityRequirementsReduction,

        [Description("characterWeaponDexterityReqReduction")]
        CharacterWeaponDexterityRequirementsReduction,

        [Description("characterMana")]
        CharacterMana,

        [Description("characterManaRegenModifier")]
        CharacterManaRegenModifier,

        [Description("characterShieldStrengthReqReduction")]
        CharacterShieldStrengthRequirementReduction,

        [Description("characterStrength")]
        CharacterStrength,

        [Description("characterStrengthModifier")]
        CharacterStrengthModifier,

        [Description("characterTotalSpeedModifier")]
        CharacterTotalSpeedModifier,

        [Description("characterWeaponStrengthReqReduction")]
        CharacterWeaponStrengthRequirementReduction,

        [Description("characterDodgePercent")]
        CharacterDodgePercent,

        [Description("defensiveBlockModifierChance")]
        DefensiveBlockModifierChance,

        [Description("defensiveReflect")]
        DefensiveReflect,

        [Description("defensiveReflectChance")]
        DefensiveReflectChance,

        [Description("defensiveBleeding")]
        DefensiveBleeding,

        [Description("defensiveBlockModifier")]
        DefensiveBlockModifier,

        [Description("defensiveDisruption")]
        DefensiveDisruption,

        [Description("defensiveElementalResistance")]
        DefensiveElementalResistance,

        [Description("defensiveFire")]
        DefensiveFire,

        [Description("defensiveCold")]
        DefensiveCold,

        [Description("defensiveLife")]
        DefensiveLife,

        [Description("defensiveLightning")]
        DefensiveLightning,

        [Description("defensivePierce")]
        DefensivePierce,

        [Description("defensivePhysical")]
        DefensivePhysical,

        [Description("defensivePhysicalChance")]
        DefensivePhysicalChance,

        [Description("defensivePoison")]
        DefensivePoison,

        [Description("defensiveProtection")]
        DefensiveProtection,

        [Description("defensiveProtectionModifier")]
        DefensiveProtectionModifier,

        [Description("defensiveSleep")]
        DefensiveSleep,

        [Description("defensiveSlowLifeLeach")]
        DefensiveSlowLifeLeach,

        [Description("defensiveSlowManaLeach")]
        DefensiveSlowManaLeach,

        [Description("defensiveStun")]
        DefensiveStun,

        [Description("offensiveDisruptionChance")]
        OffensiveDisruptionChance,

        [Description("offensiveSlowBleedingMin")]
        OffensiveSlowBleedingMin,

        [Description("offensiveSlowBleedingChance")]
        OffensiveSlowBleedingChance,

        [Description("offensiveSlowBleedingDurationMin")]
        OffensiveSlowBleedingDurationMin,

        [Description("offensiveSlowBleedingMax")]
        OffensiveSlowBleedingMax,

        [Description("offensiveSlowBleedingModifier")]
        OffensiveSlowBleedingModifier,

        [Description("offensiveSlowDefensiveReductionDurationMin")]
        OffensiveSlowDefensiveReductionDurationMin,

        [Description("offensiveSlowDefensiveReductionMin")]
        OffensiveSlowDefensiveReductionMin,

        [Description("offensiveSlowDefensiveReductionMax")]
        OffensiveSlowDefensiveReductionMax,

        [Description("offensiveSlowFireMin")]
        OffensiveSlowFireMin,

        [Description("offensiveSlowFireDurationMin")]
        OffensiveSlowFireDurationMin,

        [Description("offensiveSlowFireModifier")]
        OffensiveSlowFireModifier,

        [Description("offensiveSlowLifeLeachDurationMin")] // yes, there seems to be a typo in the TQ db.
        OffensiveSlowLifeLeechDurationMin,

        [Description("offensiveSlowLifeLeachMin")] // yes, there seems to be a typo in the TQ db.
        OffensiveSlowLifeLeechMin,

        [Description("offensiveSlowLightningModifier")]
        OffensiveSlowLightningModifier,

        [Description("offensiveSlowManaLeachMax")]
        OffensiveSlowManaLeachMax,

        [Description("offensiveSlowManaLeachDurationMin")]
        OffensiveSlowManaLeachDurationMin,

        [Description("offensiveSlowManaLeachMin")]
        OffensiveSlowManaLeachMin,

        [Description("offensiveSlowPoisonMin")]
        OffensiveSlowPoisonMin,

        [Description("offensiveSlowPoisonDurationMin")]
        OffensiveSlowPoisonDurationMin,

        [Description("offensiveTotalDamageModifier")]
        OffensiveTotalDamageModifier,

        [Description("offensiveLifeLeechMin")]
        OffensiveLifeLeechMin,

        [Description("offensiveLifeMin")]
        OffensiveLifeMin,

        [Description("offensiveLifeModifier")]
        OffensiveLifeModifier,

        [Description("offensivePhysicalModifier")]
        OffensivePhysicalModifier,

        [Description("offensivePhysicalMin")]
        OffensivePhysicalMin,

        [Description("offensivePierceMin")]
        OffensivePierceMin,

        [Description("offensiveLightningMin")]
        OffensiveLightningMin,

        [Description("offensiveLightningModifier")]
        OffensiveLightningModifier,

        [Description("offensiveFireModifier")]
        OffensiveFireModifier,

        [Description("offensiveDisruptionMin")]
        OffensiveDisruptionMin,

        [Description("offensiveFearChance")]
        OffensiveFearChance,

        [Description("offensivePhysicalMax")]
        OffensivePhysicalMax,

        [Description("offensiveManaBurnChance")]
        OffensiveManaBurnChance,

        [Description("offensiveManaBurnDamageRatio")]
        OffensiveManaBurnDamageRatio,

        [Description("offensiveManaBurnDrainMin")]
        OffensiveManaBurnDrainMin,

        [Description("offensivePierceModifierChance")]
        OffensivePierceModifierChance,

        [Description("offensivePercentCurrentLifeMin")]
        OffensivePercentCurrentLifeMin,

        [Description("offensiveSleepModifier")]
        OffensiveSleepModifier,

        [Description("offensivePierceRatioMin")]
        OffensivePierceRationMin,

        [Description("offensiveFearMin")]
        OffensiveFearMin,

        [Description("offensiveSlowPoisonModifier")]
        OffensiveSlowPoisonModifier,

        [Description("offensiveElementalMin")]
        OffensiveElementalMin,

        [Description("offensivePierceModifier")]
        OffensivePierceModifier,

        [Description("offensiveElementalModifier")]
        OffensiveElementalModifier,

        [Description("retaliationPierceChance")]
        RetaliationPierceChance,

        [Description("retaliationPierceMin")]
        RetaliationPierceMin,

        [Description("retaliationPhysicalMin")]
        RetaliationPhysicalMin,

        [Description("retaliationPhysicalMax")]
        RetaliationPhysicalMax,

        [Description("retaliationSlowLifeChance")]
        RetaliationSlowLifeChance,

        [Description("retaliationSlowLifeMin")]
        RetaliationSlowLifeMin,

        [Description("retaliationSlowLifeDurationMin")]
        RetaliationSlowLifeDurationMin,

        [Description("retaliationSlowPoisonChance")]
        RetaliationSlowPoisonChance,

        [Description("retaliationSlowPoisonDurationMin")]
        RetaliationSlowPoisonDurationMin,

        [Description("retaliationSlowPoisonMin")]
        RetaliationSlowPoisonMin,

        [Description("skillManaCostReduction")]
        SkillManaCostReduction,

        [Description("skillCooldownReduction")]
        SkillCooldownReduction,

        [Description("skillProjectileSpeedModifier")]
        SkillProjectileSpeedModifier,

        [Description("skillName")]
        SkillName,
    }
}
