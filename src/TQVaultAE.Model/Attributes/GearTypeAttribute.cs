using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class GearTypeAttribute(GearType type) : Attribute
    {
        public GearType Type { get; } = type;
    }
}
