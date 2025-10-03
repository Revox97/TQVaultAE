using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    internal class CharmStackingComponent : IStackingComponent
    {
        private readonly CharmType _charmType;

        public CharmStackingComponent(CharmType charmType, int initialStackSize)
        {
            _charmType = charmType;
            MaxStackSize = _charmType == CharmType.Charm ? 5 : 3;

            if (initialStackSize < 1 || initialStackSize > MaxStackSize)
                throw new ArgumentException(""); // TODO define exception text

            StackSize = initialStackSize;
        }

        public int MaxStackSize { get; }

        public int StackSize { get; private set; }

        public bool IsMaximum => StackSize == MaxStackSize;

        public void AddToStack()
        {
            if (StackSize + 1 > MaxStackSize)
                throw new InvalidOperationException("Stack size exceeds maximum limit");

            StackSize++;
        }

        public void RemoveFromStack()
        {
            if (StackSize - 1 < 0)
                throw new InvalidOperationException("Stack size would be less then one.");

            StackSize--;
        }
    }
}
