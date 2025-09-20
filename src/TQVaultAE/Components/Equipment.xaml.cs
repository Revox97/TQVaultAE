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
            int completeWidth = 0;
            int widthColumnZero = GetWidthByPixels(pixels, stride, completeWidth, height, true);
            completeWidth += widthColumnZero;
            int widthColumnOne = GetWidthByPixels(pixels, stride, completeWidth, height, false);
            completeWidth += widthColumnOne;
            int widthColumnTwo = GetWidthByPixels(pixels, stride, completeWidth, height, true);
            completeWidth += widthColumnTwo;
            int widthColumnThree = GetWidthByPixels(pixels, stride, completeWidth, height, false);
            completeWidth += widthColumnThree;
            int widthColumnFour = GetWidthByPixels(pixels, stride, completeWidth, height,  true);
            completeWidth += widthColumnFour;
            int widthColumnFive = GetWidthByPixels(pixels, stride, completeWidth, height, false);
            completeWidth += widthColumnFive;
            int widthColumnSix = GetWidthByPixels(pixels, stride, completeWidth, height, true);

            ItemsContainer.ColumnDefinitions[0].Width = new GridLength(widthColumnZero);
            ItemsContainer.ColumnDefinitions[1].Width = new GridLength(widthColumnOne);
            ItemsContainer.ColumnDefinitions[2].Width = new GridLength(widthColumnTwo);
            ItemsContainer.ColumnDefinitions[3].Width = new GridLength(widthColumnThree);
            ItemsContainer.ColumnDefinitions[4].Width = new GridLength(widthColumnFour);
            ItemsContainer.ColumnDefinitions[5].Width = new GridLength(widthColumnFive);
            ItemsContainer.ColumnDefinitions[6].Width = new GridLength(widthColumnSix);

            int columnLeft = widthColumnZero + 5;
            int columnMiddle = widthColumnZero + widthColumnOne + widthColumnTwo + 5;

            return (columnLeft, columnMiddle);
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
            int heightRowZero = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight += heightRowZero;
            int heightRowOne = GetHeightByPixels(pixels, stride, completeHeight, leftColumn, height, true);
            completeHeight += heightRowOne;
            int heightRowTwo = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight += heightRowTwo;
            int heightRowThree = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight += heightRowThree;
            int heightRowFour = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height,  false);
            completeHeight += heightRowFour;
            int heightRowFive = GetHeightByPixels(pixels, stride, completeHeight, leftColumn, height, false);
            completeHeight += heightRowFive;
            int heightRowSix = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight += heightRowSix;
            int heightRowSeven = GetHeightByPixels(pixels, stride, completeHeight, leftColumn, height, true);
            completeHeight += heightRowSeven;
            int heightRowEight = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight += heightRowEight;
            int heightRowNine = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight += heightRowNine;
            int heightRowTen = GetHeightByPixels(pixels, stride, completeHeight, leftColumn, height, true);
            completeHeight += heightRowTen;
            int heightRowEleven = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight += heightRowEleven;
            int heightRowTwelve = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, true);
            completeHeight += heightRowTwelve;
            int heightRowThirteen = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, false);
            completeHeight += heightRowThirteen;
            int heightRowFourteen = GetHeightByPixels(pixels, stride, completeHeight, leftColumn, height, false);
            completeHeight += heightRowFourteen;
            int heightRowFifteen = GetHeightByPixels(pixels, stride, completeHeight, middleColumn, height, true);

            ItemsContainer.RowDefinitions[0].Height = new GridLength(heightRowZero);
            ItemsContainer.RowDefinitions[1].Height = new GridLength(heightRowOne);
            ItemsContainer.RowDefinitions[2].Height = new GridLength(heightRowTwo);
            ItemsContainer.RowDefinitions[3].Height = new GridLength(heightRowThree);
            ItemsContainer.RowDefinitions[4].Height = new GridLength(heightRowFour);
            ItemsContainer.RowDefinitions[5].Height = new GridLength(heightRowFive);
            ItemsContainer.RowDefinitions[6].Height = new GridLength(heightRowSix);
            ItemsContainer.RowDefinitions[7].Height = new GridLength(heightRowSeven);
            ItemsContainer.RowDefinitions[8].Height = new GridLength(heightRowEight);
            ItemsContainer.RowDefinitions[9].Height = new GridLength(heightRowNine);
            ItemsContainer.RowDefinitions[10].Height = new GridLength(heightRowTen);
            ItemsContainer.RowDefinitions[11].Height = new GridLength(heightRowEleven);
            ItemsContainer.RowDefinitions[12].Height = new GridLength(heightRowTwelve);
            ItemsContainer.RowDefinitions[13].Height = new GridLength(heightRowThirteen);
            ItemsContainer.RowDefinitions[14].Height = new GridLength(heightRowFourteen);
            ItemsContainer.RowDefinitions[15].Height = new GridLength(heightRowFifteen);
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
