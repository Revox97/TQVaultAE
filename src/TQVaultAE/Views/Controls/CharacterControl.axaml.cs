using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Model.Players;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class CharacterControl : UserControl, IMainWindowChangedObserver
{
    public CharacterControlViewModel ViewModel { get; set; } = new();

    private int _cellSize;

    public static readonly StyledProperty<Player?> PlayerProperty =
        AvaloniaProperty.Register<CharacterControl, Player?>(nameof(Player));

    public Player? Player
    {
        get => GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    static CharacterControl()
    {
        PlayerProperty.Changed.AddClassHandler<CharacterControl>((control, args) =>
        {
            if (control is CharacterControl characterControl && args.NewValue is Player newValue)
                characterControl.ViewModel.Player = newValue;
        });
    }

    public CharacterControl()
    {
        InitializeComponent();

        if (!Design.IsDesignMode)
            Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);

        InitializeUI();
    }

    private void InitializeUI()
    {
        ItemsPanel__StorageArea.InitializeUI();
        ItemsPanel__TransferArea.InitializeUI();
        ItemsPanel__RelicVault.InitializeUI();
    }

    public void Notify(object sender, MainWindowChangedEvent @event)
    {
        _cellSize = @event.CellSize;
        UpdateUI();
    }

    private void UpdateUI()
    {
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
}