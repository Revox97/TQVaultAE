using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TQVaultAE.Models.Game;

namespace TQVaultAE.UI.Converters
{
    internal class ItemRequirementToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemRequirements requirements && requirements != default)
            {
                if (parameter is string requirementName)
                {
                    if (requirementName.Equals("LEVEL", StringComparison.OrdinalIgnoreCase))
                        return requirements.Level > 0 ? Visibility.Visible : Visibility.Collapsed;

                    if (requirementName.Equals("STRENGTH", StringComparison.OrdinalIgnoreCase))
                        return requirements.Strength > 0 ? Visibility.Visible : Visibility.Collapsed;

                    if (requirementName.Equals("DEXTERITY", StringComparison.OrdinalIgnoreCase))
                        return requirements.Dexterity > 0 ? Visibility.Visible : Visibility.Collapsed;

                    if (requirementName.Equals("INTELLIGENCE", StringComparison.OrdinalIgnoreCase))
                        return requirements.Intelligence > 0 ? Visibility.Visible : Visibility.Collapsed;
                }

                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
