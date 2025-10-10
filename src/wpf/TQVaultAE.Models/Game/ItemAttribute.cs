using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    public class ItemAttribute
    {
        public ItemAttributeType AttributeType { get; set; }

        public string Value { get; set; } = string.Empty;
    }
}
