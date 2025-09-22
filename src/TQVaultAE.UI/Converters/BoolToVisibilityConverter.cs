using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TQVaultAE.UI.Converters
{
    internal class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool input
                ? (object)(input ? Visibility.Visible : Visibility.Collapsed)
                : throw new ArgumentException($"{value} must be of type {typeof(bool)}.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility input
                ? (object)(input == Visibility.Visible)
                : throw new ArgumentException($"{value} must be of type {typeof(Visibility)}.");
        }
    }
}
