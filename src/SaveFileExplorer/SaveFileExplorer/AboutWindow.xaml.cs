using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace SaveFileExplorer
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.RemoveIcon();
            base.OnSourceInitialized(e);
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e) => Close();
    }
}
