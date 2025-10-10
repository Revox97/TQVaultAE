using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Services;
using TQVaultAE.UI.Models;

namespace TQVaultAE.UI
{
    /// <summary>
    /// Interaction logic for TqWindow.xaml
    /// </summary>
    public partial class TqWindow : Window
    {
        private readonly TQWindowModel _model;
        private readonly bool _onCloseShutdown;

        public TqWindow(Page contentPage, bool onCloseShutdonw = false, bool triggersResize = false, bool canMinimizeMaximize = true)
        {
            InitializeComponent();

            _onCloseShutdown = onCloseShutdonw;
            _model = new TQWindowModel(contentPage, triggersResize, canMinimizeMaximize);
            DataContext = _model;
        }

		private void ContentController_Loaded(object sender, RoutedEventArgs e)
		{
            ContentController.Navigate(_model.ContentPage);
            Window_SizeChanged(sender, null!);
        }

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
            if (!_model.TriggersResize)
                return;

			WindowSizeService.GetInstance().Notify(this, new WindowSizeUpdatedEventArgs()
			{
                ContentWidth = ContentController.ActualWidth,
                ContentHeight = ContentController.ActualHeight
            });
        }

		private void BorderTop_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				DragMove();
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

        internal void Exit()
        {
            if (_onCloseShutdown)
                Application.Current.Shutdown();
            else
                Close();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e) => Exit();
    }
}
