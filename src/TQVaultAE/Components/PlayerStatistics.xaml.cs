using System.Windows.Controls;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models.EventArgs;

namespace TQVaultAE.Components
{
	/// <summary>
	/// Interaction logic for PlayerStatistics.xaml
	/// </summary>
	public partial class PlayerStatistics : UserControl, IContentScaleObserver
    {
        public PlayerStatistics()
        {
            InitializeComponent();
			ContentScaleController.GetInstance().AddObserver(this);
        }

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			FontSize = 8;
		}

		public void Dispose()
		{
			ContentScaleController.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}
	}
}
