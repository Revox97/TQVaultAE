namespace TQVaultAE.Model.Items.ItemProperties
{
    public class ItemPropertyTagCollection
    {
        private static readonly Dictionary<string, string> s_nameToTagMap = new()
        {
            { "characterAttackSpeed", "CharacterAttackSpeedModifier" },
            { "characterDefensiveAbility", "CharacterDefensiveAbility" },
            { "characterDexterity", "CharacterDexterity" },
            { "characterEnergyAbsorptionPercent", "CharacterEnergyAbsorptionPercent" },
            { "characterGlobalReqReduction", "CharcterItemGlobalReduction" }, // Typo in game file, do not "fix"
            { "characterIntelligence", "CharacterIntelligence" },
            { "characterItemGlobalReduction", "CharacterItemGlobalReduction" },
            { "characterLife", "CharacterLife" },
            { "characterLifeRegen", "CharacterLifeRegen" },
            { "characterOffensiveAbility", "CharacterOffensiveAbility" },
            { "characterMana", "CharacterMana" },
            { "characterRunSpeed", "CharacterRunSpeedModifier" },
            { "characterTotalSpeed", "CharacterTotalSpeedModifier" },
            { "characterStrength", "CharacterStrength" },

            { "defensiveBleeding", "DefenseBleeding" },
            { "defensiveCold", "DefenseCold" },
            { "defensiveDisruption", "DefenseDisruption" },
            { "defensiveElementalResistance", "DefenseElementalResistance" },
            { "defensiveFire", "DefenseFire" },
            { "defensiveLife", "DefenseLife" },
            { "defensiveLightning", "DefenseLightning" },
            { "defensivePierce", "DefensePierce" },
            { "defensivePoison", "DefensePoison" },
            { "defensiveProtection", "DefenseAbsorptionProtection" },
            { "defensiveReflect", "DefenseReflect" },
            { "defensiveStun", "DefenseStun" },

            { "defensiveSlowLifeLeach", "DefenseLifeLeach" },
            { "defensiveSlowManaLeach", "DefenseManaLeach" },

            { "offensiveLife", "DamageLife" },
            { "offensivePetrify", "DamagePetrify" },
            { "offensivePhysical", "DamagePhysical" },
            { "offensivePhysicalModifier", "DamageModifierPhysical" },
            { "offensivePierceRatio", "DamageBasePierceRatio" },
            { "offensiveTotalDamage", "<<TOTAL>>" },
            { "offensiveGlobalChance", "ChanceOfTag" },
            { "offensivePercentCurrentLife", "DamagePercentCurrentLife" },
            { "offensivePercentCurrentLifeChance", "ChanceOfTag" },
            { "offensivePierce", "DamagePierce" },

            { "offensiveSlowLightning", "DamageDurationLightning" },
            { "offensiveSlowLightningDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowLifeLeach", "DamageDurationLifeLeach" },
            { "offensiveSlowLifeLeachDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowLifeLeachModifier", "DamageDurationModifierLifeLeach" },
            { "offensiveSlowLightningModifier", "DamageDurationModifierLightning" },
            { "offensiveSlowManaLeach", "DamageDurationManaLeach" },
            { "offensiveSlowManaLeachDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowOffensiveAbility", "DamageDurationOffensiveAbility" },
            { "offensiveSlowOffensiveAbilityDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowPoison", "DamageDurationPoison" },
            { "offensiveSlowPoisonDurationMin", "DamageSingleFormatTime" },

            { "skillCooldownReduction", "SkillCooldownReduction" },
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
