using System.Configuration;
using System.Data;
using System.Windows;

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
				new MainWindow().Show();
			} 
			catch (Exception ex)
			{
				// TODO Handle general exceptions
			}
		}
	}

}
