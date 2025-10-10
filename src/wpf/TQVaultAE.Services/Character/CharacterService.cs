using System.ComponentModel.DataAnnotations;

namespace TQVaultAE.Services.Character
{
    /// <summary>
    /// Represents a service to fetch and update ingame <see cref="Models.CharacterData.Character"/> data.
    /// </summary>
    public class CharacterService
    {
        private readonly Dictionary<Guid, CharacterSet> _characters = [];

        /// <summary>
        /// Creates a new instance of the <see cref="CharacterService"/> class.
        /// </summary>
        public CharacterService()
        {
            // TODO Read character list
            // TOOD Set character strategies
        }

        /// <summary>
        /// Gets a list of all <see cref="Models.CharacterData.Character"/>s available on the system.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all available <see cref="Models.CharacterData.Character">Characters</see>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Models.Game.Character> GetCharacters()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates all data of the porivded <paramref name="character"/>.
        /// </summary>
        /// <param name="character">The <see cref="Models.CharacterData.Character"/> that should be updated.</param>
        /// <returns>A new <see cref="Models.CharacterData"/> instance containing the updated data.</returns>
        public Models.Game.Character GetCharacter(Guid characterId)
        {
            // TODO strategy might need to be updated if working on not ingame characters
            Models.Game.Character character = _characters[characterId].CharacterDataStrategy.Read(characterId);
            _characters[characterId].Character = character;

            return character;
        }

        /// <summary>
        /// Writes a <see cref="Models.CharacterData.Character"/> to game memory / file.
        /// </summary>
        /// <param name="character"></param>
        public void SaveCharacter(Guid characterId)
        {
            CharacterSet characterSet = _characters[characterId];
            characterSet.CharacterDataStrategy.Write(characterSet.Character);
        }

        private void SetCharacterDataStrategy(Guid characterId)
        {
            // IF config is live update and game is running and character is ingame
            _characters[characterId].CharacterDataStrategy = new MemoryCharacterDataStrategy();

            // else
            _characters[characterId].CharacterDataStrategy = new FileCharacterDataStrategy();
        }
    }

    public class CharacterSet
    {
        [Required]
        public Models.Game.Character Character { get; set; }

        [Required]
        public CharacterDataStrategy CharacterDataStrategy { get; set; }
    }
}
