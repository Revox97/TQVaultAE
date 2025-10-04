using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace TQVaultAE.Models
{
    /// <summary>
    /// Titan Quest pre defined colors
    /// </summary>
    public enum TitanQuestColor
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
        GreenYellow,

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

    public static partial class TitanQuestColorHelper
    {
        /// <summary>
        /// Regex Match color tag 4 chars & 2 chars
        /// </summary>
        public const string RegExTQTag = @"(?<ColorTag>\{\^(?<ColorId>\w)}|\^(?<ColorId>\w))";

        public static readonly Regex RegExTQTagInstance = TitanQuestTagInstanceRegex();

        /// <summary>
        /// Regex Match starting color tag 4 chars & 2 chars or empty
        /// </summary>
        public const string RegExStartingColorTagOrEmpty = @"^" + RegExTQTag + @"?";
        public static readonly Regex RegExStartingColorTagOrEmptyInstance = StartingColorTagOrEmptyInstanceRegex();

        private record ColorMapItem(TitanQuestColor ColorEnum, char ColorChar, Color ColorSys);

        private static readonly ReadOnlyCollection<ColorMapItem> s_colorMap = new List<ColorMapItem> {
                new (TitanQuestColor.Aqua, 'A', System.Windows.Media.Color.FromRgb(0, 255, 255)),
                new (TitanQuestColor.Blue, 'B', System.Windows.Media.Color.FromRgb(0, 163, 255)),
                new (TitanQuestColor.LightCyan, 'C', System.Windows.Media.Color.FromRgb(224, 255, 255)),
                new (TitanQuestColor.DarkGray, 'D', System.Windows.Media.Color.FromRgb(153, 153, 153)),
                new (TitanQuestColor.Fuschia, 'F', System.Windows.Media.Color.FromRgb(255, 0, 255)),
                new (TitanQuestColor.Green, 'G', System.Windows.Media.Color.FromRgb(64, 255, 64)),
                new (TitanQuestColor.Indigo, 'I', System.Windows.Media.Color.FromRgb(75, 0, 130)),
                new (TitanQuestColor.Khaki, 'K', System.Windows.Media.Color.FromRgb( 195, 176, 145)),
                new (TitanQuestColor.GreenYellow, 'L', System.Windows.Media.Color.FromRgb(145, 203, 0)),
                new (TitanQuestColor.Maroon, 'M', System.Windows.Media.Color.FromRgb(128, 0, 0)), // HoverRequirementsNotMet
                new (TitanQuestColor.Orange, 'O', System.Windows.Media.Color.FromRgb(255, 173, 0)),
                new (TitanQuestColor.Purple, 'P', System.Windows.Media.Color.FromRgb(217, 5, 255)),
                new (TitanQuestColor.Red, 'R', System.Windows.Media.Color.FromRgb(255, 0, 0)),
                new (TitanQuestColor.Silver, 'S', System.Windows.Media.Color.FromRgb(224, 224, 224)),
                new (TitanQuestColor.Turquoise, 'T', System.Windows.Media.Color.FromRgb(0, 255, 209)),
                new (TitanQuestColor.Yellow, 'Y', System.Windows.Media.Color.FromRgb(255, 245, 43)),
                new (TitanQuestColor.White, 'W', System.Windows.Media.Color.FromRgb(255, 255, 255))
            }.AsReadOnly();

        /// <summary>
        /// Return color from color tag identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static TitanQuestColor GetColorFromTagIdentifier(char identifier)
        {
            IEnumerable<TitanQuestColor> map = s_colorMap.Where(c => c.ColorChar == identifier).Select(c => c.ColorEnum);
            return map.Any() ? map.First() : TitanQuestColor.White;
        }

        /// <summary>
        /// Return color tag identifier from color 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static char TagIdentifier(this TitanQuestColor color)
        {
            IEnumerable<char> map = s_colorMap.Where(c => c.ColorEnum == color).Select(c => c.ColorChar);
            return map.Any() ? map.First() : 'W';
        }

        private static readonly Regex s_getColorFromTaggedStringRegEx = ColorFromTaggedStringRegex();

        /// <summary>
        /// Return the TQColor corresponding to color tag prefix
        /// </summary>
        /// <param name="text"></param>
        /// <returns>null if no color prefix</returns>
        public static TitanQuestColor? GetColorFromTaggedString(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            TitanQuestColor? res = null;
            string colorId = s_getColorFromTaggedStringRegEx.Replace(text, @"${ColorId}").ToUpperInvariant();

            if (colorId.Length != 0)
                res = GetColorFromTagIdentifier(colorId.First());

            return res;
        }

        /// <summary>
        /// Get color tag from <see cref="TitanQuestColor"/>.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="fourCharFormat"></param>
        /// <returns></returns>
        public static string ColorTag(this TitanQuestColor color, bool fourCharFormat = true)
            => fourCharFormat ? $"{{^{color.TagIdentifier()}}}" : $"^{color.TagIdentifier()}";

        /// <summary>
        /// Remove leading color tag from <paramref name="tqText"/>
        /// </summary>
        /// <param name="tqText"></param>
        /// <returns></returns>
        public static string RemoveLeadingColorTag(this string tqText)
        {
            return string.IsNullOrWhiteSpace(tqText)
                ? tqText ?? string.Empty
                : RegExStartingColorTagOrEmptyInstance.Replace(tqText, string.Empty);
        }

        /// <summary>
        /// Gets the Color for a particular TQ defined color
        /// </summary>
        /// <param name="color">TQ color enumeration</param>
        /// <returns>System.Drawing.Color for the particular TQ color</returns>
        public static Color Color(this TitanQuestColor color)
        {
            IEnumerable<Color> map = s_colorMap.Where(c => c.ColorEnum == color).Select(c => c.ColorSys);
            return map.Any() ? map.First() : System.Windows.Media.Color.FromRgb(255, 255, 255);
        }

        [GeneratedRegex(RegExTQTag, RegexOptions.Compiled)]
        internal static partial Regex TitanQuestTagInstanceRegex();

        [GeneratedRegex(RegExStartingColorTagOrEmpty, RegexOptions.Compiled)]
        internal static partial Regex StartingColorTagOrEmptyInstanceRegex();

        [GeneratedRegex(@"^(?<ColorTag>\{\^(?<ColorId>\w)}|\^(?<ColorId>\w))?.*", RegexOptions.Compiled)]
        internal static partial Regex ColorFromTaggedStringRegex();
    }
}
