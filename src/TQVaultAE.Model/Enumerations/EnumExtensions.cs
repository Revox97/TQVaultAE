using System.ComponentModel;
using System.Reflection;

namespace TQVaultAE.Model.Enumerations
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an enum value by its DescriptionAttribute value, or by its enum name.
        /// </summary>
        public static TEnum GetEnumValue<TEnum>(this string value) where TEnum : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            foreach (FieldInfo field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                string? description = field.GetCustomAttribute<DescriptionAttribute>()?.Description;

                if (string.Equals(description, value, StringComparison.OrdinalIgnoreCase) || string.Equals(field.Name, value, StringComparison.OrdinalIgnoreCase))
                    return (TEnum)field.GetValue(null)!;
            }

            throw new ArgumentException($"'{value}' is not a valid name or description for enum {typeof(TEnum).Name}.", nameof(value));
        }

        /// <summary>
        /// Gets the DescriptionAttribute value for an enum value, or the enum name
        /// if no matching description exists.
        /// </summary>
        public static string GetEnumStringValue<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            FieldInfo? field = typeof(TEnum).GetField(value.ToString());

            if (field == null)
                return value.ToString();

            string? description = field.GetCustomAttribute<DescriptionAttribute>()?.Description;
            return description ?? field.Name;
        }
    }
}
