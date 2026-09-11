using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class OneShotBonus(OneShotBonusType type, float value)
    {
        public OneShotBonusType Type { get; set; } = type;

        public float Value { get; set; } = value;

        public override string ToString() => $"{Type} - {Value}";
    }
}
