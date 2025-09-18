using System.Collections.ObjectModel;
using System.Drawing;
using System.Text.RegularExpressions;

namespace TQVaultAE.Domain.Entities;

/// <summary>
/// Titan Quest pre defined colors
/// </summary>
public enum TQColor
{
	/// <summary>
	/// Titan Quest Aqua color
	/// </summary>
	Aqua,

	/// <summary>
	/// Titan Quest Blue color
	/// </summary>
	Blue,

	/// <summary>
	/// Titan Quest Light Cyan color
	/// </summary>
	LightCyan,

	/// <summary>
	/// Titan Quest Dark Gray color
	/// </summary>
	DarkGray,

	/// <summary>
	/// Titan Quest Fuschia color
	/// </summary>
	Fuschia,

	/// <summary>
	/// Titan Quest Green color
	/// </summary>
	Green,

	/// <summary>
	/// Titan Quest Indigo color
	/// </summary>
	Indigo,

	/// <summary>
	/// Titan Quest Khaki color
	/// </summary>
	Khaki,

	/// <summary>
	/// Titan Quest Yellow Green color
	/// </summary>
	YellowGreen,

	/// <summary>
	/// Titan Quest Maroon color
	/// </summary>
	Maroon,

	/// <summary>
	/// Titan Quest Orange color
	/// </summary>
	Orange,

	/// <summary>
	/// Titan Quest Purple color
	/// </summary>
	Purple,

	/// <summary>
	/// Titan Quest Red color
	/// </summary>
	Red,

	/// <summary>
	/// Titan Quest Silver color
	/// </summary>
	Silver,

	/// <summary>
	/// Titan Quest Turquoise color
	/// </summary>
	Turquoise,

	/// <summary>
	/// Titan Quest White color
	/// </summary>
	White,

	/// <summary>
	/// Titan Quest Yellow color
	/// </summary>
	Yellow
}


// TODO Yes the first helper, this has to be great :D
public static partial class TQColorHelper
{
	/// <summary>
	/// Regex Match color tag 4 chars & 2 chars
	/// </summary>
	public const string RegExTQTag = @"(?<ColorTag>\{\^(?<ColorId>\w)}|\^(?<ColorId>\w))";
	public static readonly Regex RegExTQTagInstance = TQTagRegex();

	/// <summary>
	/// Regex Match starting color tag 4 chars & 2 chars or empty
	/// </summary>
	public const string RegExStartingColorTagOrEmpty = @"^" + RegExTQTag + @"?";
	public static readonly Regex RegExStartingColorTagOrEmptyInstance = StartingColorTagOrEmptyInstanceRegex();

	private record ColorMapItem(TQColor ColorEnum, char ColorChar, Color ColorSys);

	private static readonly ReadOnlyCollection<ColorMapItem> s_colorMap = new([
		new(TQColor.Aqua, 'A', System.Drawing.Color.FromArgb(0, 255, 255))
		, new(TQColor.Blue, 'B', System.Drawing.Color.FromArgb(0, 163, 255))
		, new(TQColor.LightCyan, 'C', System.Drawing.Color.FromArgb(224, 255, 255))
		, new(TQColor.DarkGray, 'D', System.Drawing.Color.FromArgb(153, 153, 153))
		, new(TQColor.Fuschia, 'F', System.Drawing.Color.FromArgb(255, 0, 255))
		, new(TQColor.Green, 'G', System.Drawing.Color.FromArgb(64, 255, 64))
		, new(TQColor.Indigo, 'I', System.Drawing.Color.FromArgb(75, 0, 130))
		, new(TQColor.Khaki, 'K', System.Drawing.Color.FromArgb(195, 176, 145))
		, new(TQColor.YellowGreen, 'L', System.Drawing.Color.FromArgb(145, 203, 0))
		, new(TQColor.Maroon, 'M', System.Drawing.Color.FromArgb(128, 0, 0))
		, new(TQColor.Orange, 'O', System.Drawing.Color.FromArgb(255, 173, 0))
		, new(TQColor.Purple, 'P', System.Drawing.Color.FromArgb(217, 5, 255))
		, new(TQColor.Red, 'R', System.Drawing.Color.FromArgb(255, 0, 0))
		, new(TQColor.Silver, 'S', System.Drawing.Color.FromArgb(224, 224, 224))
		, new(TQColor.Turquoise, 'T', System.Drawing.Color.FromArgb(0, 255, 209))
		, new(TQColor.Yellow, 'Y', System.Drawing.Color.FromArgb(255, 245, 43))
		, new(TQColor.White, 'W', System.Drawing.Color.White)
	]);

