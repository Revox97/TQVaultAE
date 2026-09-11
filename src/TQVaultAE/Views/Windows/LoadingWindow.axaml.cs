using System;
using Avalonia.Controls;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Windows
{
    public partial class LoadingWindow : Window
    {
        private readonly LoadingWindowViewModel _viewModel = new();

        public LoadingWindow()
        {
            InitializeComponent();

            //Thread.Sleep(5000);

            //if (!Design.IsDesignMode)
            //    Program.Services.GetRequiredService<IEventDispatcher>().Dispatch(this, new GameDataLoadedEvent());
            //Close();
        }

        private void ButtonExit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}