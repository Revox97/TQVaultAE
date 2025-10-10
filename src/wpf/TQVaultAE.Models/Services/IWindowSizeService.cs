using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Services.Observers;

namespace TQVaultAE.Models.Services
{
    public interface IWindowSizeService
    {
        void AddObserver(IWindowSizeObserver observer);
        void RemoveObserver(IWindowSizeObserver observer);
        void Notify(object sender, WindowSizeUpdatedEventArgs args);
    }
}
