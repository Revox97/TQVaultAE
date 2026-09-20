namespace TQVaultAE.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class LocalizationTagAttribute(string localizationTag) : Attribute
    {
        public readonly string LocalizationTag = localizationTag;
    }
}
