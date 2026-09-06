using System;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Interactivity;
using TQVaultAE.ViewModels;
using Avalonia.Media;
using Avalonia.Input;
using TQVaultAE.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TQVaultAE.Views.Windows;

public partial class TQWindow : Window
{
    private readonly bool _isMainWindow;

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

    public TQWindow(Control content, string title, double initialWidth = 1300d, double initialHeight = 900d, bool allowResize = true, WindowCloseAction closeAction = WindowCloseAction.ExitApplication, bool isMainWindow = false)
    {
        InitializeComponent();
        DataContext = new TQWindowViewModel(this, title, initialWidth, initialHeight, allowResize, closeAction);

        _isMainWindow = isMainWindow;
        ContentContainer.Children.Add(content);
        BorderTopCenter.PointerPressed += BorderTopCenterMouseDown;
    }

    private void BorderTopCenterMouseDown(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(null).Properties.IsLeftButtonPressed && VisualRoot!.Parent is Window window)
            window.BeginMoveDrag(e);
    }

    public void ButtonClose_Click(object? sender, RoutedEventArgs args)
    {
        Environment.Exit(0);
    }

    private void Window_Resized(object? sender, WindowResizedEventArgs e)
    {
        if (_isMainWindow)
        {
            IWindowResizeController windowResizeController = Program.Services.GetRequiredService<IWindowResizeController>();
            windowResizeController.Invoke(e);
        }
    }
}