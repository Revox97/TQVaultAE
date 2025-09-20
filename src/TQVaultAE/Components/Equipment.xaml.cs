using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using TQVaultAE.Controllers.Observable;
using TQVaultAE.Models.EventArgs;
using TQVaultAE.Services;

namespace TQVaultAE.Components
{
	/// <summary>
	/// Interaction logic for Equipment.xaml
	/// </summary>
	public partial class Equipment : UserControl, IContentScaleObserver
	{
		public Equipment()
		{
			InitializeComponent();
			ContentScaleController.GetInstance().AddObserver(this);
		}

		public void Notify(object sender, ContentScaleUpdatedEventArgs args)
		{
			SetGridDimensions(args);
		}

		private void SetGridDimensions(ContentScaleUpdatedEventArgs? args = null)
		{
            if (EquipmentOverlay.Source is null)
                return;

            byte[] pixels = BitmapPixelSevice.GetRenderedPixels(EquipmentOverlay, out int stride, out int width, out int height);
            (int, int) columnPositions = SetColumnDefinitions(pixels, stride, height);
            SetRowDefinitions(pixels, stride, height, columnPositions.Item1, columnPositions.Item2);
        }

        private (int, int) SetColumnDefinitions(byte[] pixels, int stride, int height)
        {
            int totalWidth = 0;

            totalWidth = SetColumnDefinition(0, pixels, stride, totalWidth, height, true);
            int columnLeft = totalWidth + 5;

            totalWidth = SetColumnDefinition(1, pixels, stride, totalWidth, height, false);
            totalWidth = SetColumnDefinition(2, pixels, stride, totalWidth, height, true);
            int columnMiddle = totalWidth + 5;

            totalWidth = SetColumnDefinition(3, pixels, stride, totalWidth, height, false);
            totalWidth = SetColumnDefinition(4, pixels, stride, totalWidth, height, true);
            totalWidth = SetColumnDefinition(5, pixels, stride, totalWidth, height, false);
            _ = SetColumnDefinition(6, pixels, stride, totalWidth, height, true);

            return (columnLeft, columnMiddle);
        }

        private int SetColumnDefinition(int column, byte[] pixels, int stride, int totalWidth, int height, bool isTransparent)
        {
            int width = GetWidthByPixels(pixels, stride, totalWidth, height, isTransparent);
            ItemsContainer.ColumnDefinitions[column].Width = new GridLength(width);

            return totalWidth + width;
        }

        private static int GetWidthByPixels(byte[] pixels, int stride, int startX, int imageHeight, bool isTransparent)
        {
            int y = imageHeight / 2;

            for (int x = startX; x < stride / 4; x++) // divide stride by 4 to get width in pixels
            {
                int index = (y * stride) + (x * 4); // proper (x,y) to byte array

                if (index + 3 >= pixels.Length)
                    break;

                byte alpha = pixels[index + 3];

                if (isTransparent && alpha == 0)
                    return x - startX;

                if (!isTransparent && alpha != 0)
                    return x - startX;
            }

            return (stride / 4) - startX; // default fallback
        }

        private void SetRowDefinitions(byte[] pixels, int stride, int height, int leftColumn, int middleColumn)
        {
            int completeHeight = 0;

            completeHeight = SetRowDefinition(0, pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight = SetRowDefinition(1, pixels, stride, completeHeight, leftColumn, height, true);
            completeHeight = SetRowDefinition(2, pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight = SetRowDefinition(3, pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight = SetRowDefinition(4, pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight = SetRowDefinition(5, pixels, stride, completeHeight, leftColumn, height, false);
            completeHeight = SetRowDefinition(6, pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight = SetRowDefinition(7, pixels, stride, completeHeight, leftColumn, height, true);
            completeHeight = SetRowDefinition(8, pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight = SetRowDefinition(9, pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight = SetRowDefinition(10, pixels, stride, completeHeight, leftColumn, height, true);
            completeHeight = SetRowDefinition(11, pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight = SetRowDefinition(12, pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight = SetRowDefinition(13, pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight = SetRowDefinition(14, pixels, stride, completeHeight, leftColumn, height, false);
            _ = SetRowDefinition(15, pixels, stride, completeHeight, middleColumn, height, true);
        }

        private int SetRowDefinition(int row, byte[] pixels, int stride, int totalHeight, int column, int maxHeight, bool isTransparent)
        {
            int height = GetHeightByPixels(pixels, stride, totalHeight, column, maxHeight, isTransparent);
            ItemsContainer.RowDefinitions[row].Height = new GridLength(height);

            return totalHeight + height;
        }

        private static int GetHeightByPixels(byte[] pixels, int stride, int startY, int startX, int imageHeight, bool isTransparent)
        {
            for (int y = startY; y < imageHeight; ++y)
            {
                int index = (y * stride) + (startX * 4); // proper (x,y) to byte array

                if (index + 3 >= pixels.Length)
                    break;

                byte alpha = pixels[index + 3];

                if (isTransparent && alpha == 0)
                    return y - startY;

                if (!isTransparent && alpha != 0)
                    return y - startY;
            }

            return imageHeight - startY; // default fallback
        }

		public void Dispose()
		{
			ContentScaleController.GetInstance().RemoveObserver(this);
			GC.SuppressFinalize(this);
		}

        private void EquipmentBackground_Loaded(object sender, RoutedEventArgs e)
        {
            SetGridDimensions();
        }
    }
}
