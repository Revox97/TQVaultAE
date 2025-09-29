using System.Windows;

namespace SaveFileExplorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MenuItemAbout_Click(object sender, RoutedEventArgs e) => new AboutWindow().Show();
}