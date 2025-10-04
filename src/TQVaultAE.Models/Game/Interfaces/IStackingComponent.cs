namespace TQVaultAE.Models.Game.Interfaces
{
    public interface IStackingComponent
    {
        int MaxStackSize { get; }
        int StackSize { get; }
        bool IsMaximum { get; }
        void AddToStack(int amount = 1);
        void RemoveFromStack(int amount = 1);
    }
}
