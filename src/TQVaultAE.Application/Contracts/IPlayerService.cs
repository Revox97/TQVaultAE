using TQVaultAE.Model.Players;

namespace TQVaultAE.Application.Contracts
{
    public interface IPlayerService
    {
        Task<List<string>> GetPlayerNamesAsync();
        Task<Player> GetPlayerByNameAsync(string name);
    }
}
