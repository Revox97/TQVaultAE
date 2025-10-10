using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Services.Observers;

namespace TQVaultAE.Models.Services
{
    public interface IContentScaleService
    {
        void AddObserver(IContentScaleObserver observer);
        void RemoveObserver(IContentScaleObserver observer);
        void Notify(object sender, WindowSizeUpdatedEventArgs e);
    }
}
