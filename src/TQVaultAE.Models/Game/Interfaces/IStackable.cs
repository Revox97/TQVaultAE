namespace TQVaultAE.Models.Game.Interfaces
{
    public interface IStackable
    {
        int StackSize { get; set; }
        void AddToStack();
        void RemoveFromStack();
    }
}
