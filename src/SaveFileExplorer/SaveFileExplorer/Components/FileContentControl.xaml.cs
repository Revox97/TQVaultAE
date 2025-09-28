using System.Windows;
using System.Windows.Controls;
using SaveFileExplorer.Models;
using TQVaultAE.IO.Parsers;

namespace SaveFileExplorer.Components
{
    /// <summary>
    /// Interaction logic for FileContentControl.xaml
    /// </summary>
    public partial class FileContentControl : UserControl
    {
        private readonly FileContentControlModel _model;

        public FileContentControl()
        {
            InitializeComponent();

            _model = new FileContentControlModel();
            DataContext = _model;

            GenerateTreeView();
        }

        private void GenerateTreeView()
        {
            _model.ChrFile.Children.ForEach(c => KeyTreeView.Items.Add(GenerateTreeViewItem(c)));
        }

        private TreeViewItem GenerateTreeViewItem(ChrFileRecord record)
        {
            TreeViewItem item = new()
            {
                Header = record.Key,
                DataContext = record,
            };

            item.Selected += KeySelectionChanged;

            foreach(ChrFileRecord child in record.Children)
                item.Items.Add(GenerateTreeViewItem(child));

            return item;
        }

        private void KeySelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is TreeViewItem item && item.DataContext is ChrFileRecord record)
            {
                KeyName.Content = record.Key;
                KeyOffset.Content = $"{record.KeyStart} - {record.KeyEnd}";
                KeyLength.Content = record.KeyEnd - record.KeyStart;
                KeyIsSubStructureOpening.Content = record.Type == ChrRecordType.BeginBlock;
                KeyIsStructureClosing.Content = record.Type == ChrRecordType.EndBlock;

                ValueOffset.Content = $"{record.ValueStart} - {record.ValueEnd}";
                ValueDataLength.Content = record.ValueEnd - record.ValueStart;
                ValueDataType.Content = record.Type;

                ValueAsInt.Text = string.Empty;
                ValueAsBool.Text = string.Empty;
                ValueAsString.Text = string.Empty;
                ValueAsRaw.Text = string.Empty;

                if (record.Type == ChrRecordType.Int && record.Value is int intValue)
                {
                    ValueAsInt.Text = intValue.ToString();
                    return;
                }

                if (record.Type == ChrRecordType.Bool && record.Value is bool boolValue)
                {
                    ValueAsBool.Text = boolValue.ToString();
                    return;
                }

                if (record.Type == ChrRecordType.String && record.Value is string stringValue)
                {
                    ValueAsString.Text = stringValue;
                    return;
                }

                if (record.Value is not null)
                    ValueAsRaw.Text = record.Value.ToString();

                e.Handled = true;
            }
        }
    }
}
