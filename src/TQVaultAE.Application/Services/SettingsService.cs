using Microsoft.EntityFrameworkCore;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Model.Settings;
using TQVaultAE.Persistence;

namespace TQVaultAE.Application.Services
{
    public class SettingsService(IDbContextFactory<ApplicationDbContext> dbContextFactory, IEventDispatcher eventDispatcher) : ISettingsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContext = dbContextFactory;
        private readonly IEventDispatcher _eventDispatcher = eventDispatcher;

        public async Task UpdateAsync(Settings settings)
        {
            ApplicationDbContext db = _dbContext.CreateDbContext();
            db.Settings.Update(settings);
            await db.SaveChangesAsync();

            _eventDispatcher.Dispatch(this, new SettingsEvent());
        }
    }
}
