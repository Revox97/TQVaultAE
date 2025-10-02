using System.Windows.Media;

namespace TQVaultAE.Models.Game
{
    public class QuestItem : Item
    {
        public override Brush Color => new SolidColorBrush(Colors.Purple);

        public override Brush HoverColor => throw new NotImplementedException();

        public override bool HasVisualAccent => true;

        public override string ToString()
        {
            return $"{Name} (Quest Item) ({GetItemVersionValue})";
        }
    }
}
