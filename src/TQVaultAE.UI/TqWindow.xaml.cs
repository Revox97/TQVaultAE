using System.Windows;
using System.Windows.Input;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Services;

namespace TQVaultAE.UI
{
    /// <summary>
    /// Interaction logic for TqWindow.xaml
    /// </summary>
    public partial class TqWindow : Window
    {
        public TqWindow()
        {
            InitializeComponent();
        }

		private void ContentController_Loaded(object sender, RoutedEventArgs e)
		{
			Window_SizeChanged(sender, null!);
			WindowSizeService.GetInstance().Notify(this, new WindowSizeUpdatedEventArgs());
        }

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
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

        private void ButtonExit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}
