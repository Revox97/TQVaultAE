using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TQVaultAE.Converters
{
    // TODO check, whether it is easier to use avalonia bitmaps from the beginning
    internal class SystemToAvaloniaBitmapConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Bitmap systemBitmap)
                return value;

            try
            {
                BitmapData bitmapdata = systemBitmap.LockBits(new Rectangle(0, 0, systemBitmap.Width, systemBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                Avalonia.Media.Imaging.Bitmap avaloniaBitmap = new(Avalonia.Platform.PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul,
                    bitmapdata.Scan0,
                    new Avalonia.PixelSize(bitmapdata.Width, bitmapdata.Height),
                    new Avalonia.Vector(96, 96),
                    bitmapdata.Stride);

                systemBitmap.UnlockBits(bitmapdata);
                systemBitmap.Dispose();

                return avaloniaBitmap;
            }
            catch(Exception ex)
            {
                // Conversion failed
                // TODO add logging
                return value;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
