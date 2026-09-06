using TQVaultAE.Model.Settings;

namespace TQVaultAE.Application.Contracts
{
    public interface ISettingsService
    {
        Task UpdateAsync(Settings settings);
    }
}
