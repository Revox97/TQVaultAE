using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Application.Factories;
using TQVaultAE.FileFormats.Tex;
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
                return [.. db.Vaults];
            }
            catch (Exception ex)
            {
                throw new Exception("Fetching vaults failed.", ex);
            }
        }

        public async Task<Vault> GetCompleteVaultAsync(Vault vault)
        {
            try
            {
                await using DataDbContext db = await _dbContextFactory.CreateDbContextAsync();

                List<VaultTab> tabs = [.. db.VaultTabs.Where(x => x.VaultId == vault.Id)];

                for (int i = 0; i < tabs.Count; i++)
                    vault.Tabs[i] = await GetCompleteVaultTabAsync(tabs[i]);
            }
            catch (Exception ex)
            {
                // Todo add logging
            }

            return vault;
        }

        private static async Task<VaultTab> GetCompleteVaultTabAsync(VaultTab tab)
        {
            // TODO Move into separate library
            TexFile iconDownFile = await new GameIconService().GetTexFileByTagAsync(tab.IconSet.IconDown.ResourcePath.ToString());
            tab.IconSet.IconDown.Bitmap = iconDownFile.ToBitmap();

            TexFile iconUpFile = await new GameIconService().GetTexFileByTagAsync(tab.IconSet.IconUp.ResourcePath.ToString());
            tab.IconSet.IconUp.Bitmap = iconUpFile.ToBitmap();

            TexFile iconHoverFile = await new GameIconService().GetTexFileByTagAsync(tab.IconSet.IconHover.ResourcePath.ToString());
            tab.IconSet.IconHover.Bitmap = iconHoverFile.ToBitmap();

            // GetItems
            for (int i = 0; i < tab.Items.Count; i++)
                tab.Items[i] = await ItemFactory.GetCompleteItemAsync(tab.Items[i]);

            return tab;
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw new Exception($"Deleting vault with id '{vault.Id}' failed", ex);
            }
        }
    }
}
