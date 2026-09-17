using TQVaultAE.Model.Items;
using TQVaultAE.Model.Vaults;

namespace TQVaultAE.Persistence.Data.Entities
{
    public class ItemToVaultTab
    {
        public Guid VaultTabId { get; set; }

        public VaultTab VaultTab { get; set; } = null!;

        public Guid ItemId { get; set; }

        public Item Item { get; set; } = null!;
    }
}
