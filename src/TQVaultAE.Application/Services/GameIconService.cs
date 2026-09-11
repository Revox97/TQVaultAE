using System.Diagnostics;
using TQVaultAE.Application.Contracts;
using TQVaultAE.FileFormats.Arc;
using TQVaultAE.FileFormats.Tex;
using TQVaultAE.TitanQuestDataProviders.Database;

namespace TQVaultAE.Application.Services
{
    public class GameIconService : IGameIconService
    {
        private readonly static string _resourcesPath = Path.Combine(@"C:\Program Files (x86)\Steam\steamapps\common\Titan Quest Anniversary Edition\Resources");

        private static readonly Dictionary<string, Dictionary<string, TexFile>> s_icons = [];

        public async Task InitializeAsync(string path)
        {
            KeyValuePair<string, Dictionary<string, TexFile>> fileResult = await LoadIconsAsync(path);
            s_icons.Add(fileResult.Key, fileResult.Value);
        }

        private static async Task<KeyValuePair<string, Dictionary<string, TexFile>>> LoadIconsAsync(string path)
        {
            string[] fileParam = path.Split('\\');

            string filePath = IsInSubFolder(fileParam)
                ? Path.Combine(_resourcesPath, fileParam[0], $"{fileParam[1]}.arc")
                : Path.Combine(_resourcesPath, $"{fileParam[0]}.arc");

            ArcFile? file = null;

            try
            {
                file = await new ArcProvider().ReadAsync(filePath).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Reading tex file faile. {ex}.");
            }

            Dictionary<string, TexFile> texFileMap = [];

            if (file is not null)
            {
                foreach (ArcFileRecord record in file.Records)
                {
                    if (record.ContentType is ArcRecordType.TexFile)
                            texFileMap.Add(record.FileName, (TexFile)record.Content);
                }
            }

            bool isInSubfolder = IsInSubFolder(fileParam);
            string key = $"{(isInSubfolder ? fileParam[0] : "Base")}_{(isInSubfolder ? fileParam[1] : fileParam[0])}";
            return new KeyValuePair<string, Dictionary<string, TexFile>>(key, texFileMap);
        }

        // TODO Consider switching to bitmap
        public async Task<TexFile> GetTexFileByTagAsync(string tag)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(tag);

                string[] path = tag.Split('\\');
                bool isInSubFolder = IsInSubFolder(path);
                string key = $"{(isInSubFolder ? path[0] : "Base")}_{(isInSubFolder ? path[1] : path[0])}";

                Dictionary<string, TexFile> iconSet = s_icons.FirstOrDefault(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)).Value;

                string dbKey = string.Join('/', isInSubFolder ? path[2..] : path[1..]);
                return iconSet is null ? null! : iconSet.FirstOrDefault(x => x.Key.Equals(dbKey, StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch (Exception ex)
            {
                // Reading tex file failed.
                return null!;
            }
        }

        private static bool IsInSubFolder(string[] fileParam)
        {
            return
               fileParam[0].Equals("xpack", StringComparison.InvariantCultureIgnoreCase)
            || fileParam[0].Equals("xpack2", StringComparison.InvariantCultureIgnoreCase)
            || fileParam[0].Equals("xpack3", StringComparison.InvariantCultureIgnoreCase)
            || fileParam[0].Equals("xpack4", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
