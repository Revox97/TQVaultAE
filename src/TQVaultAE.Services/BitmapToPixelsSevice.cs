using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TQVaultAE.Models.Services;

namespace TQVaultAE.Services
{
    public class BitmapToPixelsSevice : IBitmapToPixelsService
    {
        public byte[] GetRenderedPixels(Image imageControl, out int stride, out int pixelWidth, out int pixelHeight)
        {
            if (imageControl.Source is not BitmapSource source)
                throw new InvalidOperationException("Image source is not a BitmapSource");

            // Ensure layout is finalized
            imageControl.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            imageControl.Arrange(new Rect(0, 0, imageControl.ActualWidth, imageControl.ActualHeight));
            imageControl.UpdateLayout();

            // Control's rendered size
            double targetWidth = imageControl.ActualWidth;
            double targetHeight = imageControl.ActualHeight;

            if (targetWidth <= 0 || targetHeight <= 0)
                throw new InvalidOperationException("Invalid control size");

            // Source image pixel size
            double sourceWidth = source.PixelWidth;
            double sourceHeight = source.PixelHeight;

            // Determine how WPF stretches the image
            Rect destRect = CalculateImageStretchRect(imageControl.Stretch, sourceWidth, sourceHeight, targetWidth, targetHeight);

            // Use DPI-aware rendering (96 = standard WPF DPI)
            int rtbWidth = (int)Math.Ceiling(targetWidth);
            int rtbHeight = (int)Math.Ceiling(targetHeight);

            if (rtbWidth == 0 || rtbHeight == 0)
                throw new InvalidOperationException("RenderTargetBitmap dimensions are zero");

            RenderTargetBitmap rtb = new(rtbWidth, rtbHeight, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual dv = new();
            using (DrawingContext dc = dv.RenderOpen())
            {
                // Optional: draw transparent background
                dc.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, rtbWidth, rtbHeight));

                // Draw the bitmap source using stretch rect
                dc.DrawImage(source, destRect);
            }

            rtb.Render(dv);

            stride = (rtbWidth * rtb.Format.BitsPerPixel + 7) / 8;
            pixelWidth = rtb.PixelWidth;
            pixelHeight = rtb.PixelHeight;

            byte[] pixels = new byte[stride * rtbHeight];
            rtb.CopyPixels(pixels, stride, 0);

            return pixels;
        }

        //internal static byte[] GetRenderedPixels(Image imageControl, out int stride, out int pixelWidth, out int pixelHeight)
        //{
        //    if (imageControl.Source is not BitmapSource source)
        //        throw new InvalidOperationException("Image source is not a BitmapSource");

        //    double targetWidth = imageControl.ActualWidth;
        //    double targetHeight = imageControl.ActualHeight;

        //    if (targetWidth <= 0 || targetHeight <= 0)
        //        throw new InvalidOperationException("Invalid control size");

        //    // Source image pixel size
        //    double sourceWidth = source.PixelWidth;
        //    double sourceHeight = source.PixelHeight;

        //    // Determine how WPF stretches the image
        //    Rect destRect = CalculateImageStretchRect(imageControl.Stretch, sourceWidth, sourceHeight, targetWidth, targetHeight);

        //    int rtbWidth = (int)Math.Ceiling(targetWidth);
        //    int rtbHeight = (int)Math.Ceiling(targetHeight);

        //    // TODO make sure this is dpi aware
        //    RenderTargetBitmap rtb = new(rtbWidth, rtbHeight, 96, 96, PixelFormats.Pbgra32);

        //    DrawingVisual dv = new();
        //    using DrawingContext dc = dv.RenderOpen();
            
        //    // Fill transparent background (optional)
        //    dc.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, rtbWidth, rtbHeight));

        //    // Draw the image into the computed rect
        //    dc.DrawImage(source, destRect);

        //    rtb.Render(dv);

        //    stride = rtbWidth * (rtb.Format.BitsPerPixel / 8);
        //    pixelWidth = rtb.PixelWidth;
        //    pixelHeight = rtb.PixelHeight;

        //    byte[] pixels = new byte[stride * rtbHeight];
        //    rtb.CopyPixels(pixels, stride, 0);
        //    return pixels;
        //}

        private static Rect CalculateImageStretchRect(Stretch stretch, double sourceWidth, double sourceHeight, double targetWidth, double targetHeight)
        {
            switch (stretch)
            {
                case Stretch.None:
                    return new Rect(0, 0, Math.Min(sourceWidth, targetWidth), Math.Min(sourceHeight, targetHeight));

                case Stretch.Fill:
                    return new Rect(0, 0, targetWidth, targetHeight);

                case Stretch.Uniform:
                {
                    double ratio = Math.Min(targetWidth / sourceWidth, targetHeight / sourceHeight);
                    double width = sourceWidth * ratio;
                    double height = sourceHeight * ratio;
                    double x = (targetWidth - width) / 2;
                    double y = (targetHeight - height) / 2;
                    return new Rect(x, y, width, height);
                }

                case Stretch.UniformToFill:
                {
                    double ratio = Math.Max(targetWidth / sourceWidth, targetHeight / sourceHeight);
                    double width = sourceWidth * ratio;
                    double height = sourceHeight * ratio;
                    double x = (targetWidth - width) / 2;
                    double y = (targetHeight - height) / 2;
                    return new Rect(x, y, width, height);
                }

                default:
                    return new Rect(0, 0, targetWidth, targetHeight);
            }
        }
    }
}
