using System.Windows;
using System.Windows.Controls;

namespace TQVaultAE.UI.Models
{
    internal class TQWindowModel : DependencyObject
    {
        public bool TriggersResize { get; init; }

        internal static DependencyProperty CanMinimizeMaximizeProperty = DependencyProperty.Register(
            nameof(CanMinimizeMaximize),
            typeof(bool),
            typeof(TqWindow),
            new PropertyMetadata(true)
        );

        public bool CanMinimizeMaximize
        {
            get => (bool)GetValue(CanMinimizeMaximizeProperty);
            set => SetValue(CanMinimizeMaximizeProperty, value);
        }

        internal static DependencyProperty ContentPageProperty = DependencyProperty.Register(
            nameof(ContentPage),
            typeof(Page),
            typeof(TqWindow),
            new PropertyMetadata(null!)
        );

        public Page ContentPage { get; init; }

        public TQWindowModel(Page contentPage, bool triggersResize = false, bool canMinimizeMaximize = true)
        {
            ContentPage = contentPage;
            TriggersResize = triggersResize;
            CanMinimizeMaximize = canMinimizeMaximize;
        }
    }
}
