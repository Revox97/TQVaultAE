using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Interactivity;
using TQVaultAE.ViewModels;
using Avalonia.Media;
using Avalonia.Input;

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

    public TQWindow(Control content, string title, double initialWidth = 1100d, double initialHeight = 800d, bool allowResize = true, WindowCloseAction closeAction = WindowCloseAction.ExitApplication)
    {
        InitializeComponent();
        DataContext = new TQWindowViewModel(this, title, initialWidth, initialHeight, allowResize, closeAction);

        ContentContainer.Children.Add(content);
        BorderTopCenter.PointerPressed += BorderTopCenterMouseDown;
    }

    private void BorderTopCenterMouseDown(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(null).Properties.IsLeftButtonPressed && VisualRoot is Window window)
                window.BeginMoveDrag(e);
    }

    public void ButtonClose_Click(object? sender, RoutedEventArgs args)
    {
        Environment.Exit(0);
    }
}