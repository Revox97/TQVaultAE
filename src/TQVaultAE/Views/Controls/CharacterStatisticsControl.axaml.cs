using System;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Model.Players;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class CharacterStatisticsControl : UserControl, IMainWindowChangedObserver
{
    public CharacterStatisticsViewModel ViewModel { get; set; } = new();

    private int _cellSize;

    public static readonly StyledProperty<Player?> PlayerProperty =
        AvaloniaProperty.Register<CharacterStatisticsControl, Player?>(nameof(Player));

    public Player? Player
    {
        get => GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    static CharacterStatisticsControl()
    {
        PlayerProperty.Changed.AddClassHandler<CharacterStatisticsControl>((control, args) =>
        {
            if (control is CharacterStatisticsControl cControl && args.NewValue is Player newValue)
                cControl.ViewModel.Player = newValue;
        });
    }

    public CharacterStatisticsControl()
    {
        InitializeComponent();

        if (!Design.IsDesignMode)
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

        if (_cellSize <= 30)
        {
            Resources["LabelFontSize"] = 10.0;
            return;
        }
        if (_cellSize <= 40)
        {
            Resources["LabelFontSize"] = 12.0;
            return;
        }
        if (_cellSize <= 50)
        {
            Resources["LabelFontSize"] = 14.0;
            return;
        }
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);
        GC.SuppressFinalize(this);
    }
}