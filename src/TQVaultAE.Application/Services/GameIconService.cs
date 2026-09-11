using System.Runtime.CompilerServices;
using TQVaultAE.TitanQuestDataProviders.Database;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.Application.Services
{
    public class GameIconService
    {
        private static readonly string s_resourcesPath = Path.Combine(@"C:\Program Files (x86)\Steam\steamapps\common\Titan Quest Anniversary Edition\Resources");

        private static readonly Dictionary<string, Dictionary<string, TexFile>> s_icons = [];

        // TODO Consider switching to bitmap
        public static async Task<TexFile> GetTexFileByTagAsync(string tag)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(tag);
                string[] path = tag.Split('\\');

                if (!s_icons.ContainsKey($"{path[0]}_{path[1]}"))
                    await InitializeAsync(path[0..2]);

                TexFile result = s_icons[$"{path[0]}_{path[1]}"][string.Join('/', path[2..])];
                return result;
            }
            catch(Exception ex)
            {
                // Reading tex file failed.
                return null!;
            }
        }

        private static async Task InitializeAsync(string[] fileParam)
        {
            string filePath = string.Empty;

            filePath = fileParam[0] == "Resources"
                ? Path.Combine(s_resourcesPath, $"{fileParam[1]}.arc")
                : Path.Combine(s_resourcesPath, fileParam[0], $"{fileParam[1]}.arc");

            ArcFile file = await new ArcProvider().ReadAsync(filePath).ConfigureAwait(false);

            Dictionary<string, TexFile> texFileMap = [];

            foreach (ArcFileRecord record in file.Records)
            {
                if (record.ContentType is ArcRecordType.TexFile)
                    texFileMap.Add(record.FileName, (TexFile)record.Content);
            }

            s_icons.Add($"{fileParam[0]}_{fileParam[1]}", texFileMap);
        }
    }
}
