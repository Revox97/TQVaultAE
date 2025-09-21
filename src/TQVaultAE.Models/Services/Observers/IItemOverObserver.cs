using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Models.Services.Observers
{
	public interface IItemOverObserver : IDisposable
	{
		void Notify(object sender, ItemOverEventArgs args);
	}
}
