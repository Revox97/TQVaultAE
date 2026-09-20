using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Localisation;

namespace TQVaultAE.Model
{
    public class Skill
    {
        public string Name { get; set; } = string.Empty;

        public Skill() { }

        public Skill(ArzRecord skillRecord)
        {
            string nameTag = skillRecord["skillDisplayName"]?.Get<string>(0) ?? string.Empty;
            Name = new GameLocalizationService().GetLocalizedValueByTagAsync(nameTag).Result ?? string.Empty;

            // Get remaining skill data
        }
    }
}
