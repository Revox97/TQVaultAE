using Microsoft.EntityFrameworkCore;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.UI;
using TQVaultAE.Model.Vaults;
using TQVaultAE.Persistence.Data.Entities;

namespace TQVaultAE.Persistence
{
    public class DataDbContext(DbContextOptions<DataDbContext> options) : DbContext(options)
    {
        public DbSet<Vault> Vaults => Set<Vault>();

        public DbSet<VaultTab> VaultTabs => Set<VaultTab>();

        public DbSet<Item> Items => Set<Item>();

        public DbSet<ItemToVaultTab> ItemsToVaultTab => Set<ItemToVaultTab>();

        public DbSet<Affix> Affixes => Set<Affix>();

        public DbSet<IconSet> IconSets => Set<IconSet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(DataDbContext).Assembly,
                type => type.Namespace?.StartsWith("TQVaultAE.Persistence.Data") == true);

            modelBuilder.Entity<VaultTab>()
                        .Navigation(x => x.IconSet)
                        .AutoInclude();

            modelBuilder.Entity<IconSet>()
                        .Navigation(x => x.IconDown)
                        .AutoInclude();

            modelBuilder.Entity<IconSet>()
                        .Navigation(x => x.IconUp)
                        .AutoInclude();

            modelBuilder.Entity<IconSet>()
                        .Navigation(x => x.IconHover)
                        .AutoInclude();

            base.OnModelCreating(modelBuilder);
        }

        public async Task CreateDatabaseAsync()
        {
            try
            {
                string defaultIconSetId = "defaultIconSet";

                await IconSets.AddAsync(new IconSet(
                    defaultIconSetId,
                    new Icon("defaultIcon_up", @"InGameUI\inventorybagbuttonchosen01.tex"),
                    new Icon("defaultIcon_down", @"InGameUI\inventorybagbuttonunchosen01.tex"),
                    new Icon("defaultIcon_hover", @"InGameUI\inventorybagbuttonchosen01.tex")
                ));

                Guid defaultVaultId = Guid.NewGuid();
                await Vaults.AddAsync(new Vault()
                {
                    Id = defaultVaultId,
                    Name = "Main Vault", // TODO localize
                    Type = VaultType.Items,
                    Tabs = []
                });

                await SaveChangesAsync();

                for (int i = 1; i < 13; i++)
                {
                    VaultTab tab = new()
                    {
                        VaultId = defaultVaultId,
                        Vault = Vaults.First(),
                        IconSetId = defaultIconSetId,
                        IconSet = IconSets.First(),
                        Items = [],
                        Name = $"Default Tab {i}",
                    };

                    await VaultTabs.AddAsync(tab);
                }
            }
            catch (Exception ex)
            {

            }

            await SaveChangesAsync();
        }
    }
}
