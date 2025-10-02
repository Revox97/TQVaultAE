using System.ComponentModel;
using System.Reflection;

// TODO Find good place for this one, its also in IO
namespace TQVaultAE.Models
{
    internal class EnumValueProvider
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
