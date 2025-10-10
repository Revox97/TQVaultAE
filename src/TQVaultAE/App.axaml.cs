using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using TQVaultAE.Views;

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
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();

                // TODO Update startup logic
                MainPage content = new();
                desktop.MainWindow = new TQWindow(content);
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            DataAnnotationsValidationPlugin[] dataValidationPluginsToRemove = [.. BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>()];

            // remove each entry found
            foreach (DataAnnotationsValidationPlugin? plugin in dataValidationPluginsToRemove)
                BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}