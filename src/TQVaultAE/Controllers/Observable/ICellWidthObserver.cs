using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
	// TODO Delete once replaced
	internal interface ICellWidthObserver : IDisposable
	{
		void Notify(object sender, CellWidthChangedEventArgs args);
	}
}
