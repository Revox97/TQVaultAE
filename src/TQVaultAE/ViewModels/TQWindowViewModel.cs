using System;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Rendering.Composition;
using CommunityToolkit.Mvvm.Input;
using TQVaultAE.Views;

namespace TQVaultAE.ViewModels
{
    internal class TQWindowViewModel : ViewModelBase
    {
        private readonly TQWindow _window;

        // Needed for designer
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public TQWindowViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public TQWindowViewModel(TQWindow window, string title, double initialWidth, double initialHeight, bool allowResize, WindowCloseAction closeAction)
        {
            _window = window;
            Title = title;
            Height = initialHeight;
            Width = initialWidth;
            CanResize = allowResize;
            CloseAction = closeAction;
        }

        public ICommand CloseCommand => new RelayCommand(() =>
        {
            if (CloseAction == WindowCloseAction.ExitApplication)
                Environment.Exit(0);
            else
                _window.Close();
        });

        public ICommand MinimizeCommand => new RelayCommand(() => _window.WindowState = WindowState.Minimized);

        public ICommand MaximizeCommand => new RelayCommand(() =>
        {
            // TODO Unfortunately WPF border thickness workaround does not work, figure out another way
            if (_window.WindowState != WindowState.Maximized)
            {
                _window.WindowState = WindowState.Maximized;
                return;
            }

            _window.WindowState = WindowState.Normal;
        });

        public string Title { get; set; } = "TQVaultAE 5.0.0.0";

        public double Width { get; set; } = 1100d;
        public double Height { get; set; } = 800d;

        public bool CanResize { get; set; } = true;

        public WindowCloseAction CloseAction { get; set; } = WindowCloseAction.CloseWindow;
    }

    public enum WindowCloseAction
    {
        ExitApplication,
        CloseWindow
    }
}
