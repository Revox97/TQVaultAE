using System.CodeDom;
using System.IO;
using System.Text;

namespace TQVaultAE.IO
{
    public class CharacterFileReader
    {
        // TODO Replace with path from config
        private const string Path = @"C:\Users\Leo\Documents\TQVaultTestData\Main";

        public static List<string> GetAvailableCharacters()
        {
            return [];
        }

        public Models.CharacterData.Character ReadPlayerFile(string path)
        {
            TitanQuestFile record = new TitanQuestFileService().ReadFile(System.IO.Path.Combine(Path, "_Templox", "Player.chr"));
            List<TitanQuestFilePlayerRecord> data = [.. record.Records.Where(d => d.GetType() == typeof(TitanQuestFilePlayerRecord))
                .Select(r => (TitanQuestFilePlayerRecord)r)];

            var result = new Models.CharacterData.Character()
            {
                Name = data.FirstOrDefault(d => d.KeyName == "myPlayerName")?.DataAsStr ?? string.Empty,
            };

            return result;
        }
    }
}
