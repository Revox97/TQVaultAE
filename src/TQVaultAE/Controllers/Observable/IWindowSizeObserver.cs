using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
    interface IWindowSizeObserver : IDisposable
    {
		void Notify(object sender, WindowSizeUpdatedEventArgs e);
    }
}
