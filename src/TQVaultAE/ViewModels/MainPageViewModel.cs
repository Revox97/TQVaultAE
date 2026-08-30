using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using TQVaultAE.Views.Pages;
using TQVaultAE.Views.Windows;

namespace TQVaultAE.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            _currentPage = VaultPage;
        }

        private Control _currentPage;

        public Control CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public VaultPage VaultPage { get; set; } = new VaultPage();

        public SearchPage SearchPage { get; set; } = new SearchPage();

        public SettingsPage SettingsPage { get; set; } = new SettingsPage();

        internal void SwitchPage(Control newPage) => CurrentPage = newPage;

        public ICommand SaveCommand => new RelayCommand(() => { });

        internal static void OpenAboutWindow() => new TQWindow(new AboutPage(), "About TQVaultAE", 575, 400, false, WindowCloseAction.CloseWindow) { Title = "About TQVaultAE" }.Show();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
