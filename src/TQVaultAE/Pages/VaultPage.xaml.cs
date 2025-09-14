using System.Windows.Controls;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Pages
{
	/// <summary>
	/// Interaction logic for VaultPage.xaml
	/// </summary>
	public partial class VaultPage : Page, ICellWidthObserver
	{
		public VaultPage()
		{
			InitializeComponent();
			CellWidthController.GetInstance().AddObserver(this);
		}

		public void Notify(object sender, CellWidthChangedEventArgs args)
		{
			// TODO Implement proper rescaling correctly 
			//ItemNameLabel.Width = args.NewHeaderWidthHeight * 12;
		}

		public void Dispose()
		{
			CellWidthController.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
