using System.Windows;
using System.Windows.Controls;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Pages
{
	/// <summary>
	/// Interaction logic for VaultPage.xaml
	/// </summary>
	public partial class VaultPage : Page, IContentScaleObserver
	{
		public VaultPage()
		{
			InitializeComponent();
			ContentScaleController.GetInstance().AddObserver(this);
		}

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			VaultContainer.ColumnDefinitions[1].Width = new GridLength(args.VaultTab.SpacerWidth);
		}

		public void Dispose()
		{
			ContentScaleController.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
