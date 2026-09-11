using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;

namespace TQVaultAE.ViewModels
{
    public sealed class LoadingWindowViewModel : ObservableObject
    {
        private readonly float _taskCount = 6; 

        public double Progress
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        
        // TODO Improve performance
        // Skip unnecessary data
        // Run requests in paralell
        // Find a better data structure to provide files. Search dictionary keys unfortunately, requires lots of resources
        public async Task LoadGameResourcesAsync()
        {
            if (Design.IsDesignMode)
                return;

            try
            {
                await Program.Services.GetRequiredService<ITitanQuestDatabaseService>().InitializeAsync();
                Progress += 100 / _taskCount;
                
                IGameIconService gameIconService = Program.Services.GetRequiredService<IGameIconService>();

                await gameIconService.InitializeAsync("Items");
                Progress += 100 / _taskCount;
                await gameIconService.InitializeAsync("xpack\\Items");
                Progress += 100 / _taskCount;
                await gameIconService.InitializeAsync("XPack2\\Items");
                Progress += 100 / _taskCount;
                await gameIconService.InitializeAsync("XPack3\\Items");
                Progress += 100 / _taskCount;
                await gameIconService.InitializeAsync("XPack4\\Item");
                Progress += 100 / _taskCount;

                App.Current!.Dispatcher.Invoke(() => Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new GameDataLoadedEvent()));
            }
            catch(Exception ex){

            }

        }
    }
}
