using CommunityToolkit.Mvvm.ComponentModel;
using TQVaultAE.Model.Players;

namespace TQVaultAE.ViewModels
{
    public sealed class InventoryControlViewModel : ObservableObject
    {
        public Sack? SackMain
        {
            get;
            set => SetProperty(ref field, value);
        }

        public Sack? SelectedSideSack
        {
            get;
            set => SetProperty(ref field, value);
        }

        public Sack? SackSecundary
        {
            get;
            set => SetProperty(ref field, value);
        }

        public Sack? SackTertiary
        {
            get;
            set => SetProperty(ref field, value);
        }

        public Sack? SackQuartiary
        {
            get;
            set => SetProperty(ref field, value);
        }

        public int SackCount
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}
