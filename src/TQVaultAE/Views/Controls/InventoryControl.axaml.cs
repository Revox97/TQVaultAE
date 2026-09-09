using System;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Model.Players;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class InventoryControl : UserControl, INotifyPropertyChanged, IMainWindowChangedObserver
{
    public InventoryControlViewModel ViewModel { get; } = new();

    private int _cellSize;

    public static readonly StyledProperty<Player?> PlayerProperty =
        AvaloniaProperty.Register<InventoryControl, Player?>(nameof(Player));

    public Player? Player
    {
        get => GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    static InventoryControl()
    {
        PlayerProperty.Changed.AddClassHandler<InventoryControl>((control, args) =>
        {
            if (control is InventoryControl iControl && args.NewValue is Player newValue)
            {
                iControl.ViewModel.SackMain = newValue.Sacks[0];
                iControl.ViewModel.SelectedSideSack = null;

                if (newValue.SackCount > 1)
                {
                    iControl.ViewModel.SackSecundary = newValue.Sacks[1];
                    iControl.ViewModel.SelectedSideSack = iControl.ViewModel.SackSecundary;
                    iControl.ToggleButton__SideSackTabOne.IsChecked = true;
                }

                if (newValue.SackCount > 2)
                    iControl.ViewModel.SackTertiary = newValue.Sacks[2];

                if (newValue.SackCount > 3)
                    iControl.ViewModel.SackQuartiary = newValue.Sacks[3];
            }
        });
    }

    public InventoryControl()
    {
        InitializeComponent();

        if (!Design.IsDesignMode)
            Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);

        InitializeUI();
    }

    private void InitializeUI()
    {
        ItemsPanelMain.Columns = 12;
        ItemsPanelMain.Rows = 5;
        ItemsPanelMain.InitializeUI();
        ItemsPanelSide.Columns = 8;
        ItemsPanelSide.Rows = 5;
        ItemsPanelSide.InitializeUI();
    }

    public void Notify(object sender, MainWindowChangedEvent @event)
    {
        _cellSize = @event.CellSize;
        UpdateUI();
    }

    private void UpdateUI()
    {
        int borderThickness = 2;

        double sortButtonHeight = _cellSize * 0.8;
        double sortButtonWidth = _cellSize * 2.75;

        //double tabWidth = _cellSize * 1.2;
        //double tabHeight = _cellSize;

        // Calculated like in VaultControl, needs to be done somewhere else
        int itemsContainerWidth = 18 * _cellSize;

        double tabColumnWidth = itemsContainerWidth / 12;
        double tabRowHeight = tabColumnWidth * 0.8;
        //double tabWidth = _cellSize * 0.8;
        //double tabHeight = tabWidth;

        ControlContainer.RowDefinitions.Clear();
        ControlContainer.RowDefinitions.Add(new RowDefinition(tabRowHeight, GridUnitType.Pixel));
        ControlContainer.RowDefinitions.Add(new RowDefinition(borderThickness, GridUnitType.Pixel));
        ControlContainer.RowDefinitions.Add(new RowDefinition(1, GridUnitType.Star));
        ControlContainer.RowDefinitions.Add(new RowDefinition(borderThickness, GridUnitType.Pixel));

        ControlContainer.ColumnDefinitions.Clear();
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(borderThickness, GridUnitType.Pixel));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(1, GridUnitType.Auto));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(borderThickness, GridUnitType.Pixel));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(4, GridUnitType.Pixel));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(borderThickness, GridUnitType.Pixel));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(1, GridUnitType.Auto));
        ControlContainer.ColumnDefinitions.Add(new ColumnDefinition(borderThickness, GridUnitType.Pixel));

        InvalidateMeasure();
        InvalidateArrange();
        ControlContainer.InvalidateMeasure();
        ControlContainer.InvalidateArrange();
        ControlContainer.UpdateLayout();

        ItemsPanelMain.CellSize = _cellSize;
        ItemsPanelSide.CellSize = _cellSize;
        ItemsPanelMain.UpdateUI();
        ItemsPanelSide.UpdateUI();

        //MissingSacksCover.IsVisible = DataSource.SackCount > 1;

        //if (_selectedSideBagIndex < DataSource.SackCount)
        //    _selectedSideBagIndex = DataSource.SackCount;

        Button__Sort_Main.Width = sortButtonWidth;
        Button__Sort_Main.Height = sortButtonHeight;

        Button__Sort_Side.Width = sortButtonWidth;
        Button__Sort_Side.Height = sortButtonHeight;

        Tabs__Container.ColumnDefinitions.Clear();
        // TODO remove bag icon if player has not 4 bags
        for (int i = 0; i < 3; i++)
            Tabs__Container.ColumnDefinitions.Add(new ColumnDefinition(tabColumnWidth, GridUnitType.Pixel));

        Tabs__Container.ColumnDefinitions.Add(new ColumnDefinition(1, GridUnitType.Star));
        Tabs__Container.ColumnDefinitions.Add(new ColumnDefinition(sortButtonWidth, GridUnitType.Pixel));
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }

    private void ToggleButton__SideSackTabOne_IsCheckedChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is ToggleButton toggleButton)
        {
            if (toggleButton.IsChecked  == true)
            {
                foreach (ToggleButton button in Tabs__Container.Children.Where(x => x.GetType() == typeof(ToggleButton)))
                {
                    if (button != toggleButton)
                        button.IsChecked = false;
                }

                if (toggleButton == ToggleButton__SideSackTabOne)
                {
                    ViewModel.SelectedSideSack = ViewModel.SackSecundary;
                    return;
                }

                if (toggleButton == ToggleButton__SideSackTabTwo)
                {
                    ViewModel.SelectedSideSack = ViewModel.SackTertiary;
                    return;
                }

                if (toggleButton == ToggleButton__SideSackTabThree)
                {
                    ViewModel.SelectedSideSack = ViewModel.SackQuartiary;
                    return;
                }
            }
        }
    }
}