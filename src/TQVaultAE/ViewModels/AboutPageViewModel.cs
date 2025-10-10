using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using TQVaultAE.Views;

namespace TQVaultAE.ViewModels
{
    internal class AboutPageViewModel
    {
        private readonly AboutPage _page;

        // Needed for Designer
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public AboutPageViewModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public AboutPageViewModel(AboutPage page) => _page = page;

        public ICommand CloseWindow => new RelayCommand(() =>
        {
            Window? window = TopLevel.GetTopLevel(_page) as Window;
            window?.Close();
        });
    }
}
