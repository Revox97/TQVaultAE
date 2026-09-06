using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TQVaultAE.Views.Pages;
using TQVaultAE.Views.Windows;

namespace TQVaultAE
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // TODO Update startup logic
                MainPage content = new();
                desktop.MainWindow = new TQWindow(content, "TQVaultAE 5.0.0.0", isMainWindow: true);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}