using Avalonia.Controls;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views;

public partial class ContentSelectorComboBox : UserControl
{
    public ContentSelectorComboBox()
    {
        InitializeComponent();
        DataContext = new ContentSelectorComboBoxViewModel();
    }
}