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

        public long FileLength
        {
            get => (long)GetValue(FileLengthProperty);
            set => SetValue(FileLengthProperty, value);
        } 

        internal static DependencyProperty FileNameProperty = DependencyProperty.Register(nameof(FileName), typeof(string), typeof(FileContentControlModel));

        public string FileName
        {
            get => (string)GetValue(FileNameProperty);
            set => SetValue(FileNameProperty, value);
        } 

        internal static DependencyProperty FileExtensionProperty = DependencyProperty.Register(nameof(FileExtension), typeof(string), typeof(FileContentControlModel));

        public string FileExtension
        {
            get => (string)GetValue(FileExtensionProperty);
            set => SetValue(FileExtensionProperty, value);
        } 

        public ChrFileRecord ChrFile { get; set; }

        public FileContentControlModel(string path)
        {
            Path = path;

            FileInfo fileInfo = new(Path);
            FileName = fileInfo.Name[..fileInfo.Name.IndexOf('.')];
            FileExtension = fileInfo.Extension;

            _content = File.ReadAllBytes(Path);
            FileLength = _content.Length;
            ChrFile = new ChrFileParser().Parse(_content);
        }

    }
}
