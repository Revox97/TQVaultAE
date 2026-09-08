using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Vaults;
using TQVaultAE.Persistence;

namespace TQVaultAE.Application.Services
{
    public class VaultService(IDbContextFactory<DataDbContext> dbContextFactory) : IVaultService
    {
        private readonly IDbContextFactory<DataDbContext> _dbContextFactory = dbContextFactory;

        public async Task<List<Vault>> GetVaultsAsync()
        {
            try
            {
                await using DataDbContext db = await _dbContextFactory.CreateDbContextAsync();
                return [.. db.Vaults.AsNoTracking()];
            }
            catch(Exception ex)
            {
                throw new Exception("Fetching vaults failed.", ex);
            }
        }

        public async Task<Vault> CreateVaultAsync(string name, VaultType type)
        {
            try
            {
                await using DataDbContext db = await _dbContextFactory.CreateDbContextAsync();

                Vault newVault = new()
                {
                    Name = name,
                    Type = type
                };

                EntityEntry<Vault> result = await db.Vaults.AddAsync(newVault);
                await db.SaveChangesAsync();

                return result.Entity;
            }
            catch(Exception ex)
            {
                throw new Exception("Creating new vault failed.", ex);
            }
        }

        public async Task UpdateVaultAsync(Vault vault)
        {
            try
            {
                await using DataDbContext db = await _dbContextFactory.CreateDbContextAsync();

                if (!await db.Vaults.ContainsAsync(vault))
                    throw new KeyNotFoundException($"Vault with id '{vault.Id}' does not exist.");

                db.Vaults.Update(vault);
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                throw new Exception($"Updating vault with id '{vault.Id}' failed.", ex);
            }
        }

        public async Task DeleteVaultAsync(Vault vault)
        {
            try
            {
                await using DataDbContext db = await _dbContextFactory.CreateDbContextAsync();
                db.Vaults.Remove(vault);
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"Deleting vault with id '{vault.Id}' failed", ex);
            }
        }
    }
}
