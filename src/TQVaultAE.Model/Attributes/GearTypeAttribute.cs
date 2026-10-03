using TQVaultAE.Model.Enumerations;

// TODO: ADD Tests if it will be used, otherwise remove
namespace TQVaultAE.Model.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class GearTypeAttribute(GearType type) : Attribute
    {
        public GearType Type { get; } = type;
    }
}
