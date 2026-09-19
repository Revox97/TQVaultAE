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

        [Description("characterDefensiveBlockRecoveryReduction")]
        CharacterDefensiveBlockRecoveryReduction,

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

        [Description("characterGlobalReqReduction")]
        CharacterGlobalRequirementsReduction,

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

        [Description("characterManaLimitReserveReduction")]
        CharacterManaLimitReserveReduction,

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

        [Description("characterPhysToElementalRatio")]
        CharacterPhysicalToElementalDamageRatio,

        [Description("characterRunSpeedModifier")]
        CharacterRunSpeedModifier,

        [Description("characterShieldStrengthReqReduction")]
        CharacterShieldStrengthRequirementReduction,

        [Description("characterSpellCastSpeedModifier")]
        CharacterSpellCastSpeedModifier,

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

        [Description("defensiveAbsorption")]
        DefensiveAbsorption,

        [Description("defensiveAbsorptionModifier")]
        DefensiveAbsorptionModifier,

        [Description("defensiveBleeding")]
        DefensiveBleeding,

        [Description("defensiveBleedingDurationModifier")]
        DefensiveBleedingDurationModifier,

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

        [Description("defensivePetrify")]
        DefensivePetrify,

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

        [Description("defensiveTotalSpeedChance")]
        DefensiveTotalSpeedChance,

        [Description("defensiveTotalSpeedResistance")]
        DefensiveTotalSpeedResistance,

        [Description("defensiveTrap")]
        DefensiveTrap,

        #endregion DEFENSIVE

        #region OFFENSIVE

        [Description("offensiveBaseColdChance")]
        OffensiveBaseColdChance,

        [Description("offensiveBaseColdGlobal")]
        OffensiveBaseColdGlobal,

        [Description("offensiveBaseColdMax")]
        OffensiveBaseColdMax,

        [Description("offensiveBaseColdMin")]
        OffensiveBaseColdMin,

        [Description("offensiveBaseColdXOR")]
        OffensiveBaseColdXOR,

        [Description("offensiveBaseFireChance")]
        OffensiveBaseFireChance,

        [Description("offensiveBaseFireGlobal")]
        OffensiveBaseFireGlobal,

        [Description("offensiveBaseFireMin")]
        OffensiveBaseFireMin,

        [Description("offensiveBaseFireXOR")]
        OffensiveBaseFireXOR,

        [Description("offensiveBaseLightningChance")]
        OffensiveBaseLightningChance,

        [Description("offensiveBaseLightningGlobal")]
        OffensiveBaseLightningGlobal,

        [Description("offensiveBaseLightningMax")]
        OffensiveBaseLightningMax,

        [Description("offensiveBaseLightningMin")]
        OffensiveBaseLightningMin,

        [Description("offensiveBaseLightningXOR")]
        OffensiveBaseLightningXOR,

        [Description("offensiveBonusPhysicalMin")]
        OffensiveBonusPhysicalMin,

        [Description("offensiveBonusPhysicalMax")]
        OffensiveBonusPhysicalMax,

        [Description("offensiveColdChance")]
        OffensiveColdChance,

        [Description("offensiveColdGlobal")]
        OffensiveColdGlobal,

        [Description("offensiveColdMax")]
        OffensiveColdMax,

        [Description("offensiveColdMin")]
        OffensiveColdMin,

        [Description("offensiveColdMofifier")]
        OffensiveColdModifier,

        [Description("offensiveColdXOR")]
        OffensiveColdXOR,

        [Description("offensiveConfusionChance")]
        OffensiveConfusionChance,

        [Description("offensiveConfusionGlobal")]
        OffensiveConfusionGlobal,

        [Description("offensiveConfusionMax")]
        OffensiveConfusionMax,

        [Description("offensiveConfusionMin")]
        OffensiveConfusionMin,

        [Description("offensiveConvertGlobal")]
        OffensiveConvertGlobal,

        [Description("offensiveConvertMin")]
        OffensiveConvertMin,

        [Description("offensiveDisruptionChance")]
        OffensiveDisruptionChance,

        [Description("offensiveDisruptionGlobal")]
        OffensiveDisruptionGlobal,

        [Description("offensiveDisruptionMin")]
        OffensiveDisruptionMin,

        [Description("offensiveElementalMax")]
        OffensiveElementalMax,

        [Description("offensiveElementalMin")]
        OffensiveElementalMin,

        [Description("offensiveElementalModifier")]
        OffensiveElementalModifier,

        [Description("offensiveElementalModifierChance")]
        OffensiveElementalModifierChance,

        [Description("offensiveFearGlobal")]
        OffensiveFearGlobal,

        [Description("offensiveFearChance")]
        OffensiveFearChance,

        [Description("offensiveFearMax")]
        OffensiveFearMax,

        [Description("offensiveFearMin")]
        OffensiveFearMin,

        [Description("offensiveFireChance")]
        OffensiveFireChance,

        [Description("offensiveFireGlobal")]
        OffensiveFireGlobal,

        [Description("offensiveFireMax")]
        OffensiveFireMax,

        [Description("offensiveFireMin")]
        OffensiveFireMin,

        [Description("offensiveFireModifier")]
        OffensiveFireModifier,

        [Description("offensiveFireXOR")]
        OffensiveFireXOR,

        [Description("offensiveFreezeChance")]
        OffensiveFreezeChance,

        [Description("offensiveFreezeMax")]
        OffensiveFreezeMax,

        [Description("offensiveFreezeMin")]
        OffensiveFreezeMin,

        [Description("offensiveGlobalChance")]
        OffensiveGlobalChance,

        [Description("offensiveLifeLeechMin")]
        OffensiveLifeLeechMin,

        [Description("offensiveLifeMin")]
        OffensiveLifeMin,

        [Description("offensiveLifeModifier")]
        OffensiveLifeModifier,

        [Description("offensiveLifeMofifierChance")]
        OffensiveLifeModifierChance,

        [Description("offensiveLightningChance")]
        OffensiveLightningChance,


        [Description("offensiveLightningGlobal")]
        OffensiveLightningGlobal,

        [Description("offensiveLightningMax")]
        OffensiveLightningMax,

        [Description("offensiveLightningMin")]
        OffensiveLightningMin,

        [Description("offensiveLightningModifier")]
        OffensiveLightningModifier,

        [Description("offensiveLightningXOR")]
        OffensiveLightningXOR,

        [Description("offensiveManaBurnChance")]
        OffensiveManaBurnChance,

        [Description("offensiveManaBurnDamageRatio")]
        OffensiveManaBurnDamageRatio,

        [Description("offensiveManaBurnDrainMax")]
        OffensiveManaBurnDrainMax,

        [Description("offensiveManaBurnDrainMin")]
        OffensiveManaBurnDrainMin,

        [Description("offensivePercentCurrentLifeChance")]
        OffensivePercentCurrentLifeChance,

        [Description("offensivePercentCurrentLifeMax")]
        OffensivePercentCurrentLifeMax,

        [Description("offensivePercentCurrentLifeMin")]
        OffensivePercentCurrentLifeMin,

        [Description("offensivePetrifyChance")]
        OffensivePetrifyChance,

        [Description("offensivePetrifyGlobal")]
        OffensivePetrifyGlobal,

        [Description("offensivePetrifyMax")]
        OffensivePetrifyMax,

        [Description("offensivePetrifyMin")]
        OffensivePetrifyMin,

        [Description("offensivePierceChance")]
        OffensivePierceChance,

        [Description("offensivePierceGlobal")]
        OffensivePierceGlobal,

        [Description("offensivePierceMax")]
        OffensivePierceMax,

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

        [Description("offensiveSleepGlobal")]
        OffensiveSleepGlobal,

        [Description("offensiveSleepMax")]
        OffensiveSleepMax,

        [Description("offensiveSleepMin")]
        OffensiveSleepMin,

        [Description("offensiveSleepModifier")]
        OffensiveSleepModifier,

        [Description("offensiveStunChance")]
        OffensiveStunChance,

        [Description("offensiveStunMax")]
        OffensiveStunMax,

        [Description("offensiveStunMin")]
        OffensiveStunMin,

        [Description("offensiveStunModifier")]
        OffensiveStunModifier,

        [Description("offensiveTotalDamageModifier")]
        OffensiveTotalDamageModifier,

        [Description("offensiveTotalDamageModifierChance")]
        OffensiveTotalDamageModifierChance,

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

        #region OFFENSIVE_SLOW

        [Description("offensiveSlowBleedingChance")]
        OffensiveSlowBleedingChance,

        [Description("offensiveSlowBleedingDurationMax")]
        OffensiveSlowBleedingDurationMax,

        [Description("offensiveSlowBleedingDurationModifier")]
        OffensiveSlowBleedingDurationModifier,

        [Description("offensiveSlowBleedingGlobal")]
        OffensiveSlowBleedingGlobal,

        [Description("offensiveSlowBleedingDurationMin")]
        OffensiveSlowBleedingDurationMin,

        [Description("offensiveSlowBleedingMax")]
        OffensiveSlowBleedingMax,

        [Description("offensiveSlowBleedingMin")]
        OffensiveSlowBleedingMin,

        [Description("offensiveSlowBleedingModifier")]
        OffensiveSlowBleedingModifier,

        [Description("offensiveSlowBleedingModifierChance")]
        OffensiveSlowBleedingModifierChance,

        [Description("offensiveSlowColdDurationMin")]
        OffensiveSlowColdDurationMin,

        [Description("offensiveSlowColdDurationModifier")]
        OffensiveSlowColdDurationModifier,

        [Description("offensiveSlowColdMax")]
        OffensiveSlowColdMax,

        [Description("offensiveSlowColdMin")]
        OffensiveSlowColdMin,

        [Description("offensiveSlowColdModifier")]
        OffensiveSlowColdModifier,

        [Description("offensiveSlowDefensiveReductionDurationMin")]
        OffensiveSlowDefensiveReductionDurationMin,

        [Description("offensiveSlowDefensiveReductionMin")]
        OffensiveSlowDefensiveReductionMin,

        [Description("offensiveSlowDefensiveReductionMax")]
        OffensiveSlowDefensiveReductionMax,

        [Description("offensiveSlowDefensiveReductionModifier")]
        OffensiveSlowDefensiveReductionModifier,

        [Description("offensiveSlowDefensiveReductionModifierChance")]
        OffensiveSlowDefensiveReductionModifierChance,

        [Description("offensiveSlowFireDurationMin")]
        OffensiveSlowFireDurationMin,

        [Description("offensiveSlowFireDurationModifier")]
        OffensiveSlowFireDurationModifier,

        [Description("offensiveSlowFireMax")]
        OffensiveSlowFireMax,

        [Description("offensiveSlowFireMin")]
        OffensiveSlowFireMin,

        [Description("offensiveSlowFireModifier")]
        OffensiveSlowFireModifier,

        [Description("offensiveSlowLifeLeachDurationMax")]
        OffensiveSlowLifeLeechDurationMax,

        [Description("offensiveSlowLifeLeachDurationMin")]
        OffensiveSlowLifeLeechDurationMin,

        [Description("offensiveSlowLifeLeachDurationModifier")]
        OffensiveSlowLifeLeechDurationModifier,

        [Description("offensiveSlowLifeLeachMax")]
        OffensiveSlowLifeLeechMax,

        [Description("offensiveSlowLifeLeachMin")]
        OffensiveSlowLifeLeechMin,

        [Description("offensiveSlowLifeLeachModifier")]
        OffensiveSlowLifeLeachModifier,

        [Description("offensiveSlowLightningDurationMin")]
        OffensiveSlowLightningDurationMin,

        [Description("offensiveSlowLightningDurationModifier")]
        OffensiveSlowLightningDurationModifier,

        [Description("offensiveSlowLightningGlobal")]
        OffensiveSlowLightningGlobal,

        [Description("offensiveSlowLightningMax")]
        OffensiveSlowLightningMax,

        [Description("offensiveSlowLightningMin")]
        OffensiveSlowLightningMin,

        [Description("offensiveSlowLightningModifier")]
        OffensiveSlowLightningModifier,

        [Description("offensiveSlowManaLeachDurationMin")]
        OffensiveSlowManaLeachDurationMin,

        [Description("offensiveSlowManaLeachMin")]
        OffensiveSlowManaLeachMin,

        [Description("offensiveSlowManaLeachMax")]
        OffensiveSlowManaLeachMax,

        [Description("offensiveSlowManaLeachModifier")]
        OffensiveSlowManaLeachModifier,

        [Description("offensiveSlowOffensiveAbilityDurationMin")]
        OffensiveSlowOffensiveAbilityDurationMin,

        [Description("offensiveSlowOffensiveAbilityMin")]
        OffensiveSlowOffensiveAbilityMin,

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

        [Description("offensiveSlowRunSpeedDurationMin")]
        OffensiveSlowRunSpeedDurationMin,

        [Description("offensiveSlowRunSpeedGlobal")]
        OffensiveSlowRunSpeedGlobal,

        [Description("offensiveSlowRunSpeedMax")]
        OffensiveSlowRunSpeedMax,

        [Description("offensiveSlowRunSpeedMin")]
        OffensiveSlowRunSpeedMin,

        [Description("offensiveSlowTotalSpeedDurationMin")]
        OffensiveSlowTotalSpeedDurationMin,

        [Description("offensiveSlowTotalSpeedGlobal")]
        OffensiveSlowTotalSpeedGlobal,

        [Description("offensiveSlowTotalSpeedMin")]
        OffensiveSlowTotalSpeedMin,

        #endregion OFFENSIVE_SLOW

        #region RETALIATION

        [Description("retaliationColdChance")]
        RetaliationColdChance,

        [Description("retaliationColdGlobal")]
        RetaliationColdGlobal,

        [Description("retaliationColdMax")]
        RetaliationColdMax,

        [Description("retaliationColdMin")]
        RetaliationColdMin,

        [Description("retaliationElementalChance")]
        RetaliationElementalChance,

        [Description("retaliationElementalMax")]
        RetaliationElementalMax,

        [Description("retaliationElementalMin")]
        RetaliationElementalMin,

        [Description("retaliationFireChance")]
        RetaliationFireChance,

        [Description("retaliationFireGlobal")]
        RetaliationFireGlobal,

        [Description("retaliationFireMax")]
        RetaliationFireMax,

        [Description("retaliationFireMin")]
        RetaliationFireMin,

        [Description("retaliationGlobalChance")]
        RetaliationGlobalChance,

        [Description("retaliationLightningChance")]
        RetaliationLightningChance,

        [Description("retaliationLightningMax")]
        RetaliationLightningMax,

        [Description("retaliationLightningMin")]
        RetaliationLightningMin,

        [Description("retaliationPierceChance")]
        RetaliationPierceChance,

        [Description("retaliationPierceGlobal")]
        RetaliationPierceGlobal,

        [Description("retaliationPierceMin")]
        RetaliationPierceMin,

        [Description("retaliationPierceMax")]
        RetaliationPierceMax,

        [Description("retaliationPierceModifier")]
        RetaliationPierceModifier,

        [Description("retaliationPhysicalMax")]
        RetaliationPhysicalMax,

        [Description("retaliationPhysicalMin")]
        RetaliationPhysicalMin,

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

        #region RETALIATION_SLOW

        [Description("retaliationSlowAttackSpeedDurationMin")]
        RetaliationSlowAttackSpeedDurationMin,

        [Description("retaliationSlowAttackSpeedGlobal")]
        RetaliationSlowAttackSpeedGlobal,

        [Description("retaliationSlowAttackSpeedMax")]
        RetaliationSlowAttackSpeedMax,

        [Description("retaliationSlowAttackSpeedMin")]
        RetaliationSlowAttackSpeedMin,

        [Description("retaliationSlowBleedingChance")]
        RetaliationSlowBleedingChance,

        [Description("retaliationSlowBleedingGlobal")]
        RetaliationSlowBleedingGlobal,

        [Description("retaliationSlowBleedingDurationMin")]
        RetaliationSlowBleedingDurationMin,

        [Description("retaliationSlowFireDurationMin")]
        RetaliationSlowFireDurationMin,

        [Description("retaliationSlowFireMax")]
        RetaliationSlowFireMax,

        [Description("retaliationSlowFireMin")]
        RetaliationSlowFireMin,

        [Description("retaliationSlowBleedingMin")]
        RetaliationSlowBleedingMin,

        [Description("retaliationSlowLifeChance")]
        RetaliationSlowLifeChance,

        [Description("retaliationSlowLifeDurationMin")]
        RetaliationSlowLifeDurationMin,

        [Description("retaliationSlowLifeLeachChance")]
        RetaliationSlowLifeLeachChance,

        [Description("retaliationSlowLifeLeachDurationMin")]
        RetaliationSlowLifeLeachDurationMin,

        [Description("retaliationSlowLifeLeachGlobal")]
        RetaliationSlowLifeLeachGlobal,

        [Description("retaliationSlowLifeLeachMax")]
        RetaliationSlowLifeLeachMax,

        [Description("retaliationSlowLifeLeachMin")]
        RetaliationSLowLifeLeachMin,

        [Description("retaliationSlowLifeLeachXOR")]
        RetaliationSlowLifeLeachXOR,

        [Description("retaliationSlowLifeMin")]
        RetaliationSlowLifeMin,

        [Description("retaliationSlowLightningChance")]
        RetaliationSlowLightningChance,

        [Description("retaliationSlowLightningDurationMin")]
        RetaliationSlowLightningDurationMin,

        [Description("retaliationSlowLightningMin")]
        RetaliationSlowLightningMin,

        [Description("retaliationSlowManaLeachChance")]
        RetaliationSlowManaLeachChance,

        [Description("retaliationSlowManaLeachDurationMin")]
        RetaliationSlowManaLeachDurationMin,

        [Description("retaliationSlowManaLeachGlobal")]
        RetaliationSlowManaLeachGlobal,

        [Description("retaliationSlowManaLeachMax")]
        RetaliationSlowManaLeachMax,

        [Description("retaliationSlowManaLeachMin")]
        RetaliationSlowManaLeachMin,

        [Description("retaliationSlowManaLeachXOR")]
        RetaliationSlowManaLeachXOR,

        [Description("retaliationSlowOffensiveAbilityDurationMin")]
        RetaliationSlowOffensiveAbilityDurationMin,

        [Description("retaliationSlowOffensiveAbilityMin")]
        RetaliationSlowOffensiveAbilityMin,

        [Description("retaliationSlowOffensiveReductionChance")]
        RetaliationSlowOffensiveReductionChance,

        [Description("retaliationSlowOffensiveReductionDurationMin")]
        RetaliationSlowOffensiveReductionDurationMin,

        [Description("retaliationSlowOffensiveReductionMin")]
        RetaliationSlowOffensiveReductionMin,

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

        [Description("retaliationSlowRunSpeedDurationMin")]
        RetaliationSlowRunSpeedDurationMin,

        [Description("retaliationSlowRunSpeedDurationMax")]
        RetaliationSlowRunSpeedDurationMax,

        [Description("retaliationSlowRunSpeedGlobal")]
        RetaliationSlowRunSpeedGlobal,

        [Description("retaliationSlowRunSpeedMax")]
        RetaliationSlowRunSpeedMax,

        [Description("retaliationSlowRunSpeedMin")]
        RetaliationSlowRunSpeedMin,

        #endregion RETALIATION_SLOW

        #region SKILL

        [Description("skillCooldownReduction")]
        SkillCooldownReduction,

        [Description("skillManaCostReduction")]
        SkillManaCostReduction,

        [Description("skillManaCostReductionChance")]
        SkillManaCostReductionChance,

        [Description("skillName")]
        SkillName,

        [Description("skillProjectileSpeedModifier")]
        SkillProjectileSpeedModifier,

        #endregion SKILL
    }
}
