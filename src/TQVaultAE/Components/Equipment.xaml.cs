using System.Windows;
using System.Windows.Controls;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Components
{
	/// <summary>
	/// Interaction logic for Equipment.xaml
	/// </summary>
	public partial class Equipment : UserControl, IContentScaleObserver
	{
		public Equipment()
		{
			InitializeComponent();
			ContentScaleController.GetInstance().AddObserver(this);
		}

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			SetGridDimensions(args);
		}

		private void SetGridDimensions(ContentScaleUpdatedEventArgs args)
		{
			// TODO Set correct column definitions, so items will be inserted in correct positions
		}

		public void Dispose()
		{
			ContentScaleController.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
