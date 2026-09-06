using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Model.Vaults;
using TQVaultAE.Services;

namespace TQVaultAE.Views.Controls;

public partial class VaultControl : UserControl, IMainWindowChangedObserver
{
    private const int Rows = 20;
    private const int Columns = 18;
    private const int Tabs = 12;

    private int _cellSize = 0;

    public Vault DataSource { get; set; }
    public VaultTab SelectedTab { get; set; }

    public VaultControl()
    {
        InitializeComponent();

        Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);

        DataSource = new Vault();
        DataContext = DataSource;

        InitializeUI();
    }

    private void InitializeUI()
    {
        int i = 0;

        foreach(VaultTab tab in DataSource.Tabs)
        {
            Bitmap bitmap = new(AssetLoader.Open(tab.Icon.IconDown.Uri));
            Border item = new()
            {
                Background = new ImageBrush(bitmap),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                Margin = new Thickness(1, 0),
            };

            Tabs__Container.Children.Add(item);

            Grid.SetRow(item, 0);
            Grid.SetColumn(item, i);

            i++;
        }

        SelectedTab = DataSource.Tabs[0];
        ((Border)(Tabs__Container.Children[0])).Background = new ImageBrush(new Bitmap(AssetLoader.Open(SelectedTab.Icon.IconUp.Uri)));
        ItemsPanel.InitializeUI();

        //for (int i = 0; i < Tabs; i++)
        //{
        //    Bitmap bitmap = new(AssetLoader.Open(new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_down.png")));
        //    Border item = new()
        //    {
        //        Background = new ImageBrush(bitmap),
        //        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
        //        VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
        //        Margin = new Thickness(1, 0)
        //    };

        //    Tabs__Container.Children.Add(item);

        //    Grid.SetRow(item, 0);
        //    Grid.SetColumn(item, i);
        //}
    }

    public void Notify(object sender, MainWindowChangedEvent @event)
    {
        _cellSize = @event.CellSize;
        UpdateUI();
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

        // TODO get tabs from view model and update them
    }

    public void Dispose()
    {
        Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
        GC.SuppressFinalize(this);
    }
}