using System.Text.Json.Serialization;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    public class ItemCharmItem : ItemBase
    {
        [JsonPropertyName("completedRelicLevel")]
        public int CompletedRelicLevel { get; set; }

        [JsonPropertyName("canEnchantBodyArmor")]
        public bool CanEnchantBodyArmor { get; set; }  = false;

        [JsonPropertyName("canEnchantBracelet")]
        public bool CanEnchantBracelet { get; set; }  = false;

        [JsonPropertyName("canEnchantArmband")]
        public bool CanEnchantArmband { get; set; }  = false;

        [JsonPropertyName("canEnchantHelmet")]
        public bool CanEnchantHelmet { get; set; }  = false;

        [JsonPropertyName("canEnchantMace")]
        public bool CanEnchantMace { get; set; }  = false;

        [JsonPropertyName("canEnchantSpeer")]
        public bool CanEnchantSpeer { get; set; }  = false;

        [JsonPropertyName("canEnchantAxe")]
        public bool CanEnchantAxe { get; set; }  = false;

        [JsonPropertyName("canEnchantBow")]
        public bool CanEnchantBow { get; set; }  = false;

        [JsonPropertyName("canEnchantStaff")]
        public bool CanEnchantStaff { get; set; }  = false;

        [JsonPropertyName("canEnchantSword")]
        public bool CanEnchantSword { get; set; }  = false;

        [JsonPropertyName("canEnchantGreaves")]
        public bool CanEnchantGreaves { get; set; }  = false;

        public ItemCharmItem() : base()
        {
            Class = ItemClass.ItemCharm;
        }

    }
}
