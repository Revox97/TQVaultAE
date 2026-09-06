using System.Text.Json.Serialization;

namespace TQVaultAE.Model.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Language
    {
        Deutsch,
        English
    }
}
