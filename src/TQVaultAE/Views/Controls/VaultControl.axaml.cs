using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Model.Vaults;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class VaultControl : UserControl, IMainWindowChangedObserver
{
    private const int Rows = 20;
    private const int Columns = 18;
    private const int Tabs = 12;

    private int _cellSize = 0;

    public VaultControlViewModel ViewModel { get; set; } = new();

    public static readonly StyledProperty<Vault?> VaultProperty =
        AvaloniaProperty.Register<CharacterControl, Vault?>(nameof(Vault));

    public Vault? Vault
    {
        get => GetValue(VaultProperty);
        set => SetValue(VaultProperty, value);
    }

    public VaultTab SelectedTab { get; set; }

    static VaultControl()
    {
        VaultProperty.Changed.AddClassHandler<VaultControl>((control, args) =>
        {
            if (control is VaultControl vaultControl && args.NewValue is Vault vault)
            {
                vaultControl.ViewModel.Vault = vault;
                vaultControl.UpdateVault();
            }
        });
    }

    public VaultControl()
    {
        InitializeComponent();

        if (!Design.IsDesignMode)
            Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);

        InitializeUI();
    }

    private void InitializeUI()
    {
        ItemsPanel.InitializeGrid();
    }

    public void Notify(object sender, MainWindowChangedEvent @event)
    {
        _cellSize = @event.CellSize;
        UpdateUI();
    }

    private void UpdateVault()
    {
        int i = 0;

        if (ViewModel.Vault is null)
            return;

        foreach (VaultTab tab in ViewModel.Vault.Tabs)
        {
            ToggleButton item = new()
            {
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                Margin = new Thickness(1, 0),
                Content = i + 1,
                HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Tag = tab
            };

            item.Classes.Add("ToggleButtonVaultTab");

            if (i == 0)
                item.IsChecked = true;

            item.IsCheckedChanged += (s, e) =>
            {
                if (s is ToggleButton sender && sender.IsChecked == true)
                {
                    foreach (ToggleButton button in Tabs__Container.Children.Cast<ToggleButton>())
                    {
                        if (button != sender)
                            button.IsChecked = false;
                    }

                    // TODO Move into view model
                    SelectedTab = (VaultTab)sender.Tag!;
                    ItemsPanel.Items = SelectedTab.Items;
                }
            };

            Tabs__Container.Children.Add(item);

            Grid.SetRow(item, 0);
            Grid.SetColumn(item, i);

            i++;
        }

        SelectedTab = ViewModel.Vault.Tabs[0];
        ItemsPanel.Items = SelectedTab.Items;
    }

    private void UpdateUI()
    {
        int itemsContainerWidth = Columns * _cellSize;
        int itemsContainerHeight = Rows * _cellSize;

        int borderThickness = 2;

        double sortButtonWidth = _cellSize * 0.75;
        double sortButtonHeight = sortButtonWidth * 3.5;

        double tabWidth = itemsContainerWidth / Tabs;
        double tabHeight = tabWidth * 0.8;

        double outerContainerWidth = itemsContainerWidth + (2 * borderThickness) + sortButtonWidth;
        double outerContainerHeight = itemsContainerHeight + (2 * borderThickness) + tabHeight;

        ControlContainer.Height = outerContainerHeight;
        ControlContainer.Width = outerContainerWidth;

        ControlContainer.RowDefinitions.Clear();
        ControlContainer.RowDefinitions.Add(new RowDefinition(tabHeight, GridUnitType.Pixel));
        ControlContainer.RowDefinitions.Add(new RowDefinition(borderThickness, GridUnitType.Pixel));
        ControlContainer.RowDefinitions.Add(new RowDefinition(1, GridUnitType.Star));
        ControlContainer.RowDefinitions.Add(new RowDefinition(borderThickness, GridUnitType.Pixel));

        ControlContainer.ColumnDefinitions.Clear();
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(sortButtonWidth, GridUnitType.Pixel));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(borderThickness, GridUnitType.Pixel));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(1, GridUnitType.Star));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(borderThickness, GridUnitType.Pixel));

        ItemsPanel.CellSize = _cellSize;
        ItemsPanel.UpdateUI();

        Button__Sort.Width = sortButtonWidth;
        Button__Sort.Height = sortButtonHeight;

        Tabs__Container.ColumnDefinitions.Clear();
        for (int i = 0; i < Tabs; i++)
            Tabs__Container.ColumnDefinitions.Add(new ColumnDefinition(tabWidth, GridUnitType.Pixel));
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }
}