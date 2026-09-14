using System.ComponentModel;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Model.Enumerations
{
    /// <summary>
    /// Represents the type of an <see cref="ItemProperty"/>.
    /// </summary>
    public enum ItemPropertyType
    {
        #region CHARACTER

        [Description("characterArmorAndDexterityReqReduction")]
        CharacterArmorAndDexterityRequirementsReduction,

        [Description("characterArmorDexterityReqReduction")]
        CharacterArmorDexterityRequirementsReduction,

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

        [Description("characterDefensiveAbilityModifier")]
        CharacterDefensiveAbilityModifier,

        [Description("characterDeflectProjectile")]
        CharacterDeflectProjectile,

        [Description("characterDexterity")]
        CharacterDexterity,

        [Description("characterDexterityModifier")]
        CharacterDexterityModifier,

        [Description("characterDodgePercent")]
        CharacterDodgePercent,

        [Description("characterEnergyAbsorptionPercent")]
        CharacterEnergyAbsorptionPercent,

        [Description("characterIncreasedExperience")]
        CharacterIncreasedExperience,

        [Description("characterIntelligence")]
        CharacterIntelligence,

        [Description("characterIntelligenceModifier")]
        CharacterIntelligenceModifier,

        [Description("characterLife")]
        CharacterLife,

        [Description("characterLifeModifier")]
        CharacterLifeModifier,

        [Description("characterLifeRegen")]
        CharacterLifeRegen,

        [Description("characterLifeRegenModifier")]
        CharacterLifeRegenModifier,

        [Description("characterMana")]
        CharacterMana,

        [Description("characterManaModifier")]
        CharacterManaModifier,

        [Description("characterManaRegen")]
        CharacterManaRegen,

        [Description("characterManaRegenModifier")]
        CharacterManaRegenModifier,

        [Description("characterOffensiveAbility")]
        CharacterOffensiveAbility,

        [Description("characterOffensiveAbilityModifier")]
        CharacterOffensiveAbilityModifier,

        [Description("characterShieldStrengthReqReduction")]
        CharacterShieldStrengthRequirementReduction,

        [Description("characterStrength")]
        CharacterStrength,

        [Description("characterStrengthModifier")]
        CharacterStrengthModifier,

        [Description("characterTotalSpeedModifier")]
        CharacterTotalSpeedModifier,

        [Description("characterWeaponDexterityReqReduction")]
        CharacterWeaponDexterityRequirementsReduction,

        [Description("characterWeaponStrengthReqReduction")]
        CharacterWeaponStrengthRequirementReduction,

        #endregion CHARACTER

        #region DEFENSIVE

        [Description("defensiveBleeding")]
        DefensiveBleeding,

        [Description("defensiveBlock")]
        DefensiveBlock,

        [Description("defensiveBlockChance")]
        DefensiveBlockChance,

        [Description("defensiveBlockModifier")]
        DefensiveBlockModifier,

        [Description("defensiveBlockModifierChance")]
        DefensiveBlockModifierChance,

        [Description("defensiveCold")]
        DefensiveCold,

        [Description("defensiveDisruption")]
        DefensiveDisruption,

        [Description("defensiveElementalResistance")]
        DefensiveElementalResistance,

        [Description("defensiveFire")]
        DefensiveFire,

        [Description("defensiveFreeze")]
        DefensiveFreeze,

        [Description("defensiveGlobalChance")]
        DefensiveGlobalChance,

        [Description("defensiveLife")]
        DefensiveLife,

        [Description("defensiveLightning")]
        DefensiveLightning,

        [Description("defensivePhysical")]
        DefensivePhysical,

        [Description("defensivePhysicalChance")]
        DefensivePhysicalChance,

        [Description("defensivePierce")]
        DefensivePierce,

        [Description("defensivePoison")]
        DefensivePoison,

        [Description("defensiveProtection")]
        DefensiveProtection,

        [Description("defensiveProtectionModifier")]
        DefensiveProtectionModifier,

        [Description("defensiveReflect")]
        DefensiveReflect,

        [Description("defensiveReflectChance")]
        DefensiveReflectChance,

        [Description("defensiveSleep")]
        DefensiveSleep,

        [Description("defensiveSlowLifeLeach")]
        DefensiveSlowLifeLeach,

        [Description("defensiveSlowManaLeach")]
        DefensiveSlowManaLeach,

        [Description("defensiveStun")]
        DefensiveStun,

        [Description("defensiveTotalSpeedResistance")]
        DefensiveTotalSpeedResistance,

        [Description("defensiveTrap")]
        DefensiveTrap,

        #endregion DEFENSIVE

        #region OFFENSIVE

        [Description("offensiveBonusPhysicalMin")]
        OffensiveBonusPhysicalMin,

        [Description("offensiveBonusPhysicalMax")]
        OffensiveBonusPhysicalMax,

        [Description("offensiveColdMin")]
        OffensiveColdMin,

        [Description("offensiveConfusionChance")]
        OffensiveConfusionChance,

        [Description("offensiveConfusionMax")]
        OffensiveConfusionMax,

        [Description("offensiveFireMin")]
        OffensiveFireMin,

        [Description("offensiveDisruptionChance")]
        OffensiveDisruptionChance,

        [Description("offensiveSlowBleedingChance")]
        OffensiveSlowBleedingChance,

        [Description("offensiveSlowBleedingDurationMin")]
        OffensiveSlowBleedingDurationMin,

        [Description("offensiveSlowBleedingMin")]
        OffensiveSlowBleedingMin,

        [Description("offensiveSlowBleedingMax")]
        OffensiveSlowBleedingMax,

        [Description("offensiveFearGlobal")]
        OffensiveFearGlobal,

        [Description("offensiveFearMax")]
        OffensiveFearMax,

        [Description("offensiveLifeMofifierChance")]
        OffensiveLifeModifierChance,

        [Description("offensiveSlowBleedingModifier")]
        OffensiveSlowBleedingModifier,

        [Description("offensiveColdMofifier")]
        OffensiveColdModifier,

        [Description("offensiveConfusionGlobal")]
        OffensiveConfusionGlobal,

        [Description("offensiveConfusionMin")]
        OffensiveConfusionMin,

        [Description("offensiveConvertGlobal")]
        OffensiveConvertGlobal,

        [Description("offensiveConvertMin")]
        OffensiveConvertMin,

        [Description("offensiveDisruptionGlobal")]
        OffensiveDisruptionGlobal,

        [Description("offensiveDisruptionMin")]
        OffensiveDisruptionMin,

        [Description("offensiveElementalMin")]
        OffensiveElementalMin,

        [Description("offensiveElementalModifier")]
        OffensiveElementalModifier,

        [Description("offensiveFearChance")]
        OffensiveFearChance,

        [Description("offensiveFearMin")]
        OffensiveFearMin,

        [Description("offensiveFireMax")]
        OffensiveFireMax,

        [Description("offensiveFireModifier")]
        OffensiveFireModifier,

        [Description("offensiveGlobalChance")]
        OffensiveGlobalChance,

        [Description("offensiveLifeLeechMin")]
        OffensiveLifeLeechMin,

        [Description("offensiveLifeMin")]
        OffensiveLifeMin,

        [Description("offensiveLifeModifier")]
        OffensiveLifeModifier,

        [Description("offensiveLightningMax")]
        OffensiveLightningMax,

        [Description("offensiveLightningMin")]
        OffensiveLightningMin,

        [Description("offensiveLightningModifier")]
        OffensiveLightningModifier,

        [Description("offensiveManaBurnChance")]
        OffensiveManaBurnChance,

        [Description("offensiveManaBurnDamageRatio")]
        OffensiveManaBurnDamageRatio,

        [Description("offensiveManaBurnDrainMin")]
        OffensiveManaBurnDrainMin,

        [Description("offensivePercentCurrentLifeChance")]
        OffensivePercentCurrentLifeChance,

        [Description("offensivePercentCurrentLifeMin")]
        OffensivePercentCurrentLifeMin,

        [Description("offensivePierceMin")]
        OffensivePierceMin,

        [Description("offensivePierceModifier")]
        OffensivePierceModifier,

        [Description("offensivePierceModifierChance")]
        OffensivePierceModifierChance,

        [Description("offensivePierceRatioMin")]
        OffensivePierceRationMin,

        [Description("offensivePhysicalGlobal")]
        OffensivePhysicalGlobal,

        [Description("offensivePhysicalMax")]
        OffensivePhysicalMax,

        [Description("offensivePhysicalMin")]
        OffensivePhysicalMin,

        [Description("offensivePhysicalModifier")]
        OffensivePhysicalModifier,

        [Description("offensiveSleepModifier")]
        OffensiveSleepModifier,

        [Description("offensiveSlowColdModifier")]
        OffensiveSlowColdModifier,

        [Description("offensiveSlowDefensiveReductionDurationMin")]
        OffensiveSlowDefensiveReductionDurationMin,

        [Description("offensiveSlowDefensiveReductionMin")]
        OffensiveSlowDefensiveReductionMin,

        [Description("offensiveSlowDefensiveReductionMax")]
        OffensiveSlowDefensiveReductionMax,

        [Description("offensiveSlowFireDurationMin")]
        OffensiveSlowFireDurationMin,

        [Description("offensiveSlowFireMin")]
        OffensiveSlowFireMin,

        [Description("offensiveSlowFireModifier")]
        OffensiveSlowFireModifier,

        [Description("offensiveSlowLifeLeachDurationMin")] 
        OffensiveSlowLifeLeechDurationMin,

        [Description("offensiveSlowLifeLeachMin")]
        OffensiveSlowLifeLeechMin,

        [Description("offensiveSlowLifeLeachModifier")]
        OffensiveSlowLifeLeachModifier,

        [Description("offensiveSlowLightningModifier")]
        OffensiveSlowLightningModifier,

        [Description("offensiveSlowManaLeachDurationMin")]
        OffensiveSlowManaLeachDurationMin,

        [Description("offensiveSlowManaLeachMin")]
        OffensiveSlowManaLeachMin,

        [Description("offensiveSlowManaLeachMax")]
        OffensiveSlowManaLeachMax,

        [Description("offensiveSlowPoisonDurationMin")]
        OffensiveSlowPoisonDurationMin,

        [Description("offensiveSlowPoisonDurationModifier")]
        OffensiveSlowPoisonDurationModifier,

        [Description("offensiveSlowPoisonMax")]
        OffensiveSlowPoisonMax,

        [Description("offensiveSlowPoisonMin")]
        OffensiveSlowPoisonMin,

        [Description("offensiveSlowPoisonModifier")]
        OffensiveSlowPoisonModifier,

        [Description("offensiveTotalDamageModifier")]
        OffensiveTotalDamageModifier,

        [Description("offensiveTotalDamageReductionPercentChance")]
        OffensiveTotalDamageReductionPercentChance,

        [Description("offensiveTotalDamageReductionPercentDurationMin")]
        OffensiveTotalDamageReductionPercentDurationMin,

        [Description("offensiveTotalDamageReductionPercentGlobal")]
        OffensiveTotalDamageReductionPercentGlobal,

        [Description("offensiveTotalDamageReductionPercentMin")]
        OffensiveTotalDamageReductionPercentMin,

        [Description("offensiveTotalResistanceReductionAbsoluteChance")]
        OffensiveTotalResistanceReductionAbsoluteChance,

        [Description("offensiveTotalResistanceReductionAbsoluteDurationMin")]
        OffensiveTotalResistanceReductionAbsoluteDurationMin,

        [Description("offensiveTotalResistanceReductionAbsoluteMin")]
        OffensiveTotalResistanceReductionAbsoluteMin,

        #endregion OFFENSIVE

        #region RETALIATION

        [Description("retaliationElementalChance")]
        RetaliationElementalChance,

        [Description("retaliationElementalMin")]
        RetaliationElementalMin,

        [Description("retaliationFireGlobal")]
        RetaliationFireGlobal,

        [Description("retaliationFireMax")]
        RetaliationFireMax,

        [Description("retaliationFireMin")]
        RetaliationFireMin,

        [Description("retaliationGlobalChance")]
        RetaliationGlobalChance,

        [Description("retaliationPierceChance")]
        RetaliationPierceChance,

        [Description("retaliationPierceMin")]
        RetaliationPierceMin,

        [Description("retaliationPierceMax")]
        RetaliationPierceMax,

        [Description("retaliationPhysicalMax")]
        RetaliationPhysicalMax,

        [Description("retaliationPhysicalMin")]
        RetaliationPhysicalMin,

        [Description("retaliationSlowLifeChance")]
        RetaliationSlowLifeChance,

        [Description("retaliationSlowLifeDurationMin")]
        RetaliationSlowLifeDurationMin,

        [Description("retaliationSlowLifeLeachChance")]
        RetaliationSlowLifeLeachChance,

        [Description("retaliationSlowLifeLeachDurationMin")]
        RetaliationSlowLifeLeachDurationMin,

        [Description("retaliationSlowLifeLeachMin")]
        RetaliationSLowLifeLeachMin,

        [Description("retaliationSlowLifeMin")]
        RetaliationSlowLifeMin,

        [Description("retaliationSlowOffensiveAbilityDurationMin")]
        RetaliationSlowOffensiveAbilityDurationMin,

        [Description("retaliationSlowOffensiveAbilityMin")]
        RetaliationSlowOffensiveAbilityMin,

        [Description("retaliationSlowPoisonChance")]
        RetaliationSlowPoisonChance,

        [Description("retaliationSlowPoisonDurationMin")]
        RetaliationSlowPoisonDurationMin,

        [Description("retaliationSlowPoisonGlobal")]
        RetaliationSlowPoisonGlobal,

        [Description("retaliationSlowPoisonMin")]
        RetaliationSlowPoisonMin,

        [Description("retaliationSlowPoisonXOR")]
        RetaliationSlowPoisonXOR,

        [Description("retaliationStunChance")]
        RetaliationStunChance,

        [Description("retaliationStunGlobal")]
        RetaliationStunGlobal,

        [Description("retaliationStunMax")]
        RetaliationStunMax,

        [Description("retaliationStunMin")]
        RetaliationStunMin,

        [Description("retaliationStunXOR")]
        RetaliationStunXOR,

        #endregion RETALIATION

        #region SKILL

        [Description("skillCooldownReduction")]
        SkillCooldownReduction,

        [Description("skillManaCostReduction")]
        SkillManaCostReduction,

        [Description("skillName")]
        SkillName,

        [Description("skillProjectileSpeedModifier")]
        SkillProjectileSpeedModifier,

        #endregion SKILL
    }
}
