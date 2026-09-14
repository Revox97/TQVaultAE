using CommunityToolkit.Mvvm.ComponentModel;
using TQVaultAE.Model.Players;

namespace TQVaultAE.ViewModels
{
    public class EquipmentControlViewModel : ObservableObject
    {
        public Equipment? Equipment
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}
