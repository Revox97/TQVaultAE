using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.UI;

namespace TQVaultAE.Model.Vaults
{
    /// <summary>
    /// Represents a tab linked to a <see cref="Vault"/>.
    /// </summary>
    public class VaultTab
    {
        private const int Columns = 18;
        private const int Rows = 20;

        [JsonPropertyName("id")]
        public Guid Id { get; init; } = Guid.NewGuid();

        [JsonPropertyName("vault")]
        public Guid VaultId { get; init; }

        [JsonIgnore()]
        public Vault Vault { get; init; } = null!;

        [JsonPropertyName("name")]
        [Length(10, 100, ErrorMessage = "Tab name must be between 10 and 100 characters long.")]
        public string Name { get; set; } = string.Empty;

        // TODO Make it bitmaps and use game icons.
        [JsonPropertyName("iconId")]
        public string IconSetId { get; set; } = "defaultIconSet";

        // TODO rename to IconSet and adjust type definition
        [JsonIgnore]
        public IconSet IconSet { get; set; } = null!;

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; } = [];

        /// <summary>
        /// Adds an <see cref="ItemBase"/> to the <see cref="VaultTab"/>.
        /// </summary>
        /// <param name="item">The <see cref="ItemBase"/>, which should be added.</param>
        /// <returns><see langword="true"/>, if the <paramref name="item"/> has been added successfully. Otherwise <see langword="false"/>.</returns>
        public bool AddItem(Item item)
        {
            Items.Add(item);
            return true;
        }

        public bool RemoveItem(Item item)
        {
            if (!Items.Contains(item))
                return false;

            Items.Remove(item);
            return true;
        }

        public bool ReplaceItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
