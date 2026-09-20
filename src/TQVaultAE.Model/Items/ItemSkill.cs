namespace TQVaultAE.Model.Items
{
    public class ItemSkill
    {
        public string SkillResourcePath { get; set; } = string.Empty;
        public int Level { get; set; }
        public Skill Skill { get; set; }
    }
}
