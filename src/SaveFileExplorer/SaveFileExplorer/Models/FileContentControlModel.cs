using System.IO;
using System.Windows;
using TQVaultAE.IO.Parsers;

namespace SaveFileExplorer.Models
{
    internal class FileContentControlModel : DependencyObject
    {
        private readonly byte[] _content;

        internal static DependencyProperty PathProperty = DependencyProperty.Register(nameof(Path), typeof(string), typeof(FileContentControlModel));

        public string Path
        {
            get => (string)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }

        internal static DependencyProperty FileLengthProperty = DependencyProperty.Register(nameof(FileLength), typeof(long), typeof(FileContentControlModel));

        public long FileLength => _content.Length;

        public ChrFileRecord ChrFile { get; set; }

        public FileContentControlModel(string path)
        {
            Path = path;

            _content = File.ReadAllBytes(Path);
            ChrFile = new ChrFileParser().Parse(_content);
        }

    }
}
