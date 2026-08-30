using System;
using Avalonia.Controls;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class ContentSelectorComboBox : UserControl
{
    public ContentSelectorComboBox(Uri iconPath)
    {
        InitializeComponent();
        ContentSelectorComboBoxViewModel viewModel = new();
        viewModel.SetIcon(iconPath);
        DataContext = viewModel;
    }
}