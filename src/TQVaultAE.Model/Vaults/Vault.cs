using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TQVaultAE.Model.Enumerations;

// TODO: Add reference to the owning user
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

        /// <summary>
        /// Gets or sets the type of the <see cref="Vault"/>.
        /// </summary>
        [JsonPropertyName("type")]
        public VaultType Type { get; set; } = VaultType.Items;

        /// <summary>
        /// Gets or sets the name of the <see cref="Vault"/>.
        /// </summary>
        [JsonPropertyName("name")]
        [Length(10, 255, ErrorMessage = "Vault name must be between 10 and 255 characters long.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a list of <see cref="VaultTab"/>s, that belong to the <see cref="Vault"/>.
        /// </summary>
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
