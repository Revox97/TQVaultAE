using Microsoft.EntityFrameworkCore;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.Vaults;

namespace TQVaultAE.Persistence
{
    public class DataDbContext(DbContextOptions<DataDbContext> options) : DbContext(options)
    {
        public DbSet<Vault> Vaults => Set<Vault>();

        public DbSet<VaultTab> VaultTabs => Set<VaultTab>();

        public DbSet<ItemBase> Items => Set<ItemBase>();

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
                Type = VaultType.Items
            })));

            Task.WaitAll(tasks);
            await SaveChangesAsync();
        }
    }
}
