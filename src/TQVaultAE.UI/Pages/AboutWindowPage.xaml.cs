using System.Windows;
using System.Windows.Controls;

namespace TQVaultAE.UI.Pages
{
    /// <summary>
    /// Interaction logic for AboutWindowPage.xaml
    /// </summary>
    public partial class AboutWindowPage : Page
    {
        public AboutWindowPage() => InitializeComponent();

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && Window.GetWindow(button) is TqWindow window)
                window.Exit();
        }

    }
}
