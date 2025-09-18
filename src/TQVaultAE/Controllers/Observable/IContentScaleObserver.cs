using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
	internal interface IContentScaleObserver : IDisposable
	{
		void Notify(object sender, ContentScaleUpdatedEventArgs args);
	}
}