	/// <summary>
	/// Return color from color tag identifier
	/// </summary>
	/// <param name="identifier"></param>
	/// <returns></returns>
	public static TQColor GetColorFromTagIdentifier(char identifier)
	{
		IEnumerable<TQColor>? map = s_colorMap.Where(c => c.ColorChar == identifier).Select(c => c.ColorEnum);
		return map.Any() ? map.First() : TQColor.White;
	}

	/// <summary>
	/// Return color tag identifier from color 
	/// </summary>
	/// <param name="color"></param>
	/// <returns></returns>
	public static char TagIdentifier(this TQColor color)
	{
		IEnumerable<char>? map = s_colorMap.Where(c => c.ColorEnum == color).Select(c => c.ColorChar);
		return map.Any() ? map.First() : 'W';
	}

	private static readonly Regex s_getColorFromTaggedStringRegEx = StartingColorTagOrEmptyRegex();

	/// <summary>
	/// Return the TQColor corresponding to color tag prefix
	/// </summary>
	/// <param name="text"></param>
	/// <returns>null if no color prefix</returns>
	public static TQColor? GetColorFromTaggedString(this string text)
	{
		if (string.IsNullOrWhiteSpace(text)) 
			return null;

		TQColor? res = null;

		string colorId = s_getColorFromTaggedStringRegEx.Replace(text, @"${ColorId}").ToUpperInvariant();

		if (colorId.Length != 0)
			res = GetColorFromTagIdentifier(colorId.First());

		return res;
	}

	/// <summary>
	/// Get color tag from <see cref="TQColor"/>.
	/// </summary>
	/// <param name="color"></param>
	/// <param name="fourCharFormat"></param>
	/// <returns></returns>
	public static string ColorTag(this TQColor color, bool fourCharFormat = true)
		=> fourCharFormat ? $"{{^{color.TagIdentifier()}}}" : $"^{color.TagIdentifier()}";


	/// <summary>
	/// Remove leading color tag from <paramref name="TQText"/>
	/// </summary>
	/// <param name="TQText"></param>
	/// <returns></returns>
	public static string RemoveLeadingColorTag(this string TQText)
	{
		return !string.IsNullOrWhiteSpace(TQText)
			? RegExStartingColorTagOrEmptyInstance.Replace(TQText, string.Empty)
			: string.Empty;
	}


	/// <summary>
	/// Gets the Color for a particular TQ defined color
	/// </summary>
	/// <param name="color">TQ color enumeration</param>
	/// <returns>System.Drawing.Color for the particular TQ color</returns>
	public static Color Color(this TQColor color)
	{
		IEnumerable<Color> map = s_colorMap.Where(c => c.ColorEnum == color).Select(c => c.ColorSys);
		return map.Any() ? map.First() : System.Drawing.Color.White;
	}

	[GeneratedRegex(RegExTQTag, RegexOptions.Compiled)]
	internal static partial Regex TQTagRegex();

	[GeneratedRegex(RegExStartingColorTagOrEmpty, RegexOptions.Compiled)]
	internal static partial Regex StartingColorTagOrEmptyInstanceRegex();

	[GeneratedRegex(@"^(?<ColorTag>\{\^(?<ColorId>\w)}|\^(?<ColorId>\w))?.*", RegexOptions.Compiled)]
	internal static partial Regex StartingColorTagOrEmptyRegex();
}
