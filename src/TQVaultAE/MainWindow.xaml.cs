using System.Windows;
using System.Windows.Controls.Primitives;
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

		private void ButtonVault_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton buttonVault && (buttonVault.IsChecked ?? false))
			{
				if (ButtonConfiguration is not null)
					ButtonConfiguration.IsChecked = false;

				if (ButtonSearch is not null)
					ButtonSearch.IsChecked = false;
			}
		}

		private void ButtonConfiguration_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton buttonConfiguration && (buttonConfiguration.IsChecked ?? false))
			{
				if (ButtonVault is not null)
					ButtonVault.IsChecked = false;

				if (ButtonConfiguration is not null)
				ButtonSearch.IsChecked = false;
			}
		}

		private void ButtonSearch_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton buttonSearch && (buttonSearch.IsChecked ?? false))
			{
				if (ButtonVault is not null)
					ButtonVault.IsChecked = false;

				if (ButtonConfiguration is not null)
					ButtonConfiguration.IsChecked = false;
			}

		}
	}
}