using System.Windows;
using TQVaultAE.Models;

namespace TQVaultAE
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainWindowModel _model;
		public MainWindow()
		{
			InitializeComponent();
			_model = new MainWindowModel();
			DataContext = _model;
		}
	}
}