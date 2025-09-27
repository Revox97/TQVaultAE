namespace TQVaultAE.Services.Character
{
    /// <summary>
    /// Represents a service to fetch and update ingame <see cref="Models.CharacterData.Character"/> data.
    /// </summary>
    public class CharacterService
    {
        // TODO Make it so it can be updated during runtime
        private readonly CharacterDataStrategy _characterDataStrategy = new FileCharacterDataStrategy();

        /// <summary>
        /// Creates a new instance of the <see cref="CharacterService"/> class.
        /// </summary>
        public CharacterService()
        {
            _characterDataStrategy = GetCharacterDataStrategy();
        }

        /// <summary>
        /// Gets a <see cref="Models.CharacterData.Character"/> for the provided <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Models.CharacterData.Character"/>.</param>
        /// <returns>A new <see cref="Models.CharacterData.Character"/> if a <see cref="Models.CharacterData.Character"/> with the <paramref name="name"/> exists. Otherwise <see langword="null"/>.</returns>
        public Models.CharacterData.Character? GetCharacterByName(string name)
        {
            return _characterDataStrategy.Read(name);
        }

        /// <summary>
        /// Updates all data of the porivded <paramref name="character"/>.
        /// </summary>
        /// <param name="character">The <see cref="Models.CharacterData.Character"/> that should be updated.</param>
        /// <returns>A new <see cref="Models.CharacterData"/> instance containing the updated data.</returns>
        public Models.CharacterData.Character GetCharacter(Models.CharacterData.Character character)
        {
            return _characterDataStrategy.Read(character);
        }

        /// <summary>
        /// Writes a <see cref="Models.CharacterData.Character"/> to game memory / file.
        /// </summary>
        /// <param name="character"></param>
        public void SaveCharacter(Models.CharacterData.Character character)
        {
            _characterDataStrategy.Write(character);
        }

        private static CharacterDataStrategy GetCharacterDataStrategy()
        {
            // IF config is live update and game is running
            return new MemoryCharacterDataStrategy();

            // else
            return new FileCharacterDataStrategy();
        }
    }
}
