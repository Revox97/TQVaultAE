using System.Drawing;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game.Components
{
    internal class FixedSizeComponent : ISizeComponent
    {
        private readonly Size _size;

        public FixedSizeComponent(Size size) => _size = size;

        public FixedSizeComponent(int width, int height) => _size = new Size(width, height);

        public Size GetSize() => _size;
    }
}
