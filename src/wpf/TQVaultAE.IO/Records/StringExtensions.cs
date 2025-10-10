using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TQVaultAE.IO.Records
{
    public static partial class StringExtensions
    {
        private const StringComparison NoCase = StringComparison.OrdinalIgnoreCase;

        public const string TQNewLineTag = @"{^N}";

        public static bool Contains(this string input, string search, StringComparison comparison)
            => input.IndexOf(search, comparison) > -1;

        public static bool ContainsIgnoreCase(this string input, string search)
            => input.Contains(search, NoCase);

        private static readonly DataTable s_cheapestDotNetEval = new();

        public static T Eval<T>(this string expression)
            => (T)Convert.ChangeType(s_cheapestDotNetEval.Compute(expression, null), typeof(T));

        /// <summary>
        /// Tells if <paramref name="search"/> is a regex input search
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static (bool IsRegex, string Pattern, Regex? Regex, bool RegexIsValid) IsTQVaultSearchRegEx(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return (false, string.Empty, null, false);

            bool isregex = search.First() == '/';

            if (isregex)
            {
                string pattern = search[1..];

                try
                {
                    Regex rgx = new(pattern, RegexOptions.IgnoreCase);
                    return (isregex, pattern, rgx, true);
                }
                catch (ArgumentException)
                {
                    return (true, pattern, null, false);
                }
            }

            return (false, search, null, false);
        }

        public static string MakeMD5(this string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new();

            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

        public static string ToFirstCharUpperCase(this string text)
        {
            return string.IsNullOrEmpty(text)
                ? text
                : string.Concat(char.ToUpperInvariant(text[0]), text[1..]);
        }

        /// <summary>
        /// Indicate if <paramref name="tqText"/> contain a ColorTag only
        /// </summary>
        /// <param name="tqText"></param>
        /// <returns></returns>
        public static bool IsColorTagOnly(this string tqText) => IsColorTagOnlyExtended(tqText).Value;

        private static readonly Regex s_isColorTagOnlyExtendedRegEx = IsColorTagOnlyExtendedRegex();
        /// <summary>
        /// Indicate if <paramref name="tqText"/> contain a ColorTag only
        /// </summary>
        /// <param name="tqText"></param>
        /// <returns></returns>
        public static (bool Value, int Length) IsColorTagOnlyExtended(this string tqText)
        {
            if (string.IsNullOrWhiteSpace(tqText))
                return (false, 0);

            return (s_isColorTagOnlyExtendedRegEx.IsMatch(tqText), tqText.Length);
        }


        public static string RemoveAllTQTags(this string tqText)
        {
            return string.IsNullOrWhiteSpace(tqText)
                ? tqText
                : TitanQuestColorHelper.RegExTQTagInstance.Replace(tqText, string.Empty);
        }

        private static readonly Regex s_tqCleanupRegEx = TqCleanupRegex();

        private const string TQCleanupReplaceResultPattern = @"${Legit}";

        /// <summary>
        /// Remove Leading ColorTag + Trailing comment
        /// </summary>
        /// <param name="tqText"></param>
        /// <returns></returns>
        public static string TQCleanup(this string tqText, bool keepLeadingColorTag = false)
        {
            if (tqText is null)
                return string.Empty;

            string txt = TitanQuestColorHelper.RemoveLeadingColorTag(tqText);
            string replaceTxt = TQCleanupReplaceResultPattern;

            if (keepLeadingColorTag)
            {
                TitanQuestColor? col = TitanQuestColorHelper.GetColorFromTaggedString(tqText);

                if (col.HasValue)
                    replaceTxt = col.Value.ColorTag() + TQCleanupReplaceResultPattern;
            }

            return s_tqCleanupRegEx.Replace(txt, replaceTxt).Trim();
        }

        /// <summary>
        /// Indicate if <paramref name="tqText"/> has a color tag prefix.
        /// </summary>
        /// <param name="tqText"></param>
        /// <returns></returns>
        public static bool HasColorPrefix(this string tqText)
        {
            if (tqText is null)
                return false;

            TitanQuestColor? t = TitanQuestColorHelper.GetColorFromTaggedString(tqText);
            return t.HasValue;
        }

        private static readonly char[] s_delimiter = [' '];
        private static readonly Regex s_prettyFileNameRegExNumber = PrettyFileNameNumberRegex();
        private static readonly Regex s_prettyFileNameRegExTitleCaseStart = PrettyFileNameTitleCaseStartRegex();

        // Orderered by word length
        // language=regex, IgnorePatternWhitespace
        private static readonly string s_prettyFileNameRegExLowerCaseStartPattern = @"
(?<Start>
	intelligence|protection|impairment|offensive|defensive|dexterity|elemental|
	mobility|cooldown|mastery|current|protect|defense|offense|reflect|
	damage|energy|pierce|guards|neidan|resist|health|poison|weapon|plants|hermes|sandal|
	armor|chance|[rR]unes|[dD]ream|bleed|total|bonus|woods|multi|relic|light|attac|speed|reduc|block|equip|
	clubs|sleep|metal|leech|regen|dodge|retal|
	cold|burn|life|fire|mana|stun|(?<!ext)rare|slow|wood|
	req|int|(?<!h)all(?!owed)|dmg|(?<!con)str(?!uction)|atk|att|spd|run|dex|(?<=pierce)ret|
	xp|(?<=chance)of|(?<!insec)to(?!rm)|(?<=(%|att))da|(?<=att)oa|
	[\-\+]?%|(?<=(offense|resists|%da))x(?!(tra|alted))|&|[\-\+]
)";

        private static readonly Regex s_prettyFileNameRegExLowerCaseStart = new(s_prettyFileNameRegExLowerCaseStartPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        /// <summary>
        /// Preprare <paramref name="tqPath"/> for display
        /// </summary>
        /// <param name="tqPath"></param>
        /// <returns></returns>
        public static string PrettyFileName(this string tqPath)
        {
            if (tqPath is null)
                return string.Empty;

            string filename = Path.GetFileNameWithoutExtension(tqPath).Replace('_', ' ');
            filename = s_prettyFileNameRegExNumber.Replace(filename, "(${number})"); // Enclose Numbers

            string[] filenameSplit1 = s_prettyFileNameRegExTitleCaseStart
                    .Replace(filename, ' ' + "${TitleCaseStart}") // Add space on Title Case
                    .Split(s_delimiter, StringSplitOptions.RemoveEmptyEntries); // Split on spaces

            IEnumerable<string> filenameSplit2 = filenameSplit1.SelectMany(w => s_prettyFileNameRegExLowerCaseStart
                .Replace(w, ' ' + "${Start}") // Add space on word begining for non TitleCase words
                .Split(s_delimiter, StringSplitOptions.RemoveEmptyEntries)); // Split on spaces

            filename = filenameSplit2.Select(w =>
            {
                string title = w.ToFirstCharUpperCase(); // TitleCase words
                return title switch // Substitute acronym
                {
                    "Dmg" => "Damage",
                    "Int" => "Intelligence",
                    "Str" => "Strength",
                    "Att" => "Attribute",
                    "Neg" => "Negative",
                    "Req" => "Requirement",
                    "Protect" => "Protection",
                    "Equip" => "Equipement",
                    "Atk" => "Attack",
                    "Xp" => "XP",
                    "Spd" => "Speed",
                    "BOW" => "Bow",
                    "Dex" => "Dexterity",
                    "Retal" or "Ret" => "Retaliation",
                    "DA" or "Da" => "Defensive Ability",
                    "OA" or "Oa" => "Offensive Ability",
                    var x when x.Equals("MasteryA", NoCase) => "Mastery Warfare",
                    var x when x.Equals("MasteryB", NoCase) => "Mastery Defense",
                    var x when x.Equals("MasteryC", NoCase) => "Mastery Hunting",
                    var x when x.Equals("MasteryD", NoCase) => "Mastery Rogue",
                    var x when x.Equals("MasteryE", NoCase) => "Mastery Earth",
                    var x when x.Equals("MasteryF", NoCase) => "Mastery Nature",
                    var x when x.Equals("MasteryG", NoCase) => "Mastery Spirit",
                    var x when x.Equals("MasteryH", NoCase) => "Mastery Storm",
                    _ => title
                };
            })
            .JoinString(" ");// Put it back together

            return filename;
        }

        /// <summary>
        /// Normalizes the record path to Upper Case Invariant Culture and replace slashes with backslashes.
        /// </summary>
        /// <param name="recordId">record path to be normalized</param>
        /// <returns>normalized record path</returns>
        public static string NormalizeRecordPath(this string recordId) => recordId.ToUpperInvariant().Replace('/', '\\');

        /// <summary>
        /// Make a <see cref="RecordId"/> from <paramref name="recordId"/>.
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static RecordId ToRecordId(this string recordId) => RecordId.Create(recordId);

        /// <summary>
        /// Explode <see cref="PrettyFileName"/> result 
        /// </summary>
        /// <param name="tqPath"></param>
        /// <returns></returns>
        public static (string PrettyFileName, string Effect, string Number, bool IsMatch) PrettyFileNameExploded(this string tqPath)
        {
            return tqPath.PrettyFileName().ExplodePrettyFileName();
        }

        // language=regex, IgnorePatternWhitespace
        private static readonly Regex s_explodePrettyFileName = ExplodePrettyFileNameRegex();

        /// <summary>
        /// Explode an already prettyfied file name
        /// </summary>
        /// <param name="prettyFileName"></param>
        /// <returns></returns>
        public static (string PrettyFileName, string Effect, string Number, bool IsMatch) ExplodePrettyFileName(this string prettyFileName)
        {
            Match m = s_explodePrettyFileName.Match(prettyFileName);

            return m.Success
                ? (prettyFileName, m.Groups["Effect"].Value.Trim(), m.Groups["Num"].Value, true)
                : (prettyFileName, prettyFileName, string.Empty, false);
        }

        private static readonly Regex s_allContiguousSpaceRegEx = AllContiguousSpaceRegex();
        private static readonly Regex s_insertAfterColorPrefixRegEx = InsertAfterColorPrefixRegex();

        /// <summary>
        /// Insert <paramref name="insertedText"/> between the color tag prefix and the text.
        /// </summary>
        /// <param name="tqText"></param>
        /// <param name="insertedText"></param>
        /// <returns></returns>
        public static string InsertAfterColorPrefix(this string tqText, string insertedText)
        {
            if (tqText is null)
                return insertedText;

            return string.IsNullOrEmpty(insertedText)
                ? tqText
                : s_insertAfterColorPrefixRegEx.Replace(tqText, string.Concat(@"${ColorTag}", insertedText, @"${Content}")).Trim();
        }

        public static IEnumerable<string> RemoveEmptyAndSanitize(this IEnumerable<string> tqText)
            => tqText.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t =>
            {
                // Split ColorTag & Content
                TitanQuestColor? tag = t.GetColorFromTaggedString();
                string text = t.RemoveLeadingColorTag()
                    .Replace("//", string.Empty).Trim()
                    .ToFirstCharUpperCase();

                return $"{(tag?.ColorTag())}{text}";
            });

        public static string JoinString(this IEnumerable<string> text, string delimiter) => string.Join(delimiter, text);

        public static string JoinWithoutStartingSpaces(this IEnumerable<string> tqText, string delimiter)
        {
            string[] tmp = [.. tqText];

        repass:
            List<string> res = [];

            for (int i = 0; i < tmp.Length; i++)
            {
                // Color Prefix alone
                bool isColorTagOnly = tmp[i].IsColorTagOnly();

                if (isColorTagOnly && (i + 1) == tmp.Length) // ColorTag is last element
                    continue;

                if (isColorTagOnly && (i + 1) < tmp.Length - 1)
                {
                    // Merge it with next element
                    res.Add(string.Concat(tmp[i], tmp[i + 1]));
                    i++;
                    continue;
                }

                res.Add(tmp[i]);
            }

            if (tmp.Length != res.Count)
            {
                tmp = [.. res];
                goto repass; // Recheck for multiple occurences
            }

            return string.Join(delimiter, [.. res]);
        }

        private static readonly Regex s_splitOnTQNewLineRegEx = SplitOnTqNewLineRegex();

        public static IEnumerable<string> SplitOnTQNewLine(this string tqText) => s_splitOnTQNewLineRegEx.Split(tqText);

        /// <summary>
        /// Wraps the words in a text description.
        /// </summary>
        /// <param name="tqText">Text to be word wrapped</param>
        /// <param name="columns">maximum number of columns before wrapping</param>
        /// <returns>List of wrapped text</returns>
        public static Collection<string> WrapWords(string tqText, int columns)
        {
            List<string> choppedLines = [ .. SplitOnTQNewLine(tqText) ];

            // split on columns args length
            choppedLines = [.. choppedLines.SelectMany(SplitOnColumns)];

            IEnumerable<string> SplitOnColumns(string t)
            {
                if (t.Length > columns)
                {
                    // split on spaces
                    string[] batch = s_allContiguousSpaceRegEx.Split(t);
                    string line = string.Empty;
                    string currentColor = string.Empty;
                    List<string> res = [];

                    foreach (string word in batch)
                    {
                        TitanQuestColor? foundColor = TitanQuestColorHelper.GetColorFromTaggedString(word);

                        if (line != string.Empty && !line.IsColorTagOnly())
                            line += ' ';

                        if (foundColor.HasValue)
                            currentColor = foundColor?.ColorTag() ?? string.Empty;

                        if (line.Length + word.Length > columns)
                        {
                            res.Add(line);
                            line = currentColor + string.Empty;
                        }

                        line += word;
                    }

                    res.Add(line);// remnant
                    return res;
                }
                
                return [t];
            }

            return new Collection<string>(choppedLines);
        }

        [GeneratedRegex(@"^(?<ColorTag>\{\^(?<ColorId>\w)}|\^(?<ColorId>\w))$", RegexOptions.Compiled)]
        internal static partial Regex IsColorTagOnlyExtendedRegex();

        [GeneratedRegex(@"(?<Legit>[^/]*)(?<Comment>//.*)", RegexOptions.Compiled)]
        internal static partial Regex TqCleanupRegex();

        [GeneratedRegex(@"(?<number>\d+)", RegexOptions.Compiled)]
        internal static partial Regex PrettyFileNameNumberRegex();

        [GeneratedRegex(@"(?<TitleCaseStart>BOW|DA|OA|XP|Mastery[A-Ha-h]|[A-Z][a-z]*)", RegexOptions.Compiled)]
        internal static partial Regex PrettyFileNameTitleCaseStartRegex();

        [GeneratedRegex(@"
[^\(]+\((?<Num1>\d+)\)(?<Effect>[^\(]+)\((?<Num>\d+)\) # match 'trash (0) effect (0)'
|
\((?<Num>\d+)\)(?<Effect>.+)  # match '(0) effect'
|
(?<Effect>[^\(]+)\((?<Num>\d+)\) # match 'effect (0)'
", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace)]
        internal static partial Regex ExplodePrettyFileNameRegex();

        [GeneratedRegex(@"\s+", RegexOptions.Compiled)]
        internal static partial Regex AllContiguousSpaceRegex();

        [GeneratedRegex(@"^(?<ColorTag>\{\^(?<ColorId>\w)}|\^(?<ColorId>\w))?(?<Content>.+)", RegexOptions.Compiled)]
        internal static partial Regex InsertAfterColorPrefixRegex();

        [GeneratedRegex(@"(?i)\{\^N}", RegexOptions.Compiled, "en-DE")]
        internal static partial Regex SplitOnTqNewLineRegex();
    }
}
