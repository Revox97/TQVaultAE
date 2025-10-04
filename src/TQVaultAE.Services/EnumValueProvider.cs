using System.ComponentModel;
using System.Reflection;

namespace TQVaultAE.Services
{
    public class EnumValueProvider
    {
        public static string GetValue(Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());

            if (field is null)
                return value.ToString();

            DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}
