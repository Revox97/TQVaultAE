using TQVaultAE.Model;
using TQVaultAE.TitanQuestDataProviders.Model;
using TQVaultAE.TitanQuestDataProviders.SaveGame;

namespace TQVaultAE.Application.Services
{
    public class CharacterService
    {
        public async Task<Character> GetCharacterAsync(string name)
        {
            // TODO Get file path from name
            string path = "";

            ChrProvider chrProvider = new();
            ChrFile characterFile = await chrProvider.ReadAsync(path).ConfigureAwait(false);


            return new();
        }
    }
}
