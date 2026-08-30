using Avalonia.Controls;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class ContentSelectorComboBox : UserControl
{
    public ContentSelectorComboBox()
    {
        InitializeComponent();
        DataContext = new ContentSelectorComboBoxViewModel();
    }
}