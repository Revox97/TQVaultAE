using System.IO;
using TQVaultAE.IO.Parsers;

namespace SaveFileExplorer.Models
{
    internal class FileContentControlModel
    {
        private const string Path = @"C:\Users\Leo\Documents\TQVaultTestData\Main\_Templox\Player.chr";
        public ChrFileRecord ChrFile { get; set; }

        public FileContentControlModel()
        {
            byte[] content = File.ReadAllBytes(Path);
            ChrFile = new ChrFileParser().Parse(content);
        }

    }
}
