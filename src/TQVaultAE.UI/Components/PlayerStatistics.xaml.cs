using System.Windows.Controls;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Models.Services;
using TQVaultAE.Models.Services.Observers;

namespace TQVaultAE.UI.Components
{
	/// <summary>
	/// Interaction logic for PlayerStatistics.xaml
	/// </summary>
	public partial class PlayerStatistics : UserControl, IContentScaleObserver
    {
        public PlayerStatistics()
        {
            InitializeComponent();
			ContentScaleService.GetInstance().AddObserver(this);
        }

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			FontSize = 8;
		}

		public void Dispose()
		{
			ContentScaleService.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
