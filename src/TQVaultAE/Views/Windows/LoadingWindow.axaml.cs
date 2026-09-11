using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using TQVaultAE.Events;
using TQVaultAE.Events.Events;
using TQVaultAE.Events.Observers;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Windows
{
    public partial class LoadingWindow : Window, IGameDataObserver
    {
        private readonly LoadingWindowViewModel _viewModel = new();

        public LoadingWindow()
        {
            InitializeComponent();

            if (!Design.IsDesignMode)
                Program.Services.GetRequiredService<IEventDispatcher>().AddObserver(this);
            
            DataContext = _viewModel;
            Task.Run(() => _viewModel.LoadGameResourcesAsync());
        }

        public void Notify(object sender, GameDataLoadedEvent @event) => Close();

        private void ButtonExit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => Environment.Exit(0);

        public void Dispose()
        {
            Program.Services.GetRequiredService<IEventDispatcher>().RemoveObserver(this);
            GC.SuppressFinalize(this);
        }
    }
}