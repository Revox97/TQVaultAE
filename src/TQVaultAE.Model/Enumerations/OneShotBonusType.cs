using System.ComponentModel;

namespace TQVaultAE.Model.Enumerations
{
    public enum OneShotBonusType
    {
        [Description("bonusAttributePoints")]
        AttributePoints,
        [Description("bonusExperiencePoints")]
        ExperiencePoints,
        [Description("bonusGoldPoints")]
        GoldPoints,
        [Description("bonusLifePercent")]
        LifePercent,
        [Description("bonusLifePoints")]
        LifePoints,
        [Description("bonusManaPercent")]
        ManaPercent,
        [Description("bonusManaPoints")]
        ManaPoints,
        [Description("bonusSkillPoints")]
        SkillPoints
    }
}
