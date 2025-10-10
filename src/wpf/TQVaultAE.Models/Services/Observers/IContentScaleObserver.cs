using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Models.Services.Observers
{
	public interface IContentScaleObserver : IDisposable
	{
		void Notify(object sender, ContentScaleUpdatedEventArgs args);
	}
}
