using System.Windows.Media;

namespace TQVaultAE.Models.Game.Interfaces
{
    public interface IItemColorComponenent
    {
        public Brush Color { get; }

        public Brush HoverColor { get; }

        public bool HasVisualAccent { get; }
    }
}
