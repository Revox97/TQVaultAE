using Avalonia.Controls;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Pages;

public partial class VaultPage : UserControl
{
    private readonly VaultPageViewModel _viewModel = new();

    public VaultPage()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}