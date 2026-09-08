namespace TQVaultAE.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class GearTypeDescriptionAttribute(string @class, string requirementEquationPrefix) : Attribute
    {
        /// <summary>
        /// Class constant for this <see cref="GearType"/> like <see cref="Item.ICLASS_AMULET"/>
        /// </summary>
        public readonly string Class = @class;

        /// <summary>
        /// Requirement equation prefix for use in the requirements equation see <see cref="IItemProvider.GetRequirementEquationPrefix"/>.
        /// </summary>
        public readonly string RequirementEquationPrefix = requirementEquationPrefix;
    }
}
