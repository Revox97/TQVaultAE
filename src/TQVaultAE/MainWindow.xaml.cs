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

		private void ButtonExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}