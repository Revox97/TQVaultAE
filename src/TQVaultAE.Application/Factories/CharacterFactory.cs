using TQVaultAE.Model;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.Application.Factories
{
    internal sealed class CharacterFactory
    {
        // TODO works, but files always seem to be structured in the same way, so parsing correct data should be possible
        internal Character CreateCharacterFromChrFile(ChrFile input)
        {
            string playerClass = input.Root.FindChild("playerCharacterClass")!.AsString();
            int playerLevel = input.Root.FindChild("playerLevel")!.AsInt32();
            string playerName = input.Root.FindElement("myPlayerName")!.AsString();

            // TODO How to address blocks. Are ids always the same?


            return new();
        }
    }
}
