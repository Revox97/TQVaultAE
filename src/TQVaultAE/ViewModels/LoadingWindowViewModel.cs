using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Application.Contracts;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Localisation;

namespace TQVaultAE.ViewModels
{
    public sealed class LoadingWindowViewModel : ObservableObject
    {
        private static readonly SemaphoreSlim s_taskCompletionSemaphore = new(1, 1);

        private readonly float _taskCount = 9;

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
                IGameIconService gameIconService = Program.Services.GetRequiredService<IGameIconService>();

                List<Task> initalizationTasks = [];

                initalizationTasks.Add(HandleTaskAsync(Program.Services.GetRequiredService<ITitanQuestDatabaseService>().InitializeAsync()));
                initalizationTasks.Add(HandleTaskAsync(gameIconService.InitializeAsync("Items")));
                initalizationTasks.Add(HandleTaskAsync(gameIconService.InitializeAsync("Menu")));
                initalizationTasks.Add(HandleTaskAsync(gameIconService.InitializeAsync("InGameUI")));
                initalizationTasks.Add(HandleTaskAsync(gameIconService.InitializeAsync("xpack\\Items")));
                initalizationTasks.Add(HandleTaskAsync(gameIconService.InitializeAsync("XPack2\\Items")));
                initalizationTasks.Add(HandleTaskAsync(gameIconService.InitializeAsync("XPack3\\Items")));
                initalizationTasks.Add(HandleTaskAsync(gameIconService.InitializeAsync("XPack4\\Item")));
                initalizationTasks.Add(HandleTaskAsync(Program.Services.GetRequiredService<IGameLocalizationService>().InitializeAsync()));

                Task.WaitAll(initalizationTasks);
                App.Current!.Dispatcher.Invoke(() => Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new GameDataLoadedEvent()));
            }
            catch (Exception ex)
            {

            }
        }

        public async Task HandleTaskAsync(Task task)
        {
            await task;

            try
            {
                s_taskCompletionSemaphore.Wait();
                Progress += 100 / _taskCount;
            }
            finally
            {
                s_taskCompletionSemaphore.Release();
            }

        }
    }
}
