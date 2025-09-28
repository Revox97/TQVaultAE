namespace TQVaultAE.Services.Character
{
    public abstract class CharacterDataStrategy
    {
        public abstract Models.CharacterData.Character Read(Guid id);

        public abstract Models.CharacterData.Character Read(Models.CharacterData.Character character);

        public abstract void Write(Models.CharacterData.Character data);
    }
}
