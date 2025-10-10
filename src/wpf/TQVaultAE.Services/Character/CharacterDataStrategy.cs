namespace TQVaultAE.Services.Character
{
    public abstract class CharacterDataStrategy
    {
        public abstract  Models.Game.Character Read(Guid id);

        public abstract Models.Game.Character Read(Models.Game.Character character);

        public abstract void Write(Models.Game.Character data);
    }
}
