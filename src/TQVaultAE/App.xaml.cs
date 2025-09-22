using System.Reflection;
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

                AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
                new TqWindow(new MainWindowPage(), true, true)
                {
                    MinHeight = 800,
                    MinWidth = 1300,
                    Height = 900,
                    Width = 1100,
                    Title = $"{assemblyName.Name} {assemblyName.Version}",
                    Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/TQVaultAE.UI;component/Resources/Img/mainwindow_background.jpg"))),
                }.Show();
			} 
			catch (Exception ex)
			{
                try
                {
                    // TODO log exceptions
                }
                catch
                {
                    // Show message box containing exception
                }
			}
		}
	}

}
