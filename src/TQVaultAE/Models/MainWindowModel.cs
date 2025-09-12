using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TQVaultAE.Models
{
    public class MainWindowModel
    {
		public string Title { get; } = "TQVaultAE 5.0.0.0";

		public Brush BorderTop { get; set; } = new ImageBrush()
		{
			ImageSource = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/BorderTop.png", UriKind.Absolute)),
			TileMode = TileMode.Tile,
			Viewport = new Rect(0, 0, 3, 11),
			ViewboxUnits = BrushMappingMode.Absolute,
			Stretch = Stretch.None,
		};

		public Brush BorderSide { get; set; } = new ImageBrush()
		{
			ImageSource = new BitmapImage(new Uri("pack://application:,,,/TQVaultAE;component/Resources/Img/BorderSide.png", UriKind.Absolute)),
			TileMode = TileMode.Tile,
			Viewport = new Rect(0, 0, 3, 20),
			ViewboxUnits = BrushMappingMode.Absolute,
			Stretch = Stretch.None,
		};
    }
}
