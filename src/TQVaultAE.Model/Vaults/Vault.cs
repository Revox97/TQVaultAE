using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Vaults
{
    /// <summary>
    /// Represents a TQVault vault.
    /// </summary>
    public class Vault
    {
        /// <summary>
        /// Gets or sets the id of the <see cref="Vault"/>.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("type")]
        public VaultType Type { get; set; } = VaultType.Items;

        [JsonPropertyName("name")]
        [Length(10, 255, ErrorMessage = "Vault name must be between 10 and 255 characters long.")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("tabs")]
        public List<VaultTab> Tabs { get; set; } =
        [
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
            new VaultTab(),
        ];
    }
}
