namespace TQVaultAE.Models.Game.Interfaces
{
    public interface IStackingComponent
    {
        int MaxStackSize { get; }
        int StackSize { get; }
        bool IsMaximum { get; }
        void AddToStack();
        void RemoveFromStack();
    }
}
