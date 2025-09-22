using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.UI;
using TQVaultAE.UI.Pages;

namespace TQVaultAE
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			try
			{
				base.OnStartup(e);
                new TqWindow(new MainWindowPage())
                {
                    MinHeight = 800,
                    MinWidth = 1300,
                    Height = 900,
                    Width = 1100,
                    Title = "TQVaultAE 5.5.0.0",
                    Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/mainwindow_background.jpg"))),
                }.Show();
				//new MainWindow().Show();
			} 
			catch (Exception ex)
			{
				// TODO Handle general exceptions
			}
		}
	}

}
