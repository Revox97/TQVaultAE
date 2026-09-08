using System.Globalization;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TQVaultAE.Resources;
using TQVaultAE.Views.Pages;
using TQVaultAE.Views.Windows;

namespace TQVaultAE
{
    public partial class App : Avalonia.Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Lang.Culture = new CultureInfo("en-US"); // TODO get language from settings

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // TODO Update startup logic
                MainPage content = new();
                desktop.MainWindow = new TQWindow(content, "TQVaultAE 5.0.0.0", isMainWindow: true);
            }

            base.OnFrameworkInitializationCompleted();
        }

        // Todo Create localization service
        //public void SwitchLanguage(string cultureCode)
        //{
        //    Lang.Culture = new CultureInfo(cultureCode);
        //    // Raise PropertyChanged for all localized properties
        //    // or reload the view to pick up new strings
        //}
    }
}