using System.Windows;
using System.Windows.Media;
using TQVaultAE.UI.Resources;

namespace TQVaultAE.Models
{
    public class MainWindowModel
    {
		public string Title { get; } = "TQVaultAE 5.0.0.0";

		public Brush BorderTop { get; set; } = new ImageBrush()
		{
			ImageSource = ImagePaths.Borders.Top,
			TileMode = TileMode.Tile,
			Viewport = new Rect(0, 0, 3, 11),
			ViewboxUnits = BrushMappingMode.Absolute,
			Stretch = Stretch.None,
		};

		public Brush BorderSide { get; set; } = new ImageBrush()
		{
			ImageSource = ImagePaths.Borders.Side,
			TileMode = TileMode.Tile,
			Viewport = new Rect(0, 0, 3, 20),
			ViewboxUnits = BrushMappingMode.Absolute,
			Stretch = Stretch.None,
		};
    }
}
