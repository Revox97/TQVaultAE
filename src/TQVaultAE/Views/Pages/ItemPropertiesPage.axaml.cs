using Avalonia.Controls;
using TQVaultAE.Model.Items;
using TQVaultAE.ViewModels;

namespace TQVaultAE.Views.Pages
{
    public partial class ItemPropertiesPage : ContentPage
    {
        private readonly ItemPropertiesPageViewModel _viewModel;

        // Design time constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ItemPropertiesPage() => InitializeComponent();
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public ItemPropertiesPage(Item dataContext)
        {
            InitializeComponent();

            _viewModel = new ItemPropertiesPageViewModel(dataContext);
        }
    }
}