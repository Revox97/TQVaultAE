using System.Diagnostics;
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
        private const string Path = @"C:\Users\Leo\Documents\TQVaultTestData\Main\_Templox\Player.chr";
        private readonly FileContentControlModel _model;

        public FileContentControl()
        {
            InitializeComponent();

            _model = new FileContentControlModel(Path);
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

            foreach (ChrFileRecord child in record.Children)
                item.Items.Add(GenerateTreeViewItem(child));

            return item;
        }

        private void KeySelectionChanged(object sender, RoutedEventArgs e)
        {
            // Fix event will trigger for parents as well
            e.Handled = true;

            if (sender is TreeViewItem item && item.DataContext is ChrFileRecord record)
            {
                // TODO Bind data context of the control, instead of each individual value
                KeyName.Content = record.Key;
                KeyOffset.Text = $"{record.KeyStart} - {record.KeyEnd}";
                KeyLength.Text = record.KeyLength.ToString();
                KeyIsSubStructureOpening.Text = (record.Type == ChrRecordType.BeginBlock).ToString();
                KeyIsStructureClosing.Text = (record.Type == ChrRecordType.EndBlock).ToString();

                ValueOffset.Text = $"{record.ValueStart} - {record.ValueEnd}";
                ValueDataLength.Text = record.ValueLength.ToString();
                ValueDataType.Text = record.Type.ToString();

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
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            string link = _model.Path;

            string argument = $"/select, \"{link}\"";
            Process.Start("explorer.exe", argument);
        }
    }
}
