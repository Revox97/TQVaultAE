using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;

namespace TQVaultAE.Views.Controls;

public partial class CharacterStatisticsControl : UserControl, IMainWindowChangedObserver
{
    private int _cellSize;

    public CharacterStatisticsControl()
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
        double newWidth = (6 * _cellSize) - 4;
        double newMaxHeight = 15 * _cellSize;
        CharacterStatistics__Container.Width = newWidth;
        CharacterStatistics__Container.Width = newWidth;

        CharacterStatistics__Container.MaxHeight = newMaxHeight;
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);
        GC.SuppressFinalize(this);
    }
}