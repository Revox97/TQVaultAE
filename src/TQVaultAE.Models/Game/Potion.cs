using System.Drawing;
using System.Windows.Media;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game
{
    public class Potion : Item, IStackable
    {
        // TODO Add EE potion support
        public int MaxStackSize { get; set; }

        public int StackSize { get; set; }

        public Potion() : base()
        {
            _size = new Size(1, 1);
        }

        public override Size Size => _size;

        public override Brush Color => new SolidColorBrush(Colors.White);

        public override Brush HoverColor => throw new NotImplementedException();

        public override bool HasVisualAccent => false;

        public void AddToStack()
        {
            if (StackSize + 1 > MaxStackSize)
                throw new InvalidOperationException("Stack size exceeds maximum size.");

            StackSize++;
        }

        public void RemoveFromStack()
        {
            if (StackSize - 1 < 1)
                throw new InvalidOperationException("Stack size is less then one.");

            StackSize--;
        }

        public override string ToString()
        {
            return $"{Name} ({StackSize}) ({GetItemVersionValue})";
        }
    }
}
