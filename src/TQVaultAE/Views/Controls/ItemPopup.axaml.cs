using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Views.Controls;

public partial class ItemPopup : UserControl, INotifyPropertyChanged
{
    public ItemBase DataSource
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(DataSource));
        }
    }

    public ItemPopup(ItemBase dataContext)
    {
        InitializeComponent();
        DataSource = dataContext;
        //DataContext = dataContext;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}