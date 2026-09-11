using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.Resources;
using TQVaultAE.Views.Pages;
using TQVaultAE.Views.Windows;

namespace TQVaultAE
{
    public partial class App : Avalonia.Application, IGameDataObserver
    {

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            if (!Design.IsDesignMode)
                Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Lang.Culture = new CultureInfo("en-US"); // TODO get language from settings

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                new LoadingWindow().Show();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public void Notify(object sender, GameDataLoadedEvent @event)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                MainPage content = new();
                desktop.MainWindow = new TQWindow(content, "TQVaultAE 5.0.0.0", isMainWindow: true);
                desktop.MainWindow.Show();
            }
        }

        public void Dispose()
        {
            Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
            GC.SuppressFinalize(this);
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