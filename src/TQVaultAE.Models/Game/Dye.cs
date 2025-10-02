using System.Drawing;
using System.Windows.Media;

namespace TQVaultAE.Models.Game
{
    public class Dye : Item
    {
        public Dye()
        {
            _size = new Size(1, 1);
        }

        public override Size Size => _size;

        public override Brush Color => new SolidColorBrush(Colors.White);

        public override Brush HoverColor => throw new NotImplementedException();

        public override bool HasVisualAccent => false;
    }
}
