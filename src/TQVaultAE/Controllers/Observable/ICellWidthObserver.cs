using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Controllers.Observable
{
	internal interface ICellWidthObserver : IDisposable
	{
		void Notify(object sender, CellWidthChangedEventArgs args);
	}
}
