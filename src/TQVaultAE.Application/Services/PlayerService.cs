using TQVaultAE.Application.Contracts;
using TQVaultAE.Application.Factories;
using TQVaultAE.FileFormats.Chr;
using TQVaultAE.Model.Players;
using TQVaultAE.TitanQuestDataProviders.SaveGame;

namespace TQVaultAE.Application.Services
{
    public class PlayerService : IPlayerService
    {
        // TODO Get path dynamically, hardcoded for testing purposes
        private readonly string _saveDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TQVaultTestData\Main");

        public async Task<List<string>> GetPlayerNamesAsync()
        {
            string[] playerFiles = Directory.GetDirectories(_saveDataPath);
            return playerFiles.ToList().ConvertAll(x => Path.GetFileName(x)[1..]);
        }

        public async Task<Player> GetPlayerByNameAsync(string name)
        {
            string path = Path.Combine(_saveDataPath, '_' + name, "Player.chr");

            ChrProvider chrProvider = new();
            ChrFile playerFile = await chrProvider.ReadAsync(path).ConfigureAwait(false);

            return new PlayerFactory().CreateCharacterFromChrFile(playerFile);
        }
    }
}
