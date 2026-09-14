using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
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
            UpdateContent();
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

    // TODO Pretty fuggly. Improve and or replace with actual bindings. List Views are pain in Avalonia thouth...
    public void UpdateContent()
    {
        StackPanel container = ItemPopup__Container;
        container.Children.Clear();

        System.Drawing.Color tqWhite = TitanQuestColors.White;
        System.Drawing.Color tqOrange = TitanQuestColors.Orange;

        Brush brushTitle = new SolidColorBrush(Color.FromArgb(DataSource.Color.A, DataSource.Color.R, DataSource.Color.G, DataSource.Color.B));
        Brush brushWhite = new SolidColorBrush(Color.FromArgb(tqWhite.A, tqWhite.R, tqWhite.G, tqWhite.B));
        Brush brushOrange = new SolidColorBrush(Color.FromArgb(tqOrange.A, tqOrange.R, tqOrange.G, tqOrange.B));

        TextBlock titleTb = new()
        {
            Foreground = brushTitle,
            Text = DataSource.Name,
            FontWeight = FontWeight.DemiBold,
        };

        container.Children.Add(titleTb);

        if (!string.IsNullOrEmpty(DataSource.Description))
        {
            TextBlock descriptionTb = new()
            {
                Foreground = brushWhite,
                Text = DataSource.Description,
                TextWrapping = TextWrapping.Wrap,
            };

            container.Children.Add(descriptionTb);
        }

        Separator propertySeperator = new()
        {
            Height = 15
        };

        container.Children.Add(propertySeperator);

        if (DataSource.Properties.Count > 0)
        {
            TextBlock basePropertiesLabel = new()
            {
                Foreground = brushOrange,
                Text = "Base Properties:",
            };

            container.Children.Add(basePropertiesLabel);

            foreach(ItemProperty property in DataSource.Properties)
            {
                // TODO Overload ToString in ItemProperty and get the correct localization
                TextBlock propertyTb = new()
                {
                    Foreground = brushWhite,
                    Text = $"{property.Type}: {property.Value}",
                };

                container.Children.Add(propertyTb);
            }
        }

        // TODO Add talisman values here
        Separator seperator1 = new()
        {
            Foreground = brushWhite,
            Background = brushWhite,
            Width = 200,
            Height = 15,
        };

        container.Children.Add(seperator1);

        TextBlock seedTb = new()
        {
            Foreground = brushWhite,
            Text = $"Seed: {DataSource.Seed}",
        };

        container.Children.Add(seedTb);

        // TODO Add DLC here
        Separator seperator2 = new()
        {
            Foreground = brushWhite,
            Background = brushWhite,
            Width = 200,
            Height = 15,
        };

        container.Children.Add(seperator2);

        if (DataSource.Requirements.Count > 0)
        {
            foreach(ItemRequirement requirement in DataSource.Requirements)
            {
                // TODO Overload ToString in ItemProperty and get the correct localization
                TextBlock requirementTb = new()
                {
                    Foreground = brushWhite,
                    Text = $"{requirement.Type}: {requirement.Value}",
                };

                container.Children.Add(requirementTb);
            }
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}