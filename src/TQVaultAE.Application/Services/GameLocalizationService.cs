using TQVaultAE.Application.Contracts;
using TQVaultAE.FileFormats.Arc;
using TQVaultAE.TitanQuestDataProviders.Database;

namespace TQVaultAE.Application.Services
{
    public sealed class GameLocalizationService : IGameLocalizationService
    {
        private static ArcFile? s_localizationFile;
        private static readonly Dictionary<string, string> s_localization = [];

        // Read only relevant data, other will be skipped
        private static readonly List<string> s_relevantLocalizationFiles =
        [
            "commonequipment.txt",
            "uniqueequipment.txt",
            "quest.txt",
            "xquest.txt",
            "xcommonequipment.txt",
            "xuniqueequipment.txt",
            "x2commonequipment.txt",
            "x2uniqueequipment.txt",
            "x2quest.txt"
        ];

        // TODO Make dynamic. Harcoded for testing purposes.
        private static readonly string s_localizationPath = Path.Combine(@"C:\Program Files (x86)\Steam\steamapps\common\Titan Quest Anniversary Edition\Text\Text_EN.arc");

        public async Task<string?> GetLocalizedValueByTag(string tag)
        {
            ArgumentException.ThrowIfNullOrEmpty(tag);
            return s_localization.TryGetValue(tag, out string? result) ? result : null;
        }

        public async Task InitializeAsync()
        {
            s_localizationFile ??= await new ArcProvider().ReadAsync(s_localizationPath).ConfigureAwait(false);

            foreach (ArcFileRecord record in s_localizationFile.Records.Where(x => s_relevantLocalizationFiles.Contains(x.FileName)))
            {
                if (record.ContentType is ArcRecordType.StringCollection)
                {
                    Dictionary<string, string> content = record.Content as Dictionary<string, string> ?? [];
                    foreach (KeyValuePair<string, string> tag in content)
                    {
                        if (s_localization.Keys.Any(x => x == tag.Key))
                            continue;

                        s_localization.Add(tag.Key, tag.Value);
                    }
                }
            }
        }
    }
}
