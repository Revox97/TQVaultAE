using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using TQVaultAE.Model;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Converters
{
    public class ItemTypeToAccentColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //if (value is not ItemClass itemClass)
            //    return value;

            return new SolidColorBrush(new Color(0x66, Model.Colors.TitanQuestPurple.R, Model.Colors.TitanQuestPurple.G, Model.Colors.TitanQuestPurple.B));

            // TODO Use classification for specific item types to get correct color

            //if (parameter is null || parameter is not ItemClass itemClassification)
            //    return value;

            //Color color = clasification switch
            //{
            //    ItemClasification.Legendary => Colors.TitanQuestPurple,
            //    ItemClasification.Epic => Colors.TitanQuestAqua,
            //    ItemClasification.Rare => Colors.TitanQuestYellowGreen,
            //    ItemClasification.Magical => Colors.TitanQuestYellow,
            //    ItemClasification.Common => Colors.TitanQuestSilver,
            //};
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
