using Avalonia.Input;

namespace TQVaultAE.Model.Items.ItemProperties
{
    public class ItemPropertyTagCollection
    {
        private static readonly Dictionary<string, string> s_nameToTagMap = new()
        {
            //{ "characterAttackSpeed", "CharacterAttackSpeedModifier" },
            { "characterDeflectProjectile", "CharacterDeflectProjectiles" },
            { "characterGlobalReqReduction", "CharcterItemGlobalReduction" }, // Typo in game file, do not "fix"
            { "defensiveProtection", "DefenseAbsorptionProtection" },
            { "defensiveSlowLifeLeach", "DefenseLifeLeach" },
            { "defensiveSlowManaLeach", "DefenseManaLeach" },

            // Maybe there is a pattern
            { "offensiveBaseLightning", "tagDamageBaseLightning" },
            { "offensivePierceRatio", "DamageBasePierceRatio" },

            { "offensiveSlowBleedingDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowLightningDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowLifeLeachDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowManaLeachDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowOffensiveAbilityDurationMin", "DamageSingleFormatTime" },
            { "offensiveSlowPoisonDurationModifier", "ImprovedTimeFormat" },
            { "offensiveSlowPoisonDurationMin", "DamageSingleFormatTime" },

            { "retaliationSlowLifeDurationMin", "DamageSingleFormatTime" },
            { "retaliationSlowLifeLeachDurationMin", "DamageSingleFormatTime" },
            { "retaliationSlowOffensiveAbilityDurationMin", "DamageSingleFormatTime" },
            { "retaliationSlowRunSpeedDurationMin", "DamageSingleFormatTime" },
        };

        public string this[string name]
        {
            get
            {
                return s_nameToTagMap.TryGetValue(name, out string? result)
                    ? result
                    : GetTag(name);
            }
        }

        protected static string GetTag(string name)
        {
            if (name.StartsWith("character"))
                return name.Replace("character", "Character");

            if (name.StartsWith("defensive"))
                return name.Replace("defensive", "Defense");

            if (name.StartsWith("retaliationSlow"))
                return name.Replace("retaliationSlow", "RetaliationDuration");

            if (name.StartsWith("retaliation"))
                return name.Replace("retaliation", "Retaliation");

            if (name.StartsWith("skill"))
                return name.Replace("skill", "Skill");

            if (name.StartsWith("offensiveBase"))
                return name.Replace("offensiveBase", "tagDamageBase");

            if (name.StartsWith("offensiveSlow"))
            {
                return name.EndsWith("Modifier")
                    ? name.Replace("offensiveSlow", "DamageDurationModifier")[..^8]
                    : name.Replace("offensiveSlow", "DamageDuration");
            }

            if (name.StartsWith("offensive"))
            {
                return name.EndsWith("Modifier")
                    ? name.Replace("offensive", "DamageModifier")[..^8]
                    : name.Replace("offensive", "Damage");
            }

            return name;
        }
    }
}
