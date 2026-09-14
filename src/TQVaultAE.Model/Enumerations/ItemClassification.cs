using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ItemClassification
    {
        Broken,
        Common,
        Rare,
        Epic,
        Legendary,
        Magical,
        Quest,
    }
}
