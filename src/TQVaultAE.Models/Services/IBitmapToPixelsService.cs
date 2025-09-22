using System.Windows.Controls;

namespace TQVaultAE.Models.Services
{
    public interface IBitmapToPixelsService
    {
        byte[] GetRenderedPixels(Image imageControl, out int stride, out int pixelWidth, out int pixelHeight);
    }
}
