using Avalonia.Controls;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Pages;

public partial class VaultPage : UserControl
{
    public VaultPage()
    {
        InitializeComponent();
        DataContext = new VaultPageViewModel();
    }
}