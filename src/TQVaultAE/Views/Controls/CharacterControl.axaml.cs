using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Views.Controls;

public partial class CharacterControl : UserControl, IMainWindowChangedObserver
{
    private int _cellSize;

    public CharacterControl()
    {
        InitializeComponent();

        Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);

        InitializeUI();
    }

    private void InitializeUI()
    {
        ItemsPanel__StorageArea.Columns = 16;
        ItemsPanel__StorageArea.Rows = 15;
        ItemsPanel__StorageArea.InitializeUI();

        ItemsPanel__TransferArea.Columns = 16;
        ItemsPanel__TransferArea.Rows = 15;
        ItemsPanel__TransferArea.InitializeUI();

        ItemsPanel__RelicVault.Columns = 16;
        ItemsPanel__RelicVault.Rows = 15;
        ItemsPanel__RelicVault.InitializeUI();
    }

    public void Notify(object sender, MainWindowChangedEvent @event)
    {
        _cellSize = @event.CellSize;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //double sortButtonHeight = _cellSize;
        //double sortButtonWidth = sortButtonHeight * 4;

        //double tabWidth = _cellSize * 1.2;
        //double tabHeight = _cellSize;

        InvalidateMeasure();
        InvalidateArrange();

        ItemsPanel__StorageArea.CellSize = _cellSize;
        ItemsPanel__TransferArea.CellSize = _cellSize;
        ItemsPanel__RelicVault.CellSize = _cellSize;
        ItemsPanel__StorageArea.UpdateUI();
        ItemsPanel__TransferArea.UpdateUI();
        ItemsPanel__RelicVault.UpdateUI();

        // TODO Add sort button to UI
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }

    private void ItemsPanel__PlayerStatistics_ActualThemeVariantChanged(object? sender, EventArgs e)
    {
    }
}