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
        public Guid Id { get; init; }

        [JsonPropertyName("vault")]
        public Guid VaultId { get; init; }

        [JsonIgnore()]
        public Vault Vault { get; init; } = null!;

        [JsonPropertyName("name")]
        [Length(10, 100, ErrorMessage = "Tab name must be between 10 and 100 characters long.")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("iconId")]
        public Guid IconId { get; set; }

        [JsonIgnore]
        public IconSet Icon { get; set; } = new IconSet(
            "defaultIconSet",
            new Icon("defaultIcon_up", new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_up.png")),
            new Icon("defaultIcon_down", new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_down.png")),
            new Icon("defaultIcon_hover", new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_over.png")));

        /// <summary>
        /// Gets or sets which item slots are currently allocated.
        /// </summary>
        [JsonIgnore]
        public bool[,] SlotAllocation { get; set; } = new bool[Rows,Columns];

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; } = [];

        /// <summary>
        /// Adds an <see cref="ItemBase"/> to the <see cref="VaultTab"/>.
        /// </summary>
        /// <param name="item">The <see cref="ItemBase"/>, which should be added.</param>
        /// <returns><see langword="true"/>, if the <paramref name="item"/> has been added successfully. Otherwise <see langword="false"/>.</returns>
        public bool AddItem(Item item)
        {
            //for (int i = item.Position.X; i <= item.Position.X + item.Size.Width; i++)
            //{
            //    for (int k = item.Position.Y; k <= item.Position.Y + item.Size.Height; k++)
            //    {
            //        if (SlotAllocation[k, i])
            //        {
            //            // TODO: Cannot add item, must be handled somehow
            //            return false;
            //        }
            //    }
            //}

            //for (int i = item.Position.X; i <= item.Position.X + item.Size.Width; i++)
            //{
            //    for (int k = item.Position.Y; k <= item.Position.Y + item.Size.Height; k++)
            //        SlotAllocation[k, i] = true;
            //}

            Items.Add(item);
            return true;
        }

        public bool RemoveItem(Item item)
        {
            if (!Items.Contains(item))
                return false;

            //for (int i = item.Position.X; i <= item.Position.X + item.Size.Width; i++)
            //{
            //    for (int k = item.Position.Y; k <= item.Position.Y + item.Size.Height; k++)
            //        SlotAllocation[k, i] = false;
            //}

            Items.Remove(item);
            return true;
        }

        public bool ReplaceItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
