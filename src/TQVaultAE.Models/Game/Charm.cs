using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.Game.Enumerations;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game
{
    public class Charm : Item, IStackable
    {
        public int MaxStackSize => CharmType == CharmType.Charm ? 5 : 3;

        public bool IsCompleted => (CharmType == CharmType.Charm && StackSize == 5) || (CharmType == CharmType.Relic && StackSize == 3);

        public BitmapImage IconComplete { get; set; }

        public int StackSize { get; set; }

        public CharmType CharmType { get; set; }

        public GameDifficulty Difficulty { get; set; }

        public ItemRequirements ItemRequirements { get; set; }

        public List<ItemAttribute> Attributes { get; set; } = [];

        public List<ItemAttribute> CompletionBonusAttributes { get; set; } = [];

        public Charm() : base()
        {
            _size = new Size(1, 1);
        }

        public override Size Size => _size;

        public override Brush Color => new SolidColorBrush(Colors.Orange);

        public override Brush HoverColor => throw new NotImplementedException();

        public override bool HasVisualAccent => false;

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

        public override string ToString()
        {
            return $"{Name} - {Description} {GetItemVersionValue})";
        }
    }
}
