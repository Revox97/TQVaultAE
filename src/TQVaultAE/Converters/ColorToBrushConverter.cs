using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace TQVaultAE.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is Color color
                ? new SolidColorBrush(color)
                : value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
