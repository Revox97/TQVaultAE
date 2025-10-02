using System.Drawing;
using System.Windows.Media;

namespace TQVaultAE.Models.Game
{
    public class Formula : Item
    {
        public int Cost { get; set; }

        public Artifact CreatesArtifact { get; set; }

        public List<Item> RequiredComponents { get; set; } = [];

        public Formula() : base()
        {
            _size = new Size(2, 1);
        }

        public override Size Size => _size;

        public override Brush Color => new SolidColorBrush(Colors.White);

        public override Brush HoverColor => throw new NotImplementedException();

        public override bool HasVisualAccent => true;
    }
}
