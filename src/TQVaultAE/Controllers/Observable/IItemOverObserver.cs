using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
	internal interface IItemOverObserver : IDisposable
	{
		void Notify(object sender, ItemOverEventArgs args);
	}
}
