using System;
using Avalonia.Controls;
using TQVaultAE.Observers;
using TQVaultAE.Observers.EventArgs;
using TQVaultAE.Services;

namespace TQVaultAE.Views.Controls;

public partial class CharacterStatisticsControl : UserControl, IWindowResizeObserver
{
    private int _cellSize;

    public CharacterStatisticsControl()
    {
        InitializeComponent();
        WindowResizeController.GetInstance().AddObserver(this);
    }

    public void Update(WindowSizeChangedEventArgs args)
    {
        _cellSize = args.CellSize;
        UpdateUI();
    }

    private void UpdateUI()
    {
        double newWidth = (6 * _cellSize) - 4;
        double newMaxHeight = 15 * _cellSize;
        CharacterStatistics__Container.Width = newWidth;
        CharacterStatistics__Container.Width = newWidth;

        CharacterStatistics__Container.MaxHeight = newMaxHeight;
    }

    public void Dispose()
    {
        WindowResizeController.GetInstance().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }
}