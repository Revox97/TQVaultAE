using System;
using System.Windows.Input;
using Avalonia.Controls;
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

        public TQWindowViewModel(TQWindow window)
        {
            _window = window;
        }

        public static ICommand CloseCommand => new RelayCommand(() => Environment.Exit(0));

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
    }
}
