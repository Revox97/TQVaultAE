namespace TQVaultAE.Model.Items.ItemProperties
{
    public class ItemPropertyTagCollection
    {
        private static readonly Dictionary<string, string> s_nameToTagMap = new()
        {
            { "Chance", "ChanceOfTag" },

            //{ "characterAttackSpeed", "CharacterAttackSpeedModifier" },
            { "characterAttackSpeedModifier", "CharacterAttackSpeedModifier" },
            { "characterDefensiveAbility", "CharacterDefensiveAbility" },
            { "characterDeflectProjectile", "CharacterDeflectProjectiles" },
            { "characterDexterity", "CharacterDexterity" },
            { "characterDodgePercent", "CharacterDodgePercent" },
            { "characterEnergyAbsorptionPercent", "CharacterEnergyAbsorptionPercent" },
            { "characterGlobalReqReduction", "CharcterItemGlobalReduction" }, // Typo in game file, do not "fix"
            //{ "characterIncreasedExperience", "CharacterIncreasedExperience" },
            { "characterIntelligence", "CharacterIntelligence" },
            { "characterLife", "CharacterLife" },
            { "characterLifeModifier", "CharacterLifeModifier" },
            //{ "characterLifeRegen", "CharacterLifeRegen" },
            { "characterLifeRegenModifier", "CharacterLifeRegenModifier" },
            { "characterOffensiveAbility", "CharacterOffensiveAbility" },
            { "characterOffensiveAbilityModifier", "CharacterOffensiveAbilityModifier" },
            { "characterMana", "CharacterMana" },
            { "characterManaRegen", "CharacterManaRegen" },
            { "characterManaRegenModifier", "CharacterManaRegenModifier" },
            //{ "characterRunSpeed", "CharacterRunSpeedModifier" },
            { "characterRunSpeedModifier", "CharacterRunSpeedModifier" },
            //{ "characterTotalSpeed", "CharacterTotalSpeedModifier" },
            { "characterTotalSpeedModifier", "CharacterTotalSpeedModifier" },
            { "characterStrength", "CharacterStrength" },
            { "characterStrengthModifier", "CharacterStrengthModifier" },
            { "characterWeaponStrengthReqReduction", "CharacterWeaponStrengthReqReduction" },

            { "defensiveBleeding", "DefenseBleeding" },
            //{ "defensiveBlock", "DefenseBlock" },
            { "defensiveCold", "DefenseCold" },
            { "defensiveDisruption", "DefenseDisruption" },
            { "defensiveElementalResistance", "DefenseElementalResistance" },
            { "defensiveFire", "DefenseFire" },
            { "defensiveLife", "DefenseLife" },
            { "defensiveLightning", "DefenseLightning" },
            { "defensivePierce", "DefensePierce" },
            { "defensivePhysical", "DefensePhysical" },
            { "defensivePhysicalChance", "ChanceOfTag" },
            { "defensivePoison", "DefensePoison" },
            { "defensiveProtection", "DefenseAbsorptionProtection" },
            { "defensiveProtectionChance", "ChanceOfTag" },
            { "defensiveReflect", "DefenseReflect" },
            { "defensiveReflectChance", "ChanceOfTag" },
            //{ "defensiveSleep", "DefenseSleep" },
            { "defensiveStun", "DefenseStun" },
            { "defensiveTotalSpeedResistance", "DefenseTotalSpeedResistance" },

            { "defensiveSlowLifeLeach", "DefenseLifeLeach" },
            { "defensiveSlowManaLeach", "DefenseManaLeach" },

            { "offensiveBaseLightning", "tagDamageBaseLightning" },

            { "offensiveCold", "DamageCold" },
            { "offensiveColdModifier", "DamageModifierCold" },
            { "offensiveConfusion", "DamageConfusion" },
            { "offensiveConfusionChance", "ChanceOfTag" },
            { "offensiveConvert", "DamageConvert" },
            { "offensiveDisruption", "DamageDisruption" },
            { "offensiveFear", "DamageFear" },
            { "offensiveFearChance", "ChanceOfTag" },
            { "offensiveLife", "DamageLife" },
            { "offensiveLifeLeech", "DamageLifeLeech" },
            { "offensiveLightning", "DamageLightning" },
            { "offensiveLightningModifier", "DamageModifierLightning" },
            { "offensivePetrify", "DamagePetrify" },
            { "offensivePhysical", "DamagePhysical" },
            { "offensivePhysicalModifier", "DamageModifierPhysical" },
            { "offensivePierceModifier", "DamageModifierPierce" },
            { "offensivePierceRatio", "DamageBasePierceRatio" },
            { "offensiveTotalDamage", "<<TOTAL>>" },
            { "offensiveGlobalChance", "ChanceOfTag" },
            { "offensivePercentCurrentLife", "DamagePercentCurrentLife" },
            { "offensivePercentCurrentLifeChance", "ChanceOfTag" },
            { "offensivePierce", "DamagePierce" },
            { "offensiveStun", "DamageStun" },
            { "offensiveTotalDamageReductionPercent", "DamageTotalDamageReductionPercent" },
            { "offensiveTotalDamageReductionPercentChance", "ChanceOfTag" },

            { "offensiveSlowBleeding", "DamageDurationBleeding" },
            { "offensiveSlowBleedingModifier", "DamageDurationModifierBleeding" },
            { "offensiveSlowBleedingDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowCold", "DamageDurationCold" },
            { "offensiveSlowColdModifier", "DamageDurationModifierCold" },
            { "offensiveSlowLightning", "DamageDurationLightning" },
            { "offensiveSlowLightningDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowLifeLeach", "DamageDurationLifeLeach" },
            { "offensiveSlowLifeLeachDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowLifeLeachModifier", "DamageDurationModifierLifeLeach" },
            { "offensiveSlowLightningModifier", "DamageDurationModifierLightning" },
            { "offensiveSlowManaLeach", "DamageDurationManaLeach" },
            { "offensiveSlowManaLeachModifier", "DamageDurationModifierManaLeach" },
            { "offensiveSlowManaLeachDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowOffensiveAbility", "DamageDurationOffensiveAbility" },
            { "offensiveSlowOffensiveAbilityDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowPoison", "DamageDurationPoison" },

            // TODO Extra property, how to handle this with the current regex?
            { "offensiveSlowPoisonDuration", "DamageDurationModifierPoison" },
            { "offensiveSlowPoisonDurationModifier", "ImprovedTimeFormat" },

            { "offensiveSlowPoisonDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowPoisonModifier", "DamageDurationModifierPoison" },

            { "retaliationElemental", "RetaliationElemental" },
            { "retaliationFire", "RetaliationFire" },
            { "retaliationGlobalChance", "ChanceOfTag" },
            { "retaliationPhysical", "RetaliationPhysical" },
            { "retaliationStun", "RetaliationStun" },

            { "retaliationSlowLife", "RetaliationDurationLife" },
            { "retaliationSlowLifeDurationMin", "DamageSingleFormatTime" },
            { "retaliationSlowLifeLeach", "RetaliationDurationLifeLeach" },
            { "retaliationSlowLifeLeachChance", "ChanceOfTag" },
            { "retaliationSlowLifeLeachDurationMin", "DamageSingleFormatTime" },
            { "retaliationSlowOffensiveAbility", "RetaliationDurationOffensiveAbility" },
            { "retaliationSlowOffensiveAbilityDurationMin", "DamageSingleFormatTime" },
            { "retaliationSlowRunSpeed", "RetaliationDurationRunSpeed" },
            { "retaliationSlowRunSpeedDurationMin", "DamageSingleFormatTime" },

            { "skillCooldownReduction", "SkillCooldownReduction" },
            //{ "skillProjectileSpeed", "SkillProjectileSpeedModifier" },

            {"RangeFormat", "DamageRangeFormat" }
        };

        public string this[string name]
        {
            get
            {
                return s_nameToTagMap.TryGetValue(name, out string? result) && result is not null
                    ? result : name;
            }
        }
    }
}
