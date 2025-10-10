using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Interactivity;
using TQVaultAE.ViewModels;
using Avalonia.Media;

namespace TQVaultAE.Views;

public partial class TQWindow : Window
{
    // Needed for Designer
    public TQWindow()
    {
        InitializeComponent();
        ContentContainer.Children.Add(new Border()
        {
            Child = new Label()
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Content = "Page content",
                Background = new SolidColorBrush(Colors.Blue),
                Foreground = new SolidColorBrush(Colors.White)
            }
        });
    }

    public TQWindow(Control content)
    {
        InitializeComponent();
        DataContext = new TQWindowViewModel(this);

        ContentContainer.Children.Add(content);
    }

    public void ButtonClose_Click(object? sender, RoutedEventArgs args)
    {
        Environment.Exit(0);
    }
}