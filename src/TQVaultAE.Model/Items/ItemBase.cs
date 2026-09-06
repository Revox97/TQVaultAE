using System.Drawing;
using System.Text.Json.Serialization;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents a Titan Quest item.
    /// </summary>
    public abstract class ItemBase
    {
        // TODO find a way to set a unique id per item
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("classification")]
        public ItemClassification Classification { get; set; }

        [JsonPropertyName("class")]
        public ItemClass Class { get; set; }

        [JsonPropertyName("position")]
        public Point Position { get; set; }

        [JsonPropertyName("size")]
        public Size Size { get; set; }

        [JsonPropertyName("iconDbPath")]
        public Uri IconDbPath { get; set; } = new Uri("avares://TQVaultAE/Assets/Img/MockItem1x1.png");

        [JsonPropertyName("baseItemProperties")]
        public List<string> BaseItemProperties { get; set; } = [];

        [JsonPropertyName("additionalItemProperties")]
        public List<string> AdditionalItemProperties { get; set; } = [];

        [JsonPropertyName("ItemLevel")]
        public int ItemLevel { get; set; }

        [JsonPropertyName("databasePath")]
        public string DatabasePath { get; set; } = string.Empty;

        // TODO Add remaining properties
    }
}
