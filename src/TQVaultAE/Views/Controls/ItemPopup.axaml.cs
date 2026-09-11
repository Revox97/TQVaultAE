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

    // Design time
    // TODO add mockup service for design time
    public ItemPopup()
    {
        InitializeComponent();

        DataSource = new()
        {
            Name = "Test Item",
            Seed = 12345,
        };
    }

    public ItemPopup(Item item)
    {
        InitializeComponent();
        DataSource = item;
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}