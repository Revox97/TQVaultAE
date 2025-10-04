using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TQVaultAE.UI.Converters
{
    internal class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool input)
                throw new ArgumentException($"{value} must be of type {typeof(bool)}.");

            Visibility invisible = parameter is string invisibleValue && invisibleValue.Equals("COLLAPSED", StringComparison.OrdinalIgnoreCase)
                ? Visibility.Collapsed
                : Visibility.Hidden;

            return input ? Visibility.Visible : invisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility input
                ? (object)(input == Visibility.Visible)
                : throw new ArgumentException($"{value} must be of type {typeof(Visibility)}.");
        }
    }
}
