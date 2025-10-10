using Avalonia.Controls;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views;

public partial class AboutPage : UserControl
{
    public AboutPage()
    {
        InitializeComponent();

        DataContext = new AboutPageViewModel(this);
    }
}