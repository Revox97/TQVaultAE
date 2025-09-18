using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Pages;

namespace TQVaultAE
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly VaultPage _vaultPage;
		private readonly ConfigurationPage _configurationPage;
		private readonly SearchPage _searchPage;

		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowModel();

			_vaultPage = new VaultPage();
			_configurationPage = new ConfigurationPage();
			_searchPage = new SearchPage();
			ButtonVault.IsChecked = true;
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

				SwitchPage(_vaultPage);
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

				SwitchPage(_configurationPage);
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

				SwitchPage(_searchPage);
			}
		}

		private void SwitchPage(Page newPage)
		{
			try
			{
				ContentController.Navigate(newPage);
			}
			catch (Exception ex)
			{
				// TODO Log Exception
				return;
			}
		}

		private void BorderTop_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				DragMove();
        }

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			WindowSizeUpdater.GetInstance().Notify(this, new WindowSizeUpdatedEventArgs()
			{
				ContentWidth = ContentController.ActualWidth,
				ContentHeight = ContentController.ActualHeight
			});
        }

		private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
		{
			if (WindowState == WindowState.Maximized)
			{
				WindowState = WindowState.Normal;
				BorderThickness = new Thickness(0);
			}
			else
			{
				// TODO Check how to avoid full screen
				WindowState = WindowState.Maximized;
				BorderThickness = new Thickness(8);
			}
		}

		private void ButtonMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
	}
}