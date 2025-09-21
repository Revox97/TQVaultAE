using System.Windows;
using System.Windows.Controls;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Services;
using TQVaultAE.Models.Services.Observers;

namespace TQVaultAE.UI.Pages
{
	/// <summary>
	/// Interaction logic for VaultPage.xaml
	/// </summary>
	public partial class VaultPage : Page, IContentScaleObserver
	{
		public VaultPage()
		{
			InitializeComponent();
			ContentScaleService.GetInstance().AddObserver(this);
		}

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			VaultContainer.ColumnDefinitions[1].Width = new GridLength(args.VaultTab.SpacerWidth);
		}

		public void Dispose()
		{
			ContentScaleService.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
