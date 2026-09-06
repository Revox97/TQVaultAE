using Microsoft.EntityFrameworkCore;
using TQVaultAE.Model.Vaults;
using TQVaultAE.Persistence;

namespace TQVaultAE.Application.Services
{
    public class VaultService(IDbContextFactory<DataDbContext> dbContextFactory)
    {
        private readonly IDbContextFactory<DataDbContext> _dbContextFactory = dbContextFactory;

        public async Task<List<Vault>> GetVaultsAsync()
        {
            await using DataDbContext db = await _dbContextFactory.CreateDbContextAsync();

            return await db.Vaults
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
