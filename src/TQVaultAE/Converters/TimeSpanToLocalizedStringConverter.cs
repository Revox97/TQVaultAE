using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TQVaultAE.Converters
{
    internal class TimeSpanToLocalizedStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not TimeSpan timespan)
                return value; // TODO Log warning

            return timespan.ToString(@"d\.hh\:mm\:ss"); // TODO Localize
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
