using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonPropertyName("name")]
        [Length(10, 255, ErrorMessage = "Vault name must be between 10 and 255 characters long.")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("tabs")]
        public Dictionary<int, VaultTab> Tabs { get; set; } = new Dictionary<int, VaultTab>() {
            { 0, new VaultTab() },
            { 1, new VaultTab() },
            { 2, new VaultTab() },
            { 3, new VaultTab() },
            { 4, new VaultTab() },
            { 5, new VaultTab() },
            { 6, new VaultTab() },
            { 7, new VaultTab() },
            { 8, new VaultTab() },
            { 9, new VaultTab() },
            { 10, new VaultTab() },
            { 11, new VaultTab() },
            { 12, new VaultTab() },
        };
    }
}
