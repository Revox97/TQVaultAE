using CommunityToolkit.Mvvm.ComponentModel;
using TQVaultAE.Model.Players;

namespace TQVaultAE.ViewModels
{
    public class CharacterStatisticsViewModel : ObservableObject
    {
        public Player? Player
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}
