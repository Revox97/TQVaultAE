using System;
using System.Collections.Generic;
using Avalonia.Controls;
using TQVaultAE.Models;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class ContentSelectorComboBox : UserControl
{
    public event EventHandler SelectionChanged;

    public ContentSelectorComboBox(Uri iconPath, List<string> values)
    {
        InitializeComponent();
        ContentSelectorComboBoxViewModel viewModel = new(iconPath, values.ConvertAll(x => new ItemContainer() { Name = x }));
        //viewModel.SetIcon(iconPath);
        DataContext = viewModel;
    }
}