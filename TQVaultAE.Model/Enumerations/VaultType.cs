using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VaultType
    {
        Items,
        Sets,
        Crafting
    }
}
