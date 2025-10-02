using System.Drawing;
using System.Windows.Media;
using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    public class Artifact : EquipableItem
    {
        public ArtifactType ArtifactType { get; set; }

        public Artifact() : base()
        {
            _size = new Size(2, 2);
        }

        public override Size Size => _size;

        public override Brush Color => new SolidColorBrush(Colors.LightBlue);

        public override Brush HoverColor => throw new NotImplementedException();

        public override bool HasVisualAccent => true;
    }
}
