namespace TQVaultAE.Model.Items.ItemProperties
{
    public class ItemPropertyTagCollection
    {
        private static readonly Dictionary<string, string> s_nameToTagMap = new()
        {
            { "characterAttackSpeed", "CharacterAttackSpeedModifier" },
            { "characterDefensiveAbility", "CharacterDefensiveAbility" },
            { "characterMana", "CharacterMana" },
            { "characterStrength", "CharacterStrengthModifier" },
            { "characterOffensiveAbility", "CharacterOffensiveAbility" },

            { "defensiveBleeding", "DefenseBleeding" },
            { "defensiveDisruption", "DefenseDisruption" },
            { "defensiveElementalResistance", "DefenseElementalResistance" },
            { "defensiveLife", "DefenseLife" },
            { "defensiveProtection", "DefenseAbsorptionProtection" },
            { "defensiveStun", "DefenseStun" },

            { "skillCooldownReduction", "SkillCooldownReduction" },

            { "offensiveLife", "DamageLife" },
            { "offensivePetrify", "DamagePetrify" },
            { "offensivePhysical", "DamagePhysical" },
            { "offensivePhysicalModifier", "DamageModifierPhysical" },
            { "offensivePierceRatio", "DamageBasePierceRatio" },
            { "offensiveTotalDamage", "<<TOTAL>>" },
            { "offensiveGlobalChance", "ChanceOfTag" },

            { "offensiveSlowLightning", "DamageDurationLightning" },
            { "offensiveSlowLightningDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowLifeLeach", "DamageDurationModifierLifeLeach" },

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
