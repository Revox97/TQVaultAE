using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Services.Observers;

namespace TQVaultAE.Models.Services
{
    public interface IItemHoverService
    {
        void AddObserver(IItemOverObserver observer);

        void RemoveObserver(IItemOverObserver observer);
        void Notify(object sender, ItemOverEventArgs args);
    }
}
