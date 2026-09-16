using Microsoft.EntityFrameworkCore;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.UI;
using TQVaultAE.Model.Vaults;

namespace TQVaultAE.Persistence
{
    public class DataDbContext(DbContextOptions<DataDbContext> options) : DbContext(options)
    {
        public DbSet<Vault> Vaults => Set<Vault>();

        public DbSet<VaultTab> VaultTabs => Set<VaultTab>();

        public DbSet<Item> Items => Set<Item>();

        public DbSet<Affix> Affixes => Set<Affix>();

        public DbSet<IconSet> IconSets => Set<IconSet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(DataDbContext).Assembly,
                type => type.Namespace?.StartsWith("TQVaultAE.Persistence.Data") == true);

            base.OnModelCreating(modelBuilder);
        }

        public async Task CreateDatabaseAsync()
        {
            List<Task> tasks = [];

            tasks.Add(Task.Run(() => Vaults.AddAsync(new Vault()
            {
                Id = Guid.Empty,
                Name = "Main Vault", // TODO localize
                Type = VaultType.Items,
                Tabs = [
                    new VaultTab()
                    {
                        Id = Guid.NewGuid(),
                        //IconSet = new IconSet(Guid.NewGuid(), "", "", ""),
                        Items =
                        [

                        ],
                        Name = "Default Tab 1",
                    },
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
                ]
            })));

            Task.WaitAll(tasks);
            await SaveChangesAsync();
        }
    }
}
