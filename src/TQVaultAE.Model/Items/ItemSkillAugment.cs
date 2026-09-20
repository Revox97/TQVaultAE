using System.ComponentModel.DataAnnotations.Schema;

namespace TQVaultAE.Model.Items
{
    // TODO Find better naming
    public class ItemSkillAugment(string recordPath, int value)
    {
        public string RecordPath { get; set; } = recordPath;

        [NotMapped]
        public string SkillName { get; set; } = string.Empty;

        public int Value { get; set; } = value;

        public override string ToString()
        {
            return $"+{Value} to {SkillName}"; // TODO localize
        }
    }
}
