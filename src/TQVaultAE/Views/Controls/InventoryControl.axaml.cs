using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Services;

namespace TQVaultAE.Views.Controls;

public partial class InventoryControl : UserControl, IMainWindowChangedObserver
{
    private int _cellSize;

    public InventoryControl()
    {
        InitializeComponent();

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

        double sortButtonHeight = _cellSize;
        double sortButtonWidth = sortButtonHeight * 4;

        double tabWidth = _cellSize * 1.2;
        double tabHeight = _cellSize;

        ControlContainer.RowDefinitions.Clear();
        ControlContainer.RowDefinitions.Add(new RowDefinition(tabHeight, GridUnitType.Pixel));
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

        Button__Sort_Main.Width = sortButtonWidth;
        Button__Sort_Main.Height = sortButtonHeight;

        Button__Sort_Side.Width = sortButtonWidth;
        Button__Sort_Side.Height = sortButtonHeight;

        Tabs__Container.ColumnDefinitions.Clear();
        for (int i = 0; i < 3; i++)
            Tabs__Container.ColumnDefinitions.Add(new ColumnDefinition(tabWidth, GridUnitType.Pixel));

        Tabs__Container.ColumnDefinitions.Add(new ColumnDefinition(1, GridUnitType.Star));
        Tabs__Container.ColumnDefinitions.Add(new ColumnDefinition(sortButtonWidth, GridUnitType.Pixel));
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }
}