using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace TQVaultAE.Converters
{
    public class ItemTypeToAccentColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new SolidColorBrush(new Color(0x66, Model.UI.Colors.TitanQuestPurple.R, Model.UI.Colors.TitanQuestPurple.G, Model.UI.Colors.TitanQuestPurple.B));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
