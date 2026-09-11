using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Application.Services;
using TQVaultAE.Events;
using TQVaultAE.Persistence;
using TQVaultAE.Services;
using TQVaultAE.ViewModels;

namespace TQVaultAE
{
    internal sealed class Program
    {
        // TODO Move Services into a better location
        public static IServiceProvider Services { get; private set; } = null!;

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            Task.Run(() => InitializeServices());
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        private static async Task InitializeServices()
        {
            ServiceCollection services = new();
            //InitializeDbContextFactories(ref services);

            // Register viewModels, services, etc. here
            services.AddTransient<VaultPageViewModel>();
            services.AddTransient<AboutPageViewModel>();
            services.AddTransient<TQWindowViewModel>();
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<ContentSelectorComboBoxViewModel>();

            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddSingleton<IWindowResizeController, WindowResizeController>();
            services.AddSingleton<IVaultService, VaultService>();
            services.AddSingleton<IPlayerService, PlayerService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<ITitanQuestDatabaseService, TitanQuestDatabaseService>();
            services.AddSingleton<IGameIconService, GameIconService>();

            Services = services.BuildServiceProvider();

            // Migrate database
            using IServiceScope scope = Services.CreateScope();
            DataDbContext dataDb = scope.ServiceProvider.GetRequiredService<DataDbContext>();
            await dataDb.Database.MigrateAsync();

            ApplicationDbContext appDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await appDb.Database.MigrateAsync();

            // Create initial data
            // TODO implement better check for initial startup
            if (!await dataDb.Vaults.AnyAsync())
                await dataDb.CreateDatabaseAsync();

            if (!await appDb.Icons.AnyAsync())
                await appDb.CreateDatabaseAsync();
        }

        private static void InitializeDbContextFactories(ref ServiceCollection services)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appDirectory = Path.Combine(appData, "TQVaultAE");
            Directory.CreateDirectory(appDirectory);

            string databasePath = Path.Combine(appDirectory, "data.db");
            services.AddDbContextFactory<DataDbContext>(options => options.UseSqlite($"Data Source={databasePath}"));

            databasePath = Path.Combine(appDirectory, "application.db");
            services.AddDbContextFactory<DataDbContext>(options => options.UseSqlite($"Data Source={databasePath}"));
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
                        .UsePlatformDetect()
                        .WithInterFont()
                        .LogToTrace();
    }
}
