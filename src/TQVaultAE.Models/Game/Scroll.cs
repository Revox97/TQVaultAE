using System.Drawing;
using System.Windows.Media;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game
{
    public class Scroll : Item, IStackable
    {
        public int MaxStackSize { get; set; } = 1;

        public int StackSize { get; set; }

        public Scroll() : base()
        {
            _size = new Size(2, 2);
        }

        public ItemRequirements Requirements { get; set; }

        public override Size Size => _size;

        public override Brush Color => new SolidColorBrush(Colors.GreenYellow);

        public override Brush HoverColor => throw new NotImplementedException();

        public override bool HasVisualAccent => true;

        public void AddToStack()
        {
            if (StackSize + 1 > MaxStackSize)
                throw new InvalidOperationException("Increment exceeds maximum stack size.");

            StackSize++;
        }

        public void RemoveFromStack()
        {
            if (StackSize - 1 < 1)
                throw new InvalidOperationException("Decrement lowers stack to less then one.");

            StackSize--;
        }

        public override string ToString()
        {
            return $"{Name} ({StackSize}) ({GetItemVersionValue})";
        }
    }
}
