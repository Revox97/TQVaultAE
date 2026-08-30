using System;
using Avalonia.Controls;
using TQVaultAE.Observers;
using TQVaultAE.Observers.EventArgs;
using TQVaultAE.Services;

namespace TQVaultAE.Views.Controls;

public partial class CharacterControl : UserControl, IWindowResizeObserver
{
    private int _cellSize;

    public CharacterControl()
    {
        InitializeComponent();

        WindowResizeController.GetInstance().AddObserver(this);
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

    public void Update(WindowSizeChangedEventArgs args)
    {
        _cellSize = args.CellSize;
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
        WindowResizeController.GetInstance().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }

    private void ItemsPanel__PlayerStatistics_ActualThemeVariantChanged(object? sender, EventArgs e)
    {
    }
}