using CommunityToolkit.Mvvm.ComponentModel;
using TQVaultAE.Model.Vaults;

namespace TQVaultAE.ViewModels
{
    public class VaultControlViewModel : ObservableObject
    {
        public Vault? Vault
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}
