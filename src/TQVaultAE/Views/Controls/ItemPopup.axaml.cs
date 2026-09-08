using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Views.Controls;

public partial class ItemPopup : UserControl, INotifyPropertyChanged
{
    // TODO move into  view model
    public Item DataSource
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(DataSource));
        }
    }

    public ItemPopup(Item dataContext)
    {
        InitializeComponent();
        DataSource = dataContext;
        //DataContext = dataContext;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}