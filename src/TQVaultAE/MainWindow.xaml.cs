using System.Windows;
using TQVaultAE.Models;

namespace TQVaultAE
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new MainWindowModel();
		}

		private void ButtonClose_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}