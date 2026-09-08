namespace TQVaultAE.Model.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class GameDlcDescriptionAttribute(string gameExtensionCode, string translationTag) : Attribute
    {
        public readonly string Code = gameExtensionCode;
        public readonly string TranslationTag = translationTag;
    }
}
