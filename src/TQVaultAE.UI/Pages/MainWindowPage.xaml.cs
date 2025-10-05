using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TQVaultAE.UI.Controllers;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI.Pages
{
    /// <summary>
    /// Interaction logic for MainWindowPage.xaml
    /// </summary>
    public partial class MainWindowPage : Page
    {
        private readonly MainWindowController _controller;
        private readonly DragController _dragController;

		private readonly VaultPage _vaultPage;
		private readonly ConfigurationPage _configurationPage;
		private readonly SearchPage _searchPage;

        public MainWindowPage()
        {
            InitializeComponent();

            _dragController = DragController.GetInstance();
            Mouse.AddPreviewMouseMoveHandler(this, OnMouseMoved);

			DataContext = new MainWindowPageModel();

			_vaultPage = new VaultPage();
			_configurationPage = new ConfigurationPage();
			_searchPage = new SearchPage();
            _controller = new MainWindowController();

			ButtonVault.IsChecked = true;
        }

        private void OnMouseMoved(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);
            _dragController.UpdateMousePosition(position);
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

        private void About_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => _controller.ShowAboutWindow();
    }
}
