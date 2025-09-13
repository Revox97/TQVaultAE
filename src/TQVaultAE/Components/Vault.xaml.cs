using System.Windows.Controls;

namespace TQVaultAE.Components
{
    /// <summary>
    /// Interaction logic for Vault.xaml
    /// </summary>
    public partial class Vault : UserControl
    {
        public Vault()
        {
            InitializeComponent();
			CreateItemsPanel();
        }

		private void CreateItemsPanel()
		{
			ItemsPanel panel = new(12, 20);

			Container.Children.Add(panel);
			Grid.SetRow(panel, 1);
			Grid.SetColumn(panel, 1);
        }
    }
}
