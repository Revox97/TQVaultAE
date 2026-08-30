using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ItemClassification
    {
        Common,
        Epic,
        Legendary,
        Magical,
        Rare
    }
}
