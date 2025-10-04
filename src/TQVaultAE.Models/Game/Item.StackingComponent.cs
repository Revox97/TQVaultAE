using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game
{
    public partial class Item
    {
        private readonly IStackingComponent? _stackingComponent = null!;

        public bool CanStack => _stackingComponent is not null;

        public int StackSize
        {
            get
            {
                return CanStack
                    ? _stackingComponent!.StackSize
                    : 0;
            }
        }

        public void AddToStack(int amount = 1)
        {
            if (!CanStack)
                throw new InvalidOperationException("Item is not stackable.");

            _stackingComponent!.AddToStack(amount);
        }

        public void RemoveFromStack(int amount = 1)
        {
            if (!CanStack)
                throw new InvalidOperationException("Item is not stackable.");

            _stackingComponent!.RemoveFromStack(amount);
        }

        public int MaxStackSize
        {
            get
            {
                return CanStack
                    ? _stackingComponent!.MaxStackSize
                    : throw new InvalidOperationException("Item is not stackable.");
            }
        }

        public bool IsMaximum
        {
            get
            {
                return CanStack
                    ? _stackingComponent!.IsMaximum
                    : throw new InvalidOperationException("Item is not stackable.");
            }
        }
    }
}
