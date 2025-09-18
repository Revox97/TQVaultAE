using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
	internal interface IContentScaleObserver
	{
		void Notify(object sender, ContentScaleUpdatedEventArgs args);
	}
}
