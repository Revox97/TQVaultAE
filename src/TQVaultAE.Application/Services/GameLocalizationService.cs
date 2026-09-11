using TQVaultAE.TitanQuestDataProviders.Database;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.Application.Services
{
    public static class GameLocalizationService
    {
        private static ArcFile? s_localizationFile;
        private static readonly Dictionary<string, string> s_localization = [];

        // TODO Make dynamic. Harcoded for testing purposes.
        private static readonly string s_localizationPath = Path.Combine(@"C:\Program Files (x86)\Steam\steamapps\common\Titan Quest Anniversary Edition\Text\Text_EN.arc");

        public static async Task<string?> GetLocalizedValueByTag(string tag)
        {
            ArgumentException.ThrowIfNullOrEmpty(tag);

            if (s_localization.Count == 0)
                await InitializeAsync();

            return s_localization.TryGetValue(tag, out string? result) ? result : null;
        }

        private static async Task InitializeAsync()
        {
            s_localizationFile ??= await new ArcProvider().ReadAsync(s_localizationPath).ConfigureAwait(false);

            foreach (ArcFileRecord record in s_localizationFile.Records)
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
