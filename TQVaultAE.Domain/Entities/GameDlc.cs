//using EnumsNET;
namespace TQVaultAE.Domain.Entities;

/// <summary>
/// Game DLC enumeration
/// </summary>
public enum GameDlc
{
	[GameDlcDescription("TQ", "tagBackground01")]
	TitanQuest,
	[GameDlcDescription("IT", "tagBackground02")]
	ImmortalThrone,
	[GameDlcDescription("RAG", "tagBackground03")]
	Ragnarok,
	[GameDlcDescription("ATL", "tagBackground04")]
	Atlantis,
	[GameDlcDescription("EEM", "x4tagBackground05")]
	EternalEmbers
}

// TODO Move into own file
public static class GameDlcExtension
{
	public static string GetCode(this GameDlc ext)
	{
		// TODO get custom attribute and return
		//Type type = typeof(GameDlc);
		//return type.CustomAttributes.FirstOrDefault(a => a.NamedArguments.Contains(new System.Reflection.CustomAttributeNamedArgument("")))

		return string.Empty; // Temp value

		//return Enums.GetAttributes(ext).Get<GameDlcDescriptionAttribute>().Code;
	}

	public static string GetTranslationTag(this GameDlc ext)
	{
		// TODO get custom attribute and return
		//Type type = typeof(GameDlc);
		//return type.CustomAttributes.FirstOrDefault(a => a.NamedArguments.Contains(new System.Reflection.CustomAttributeNamedArgument("")))

		return string.Empty; // Temp value

		//return Enums.GetAttributes(ext).Get<GameDlcDescriptionAttribute>().TranslationTag;
	}

	public static string GetSuffix(this GameDlc ext)
	{
		return ext switch
		{
			GameDlc.TitanQuest => string.Empty,
			_ => $"({GetCode(ext)})"
		};
	}
}

public class GameDlcDescriptionAttribute(string gameExtensionCode, string translationTag) : Attribute
{
	public readonly string Code = gameExtensionCode;
	public readonly string TranslationTag = translationTag;
}
