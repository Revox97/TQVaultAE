using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Models.Services.Observers
{
    public interface IWindowSizeObserver : IDisposable
    {
		void Notify(object sender, WindowSizeUpdatedEventArgs e);
    }
}
