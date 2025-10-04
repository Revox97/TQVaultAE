using System.Drawing;
using TQVaultAE.Models.Game.Interfaces;

namespace TQVaultAE.Models.Game
{
    public partial class Item
    {
        private readonly ISizeComponent _sizeComponent;

        /// <summary>
        /// The size in cells in an <see langword="ItemsPanel"/>.
        /// </summary>
        public Size Size => _sizeComponent.GetSize();
    }
}
