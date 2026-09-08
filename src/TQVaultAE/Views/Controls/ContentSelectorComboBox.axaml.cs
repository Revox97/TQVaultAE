using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TQVaultAE.Models;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Controls;

public partial class ContentSelectorComboBox : UserControl
{
    public static readonly RoutedEvent<SelectionChangedEventArgs> SelectionChangedEvent =
        RoutedEvent.Register<ContentSelectorComboBox, SelectionChangedEventArgs>(nameof(SelectionChanged), RoutingStrategies.Direct);

    public event EventHandler<SelectionChangedEventArgs> SelectionChanged
    {
        add => AddHandler(SelectionChangedEvent, value);
        remove => RemoveHandler(SelectionChangedEvent, value);
    }

    public ContentSelectorComboBox(Uri iconPath, List<string> values)
    {
        InitializeComponent();
        ContentSelectorComboBoxViewModel viewModel = new(iconPath, values.ConvertAll(x => new ItemContainer() { Name = x }));
        DataContext = viewModel;
    }

    protected virtual void OnValueChanged(SelectionChangedEventArgs args)
    {
        args.RoutedEvent = SelectionChangedEvent;
        RaiseEvent(args);
    }

    private void ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e) => OnValueChanged(e);
}