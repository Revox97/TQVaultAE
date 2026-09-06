using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Views.Controls;

public partial class EquipmentControl : UserControl, IMainWindowChangedObserver
{
    private int _cellSize;

    public EquipmentControl()
    {
        InitializeComponent();
        Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);
    }

    public void Notify(object sender, MainWindowChangedEvent @event)
    {
        _cellSize = @event.CellSize;
        UpdateUI();
    }

    private void UpdateUI()
    {
        double newHeight = 15 * _cellSize;
        double newWidth = 10 * _cellSize;

        Equipment__Container.RowDefinitions.Clear();
        Equipment__Container.ColumnDefinitions.Clear();

        // 15
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * 1.5, GridUnitType.Pixel)); //  13.5
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * 2, GridUnitType.Pixel)); //  11.5
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * .5, GridUnitType.Pixel)); //  11
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize, GridUnitType.Pixel)); //  10
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * .5, GridUnitType.Pixel)); //  9.5

        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize, GridUnitType.Pixel)); //  8.5
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * 2, GridUnitType.Pixel)); //  6.5

        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * .5, GridUnitType.Pixel)); //  6
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * 2, GridUnitType.Pixel)); //  4
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * .5, GridUnitType.Pixel)); //  3.5
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize, GridUnitType.Pixel)); //  2.5
        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize , GridUnitType.Pixel)); // 1.5

        Equipment__Container.RowDefinitions.Add(new RowDefinition(_cellSize * 1.5, GridUnitType.Pixel)); //  0

        // 10
        Equipment__Container.ColumnDefinitions.Add(new ColumnDefinition(_cellSize * 1.5, GridUnitType.Pixel)); //  8.5
        Equipment__Container.ColumnDefinitions.Add(new ColumnDefinition(_cellSize * 2, GridUnitType.Pixel)); //  6.5

        Equipment__Container.ColumnDefinitions.Add(new ColumnDefinition(_cellSize * .5, GridUnitType.Pixel)); //  6

        Equipment__Container.ColumnDefinitions.Add(new ColumnDefinition(_cellSize * 2, GridUnitType.Pixel)); //  4

        Equipment__Container.ColumnDefinitions.Add(new ColumnDefinition(_cellSize * .5, GridUnitType.Pixel)); //  3.5

        Equipment__Container.ColumnDefinitions.Add(new ColumnDefinition(_cellSize * 2, GridUnitType.Pixel)); //  1.5
        Equipment__Container.ColumnDefinitions.Add(new ColumnDefinition(_cellSize * 1.5, GridUnitType.Pixel)); //  8.5
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }
}