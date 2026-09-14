using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using TQVaultAE.Model.Enumerations;
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
            Properties = new ObservableCollection<ItemProperty>([
                new ItemProperty() { Type = ItemPropertyType.CharacterArmorAndDexterityRequirementsReduction, Value = 12f },
                new ItemProperty() { Type = ItemPropertyType.OffensiveColdModifier, Value = 13f },
                new ItemProperty() { Type = ItemPropertyType.DefensiveBleeding, Value = 0.5f },
            ]),
            Requirements = new ObservableCollection<ItemRequirement>([
                new ItemRequirement(ItemRequirementType.Dexterity, 12),
                new ItemRequirement(ItemRequirementType.Level, 35),
                new ItemRequirement(ItemRequirementType.Strenth, 120),
            ]),
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